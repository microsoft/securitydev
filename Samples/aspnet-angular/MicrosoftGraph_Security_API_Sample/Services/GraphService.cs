// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using Microsoft.Identity.Client;
using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Requests;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Directory = System.IO.Directory;

namespace MicrosoftGraph_Security_API_Sample.Services
{
    public class GraphService : IGraphService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphService" /> class
        /// </summary>
        public GraphService(AzureConfiguration azureConfig, string jwtToken)
        {
            _graphClient = SDKHelper.GetAuthenticatedClient(azureConfig, jwtToken);

            AccessToken = SDKHelper.GetAccessToken(azureConfig, jwtToken);

            JWTToken = jwtToken;

            if (_graphClient != null)
            {
                GraphUrlVersion = azureConfig.UrlVersion;
                NotificationUri = azureConfig.NotificationUri;
                GraphUrl = azureConfig.BaseUrl + GraphUrlVersion;
                _graphClient.BaseUrl = GraphUrl;
            }
        }

        public string GetAccessToken()
        {
            return AccessToken;
        }

        /// <summary>
        /// The Microsoft graph beta url version (User Picture details is in Beta)
        /// So we use GraphBetaUrl to retrieve additional details about the user
        /// </summary>
        public string GraphBetaUrl = "https://graph.microsoft.com/beta";

        /// <summary>
        /// The Access Token for this appication
        /// </summary>
        public string JWTToken { get; set; }

        /// <summary>
        /// The Access Token for Microsoft graph with App Scope
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The Notification Uri
        /// </summary>
        public string NotificationUri { get; set; } = string.Empty;

        /// <summary>
        /// Gets the Microsoft graph base url
        /// </summary>
        public string GraphUrl { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the Microsoft graph url version (v1.0 or beta) based on web.config file
        /// </summary>
        public string GraphUrlVersion { get; set; }

        /// <summary>
        /// The graphClient object
        /// </summary>
        private GraphServiceClient _graphClient = null;

        /// <summary>
        /// Get the current user's email address from their profile.
        /// </summary>
        /// <returns>Email address of the signed in user</returns>
        public async Task<string> GetMyEmailAddressAsync()
        {
            try
            {
                User me = await _graphClient.Me.Request().Select("mail,userPrincipalName").GetAsync();
                return me.Mail ?? me.UserPrincipalName;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get additional details about the user to help in investigating the alert
        /// </summary>
        /// <param name="principalName">User principal name</param>
        /// <param name="populatePicture"></param>
        /// <param name="populateManager"></param>
        /// <param name="populateDevices"></param>
        /// <param name="populateRiskyUser"></param>
        /// <returns>Graph User Model</returns>
        public async Task<UserAccountDevice> GetUserDetailsAsync(string principalName, bool populatePicture = false, bool populateManager = false, bool populateDevices = false, bool populateRiskyUser = false)
        {
            try
            {
                List<Task> taskList = new List<Task>();
                UserAccountDevice userModel = new UserAccountDevice();
                Task<Stream> populatePictureTask = null;
                Task<IUserRegisteredDevicesCollectionWithReferencesPage> registeredDevicesTask = null;
                Task<IUserOwnedDevicesCollectionWithReferencesPage> ownedDevicesTask = null;
                Task<DirectoryObject> managerTask = null;
                Task<RiskyUser> riskyUserTask = null;
                _graphClient.BaseUrl = this.GraphBetaUrl;

                var userPageTask = _graphClient.Users.Request().Filter($"UserPrincipalName eq '{principalName}'").GetAsync();

                taskList.Add(userPageTask);

                if (populatePicture)
                {
                    populatePictureTask = _graphClient.Users[principalName].Photo.Content.Request().GetAsync();

                    taskList.Add(populatePictureTask);
                }

                if (populateManager)
                {
                    managerTask = ManagerAsync(principalName, _graphClient);
                    taskList.Add(managerTask);
                }

                if (populateDevices)
                {
                    registeredDevicesTask = _graphClient.Users[principalName].RegisteredDevices.Request().GetAsync();
                    taskList.Add(registeredDevicesTask);
                    ownedDevicesTask = _graphClient.Users[principalName].OwnedDevices.Request().GetAsync();
                    taskList.Add(ownedDevicesTask);
                }

                //if (populateRiskyUser)
                //{
                //    riskyUserTask = RiskyUserAsync(principalName);
                //    taskList.Add(riskyUserTask);
                //}

                await Task.WhenAll(taskList);

                if (userPageTask.Result.Count > 0)
                {
                    userModel = BuildGraphAccountDeviceModel(userPageTask.Result?[0], principalName);
                }

                if (populatePicture && populatePictureTask != null && populatePictureTask?.Result != null)
                {
                    MemoryStream picture1 = (MemoryStream)populatePictureTask.Result;
                    string pic = "data:image/png;base64," + Convert.ToBase64String(picture1.ToArray(), 0, picture1.ToArray().Length);
                    userModel.Picture = pic;
                }

                if (populateManager && managerTask != null && managerTask.Result != null)
                {
                    if (!string.IsNullOrEmpty(managerTask.Result?.Id))
                    {
                        userModel.Manager = BuildGraphAccountDeviceModel(await _graphClient.Users[managerTask.Result?.Id].Request().GetAsync());
                    }
                }

                if (populateRiskyUser)
                {
                    if (userPageTask.Result.Count > 0)
                    {
                        riskyUserTask = RiskyUserAsync(userPageTask.Result?[0].Id);
                        userModel.RiskyUser = await riskyUserTask;
                    }
                    Debug.WriteLine(riskyUserTask.Result);
                }

                if (populateDevices && registeredDevicesTask != null && registeredDevicesTask.Result != null)
                {
                    userModel.RegisteredDevices = await Task.WhenAll(registeredDevicesTask.Result.Select(dev => GetDeviceDetailsAsync(dev.Id)));
                }

                if (populateDevices && ownedDevicesTask != null && ownedDevicesTask.Result != null)
                {
                    userModel.OwnedDevices = await Task.WhenAll(ownedDevicesTask.Result.Select(dev => GetDeviceDetailsAsync(dev.Id)));
                }

                _graphClient.BaseUrl = this.GraphUrl;

                return userModel;
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
        }

        private static async Task<DirectoryObject> ManagerAsync(string principalName, GraphServiceClient _graphClient)
        {
            try
            {
                return await Task.Run(() => _graphClient.Users[principalName].Manager.Request().GetAsync());
            }
            catch /// If the permissions scope for Graph API is not enough then there will be an exception
            {
                return null;
            }
        }

        //private static async Task<DirectoryObject> RiskyUserAsync(string id, GraphServiceClient _graphClient)
        //{
        //    try
        //    {
        //        return await Task.Run(() => _graphClient.Users[id].Manager.Request().GetAsync());
        //    }
        //    catch /// If the permissions scope for Graph API is not enough then there will be an exception
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Get Risky user by ID
        /// Risky users is still in Beta. So This sample uses REST queries to get the risky user, since the official SDK is only available for workloads in V1.0
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>RiskyUser Object</returns>
        public async Task<RiskyUser> RiskyUserAsync(string id)
        {
            try
            {
                var startDateTime = DateTime.Now;
                string endpoint = $"https://graph.microsoft.com/beta/riskyUsers?$filter=id eq '{id}'";
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                        using (var response = await client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                var settings = new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore,
                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                };
                                RiskyUsersResult riskyUsersResult = JsonConvert.DeserializeObject<RiskyUsersResult>(result, settings);

                                Debug.WriteLine($"GraphService/RiskyUserAsync execution time: {DateTime.Now - startDateTime}");
                                return riskyUsersResult.value?[0];
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get additional details about the affected device
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Graph Device Model</returns>
        public async Task<Device> GetDeviceDetailsAsync(string id)
        {
            var device = await _graphClient.Devices[id].Request().GetAsync();

            return device;
        }

        /// <summary>
        /// Update specific fields of alert
        /// </summary>
        /// <param name="alert"></param>
        /// <param name="updateAlertModel"></param>
        /// <returns>the updated alert</returns>
        public async Task<Alert> UpdateAlertAsync(Alert alert, AlertUpdateRequest updateAlertModel)
        {
            if (alert == null)
            {
                throw new ArgumentNullException(nameof(alert));
            }

            if (!Enum.TryParse<AlertStatus>(updateAlertModel.Status, true, out var status))
            {
                throw new ArgumentOutOfRangeException(nameof(alert.Status));
            }

            alert.Status = status;

            if (!Enum.TryParse<AlertFeedback>(updateAlertModel.Feedback, true, out var feedback))
            {
                throw new ArgumentOutOfRangeException(nameof(alert.Feedback));
            }

            alert.Feedback = feedback;
            alert.Comments = updateAlertModel.Comments ?? new List<string>();

            alert.AssignedTo = updateAlertModel.AssignedTo;
            //alert.AssignedTo = await GetMyEmailAddressAsync();

            return await _graphClient.Security.Alerts[alert.Id].Request().UpdateAsync(alert);
        }

        /// <summary>
        /// Get alert by Id
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns>Alert object</returns>
        public async Task<Alert> GetAlertDetailsAsync(string alertId)
        {
            if (string.IsNullOrEmpty(alertId))
            {
                return null;
            }

            var startDateTime = DateTime.Now;
            Alert alert = await _graphClient.Security.Alerts[alertId].Request().GetAsync();

            object aipData = null;
            alert.AdditionalData?.TryGetValue("AIPDataAccessState", out aipData);
            var finishDateTime = DateTime.Now;
            Debug.WriteLine($"Get Alert Details: {finishDateTime - startDateTime}");
            alert.AdditionalData.Clear();
            if (aipData != null)
            {
                alert.AdditionalData.Add("AIPDataAccessState", aipData);
            }
            return alert;
        }

        /// <summary>
        /// Get alerts based on the alert filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>alerts matching the filtering criteria</returns>
        public async Task<Tuple<IEnumerable<Alert>, string>> GetAlertsAsync(AlertFilterModel filters, Dictionary<string, string> odredByParams = null)
        {
            try
            {
                var startDateTime = DateTime.Now;
                if (filters == null)
                {
                    var result = await _graphClient.Security.Alerts.Request().GetAsync();
                    Debug.WriteLine($"GraphService/GetAlertsAsync execution time: {DateTime.Now - startDateTime}");
                    return new Tuple<IEnumerable<Alert>, string>(result, string.Empty);
                }
                else if (filters != null && filters.Count == 0)
                {
                    var result = await _graphClient.Security.Alerts.Request().Top(filters.Top).GetAsync();
                    Debug.WriteLine($"GraphService/GetAlertsAsync execution time: {DateTime.Now - startDateTime}");
                    return new Tuple<IEnumerable<Alert>, string>(result, string.Empty);
                }
                else
                {
                    // var s = _graphClient.Security.Alerts.Request()
                    // Create filter query
                    var filterQuery = GraphQueryProvider.GetQueryByAlertFilter(filters);

                    var customOrderByParams = new Dictionary<string, string>();
                    //// If there are no filters and there are no custom odredByParams (if specified only top X)
                    if ((odredByParams == null || odredByParams.Count() < 1) && filters.Count < 1)
                    {
                        //// Order by 1. Provider 2. CreatedDateTime (desc)
                        customOrderByParams.Add("vendorInformation/provider", "asc");
                        customOrderByParams.Add("createdDateTime", "desc");
                    }
                    else if (filters.Count >= 1 && filters.ContainsKey("createdDateTime"))
                    {
                        customOrderByParams.Add("createdDateTime", "desc");
                    }

                    // Create request with filter and top X
                    var request = _graphClient.Security.Alerts.Request().Filter(filterQuery).Top(filters.Top);

                    // Add order py params
                    if (customOrderByParams.Count > 0)
                    {
                        request = request.OrderBy(string.Join(", ", customOrderByParams.Select(param => $"{param.Key} {param.Value}")));
                    }
                    else if (odredByParams != null && odredByParams.Count() > 0)
                    {
                        request = request.OrderBy(string.Join(", ", odredByParams.Select(param => $"{param.Key} {param.Value}")));
                    }

                    // Get alerts
                    var result = await request.GetAsync();

                    Debug.WriteLine($"GraphService/GetAlertsAsync execution time: {DateTime.Now - startDateTime}");
                    return new Tuple<IEnumerable<Alert>, string>(result, filterQuery);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Create web hook subscriptions in order to receive the notifications
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>The subscription object</returns>
        public async Task<Tuple<Subscription, string>> SubscribeAsync(AlertFilterModel filters)
        {
            var startDateTime = DateTime.Now;
            try
            {
                var changeType = "updated";
                var expirationDate = DateTime.UtcNow.AddHours(3);

                var randno = new Random().Next(1, 100).ToString();
                var clientState = "IsgSdkSubscription" + randno;

                var filteredQuery = GraphQueryProvider.GetQueryByAlertFilter(filters);

                var resource = filters.ContainsKey("AlertId") && filters.HasPropertyFilter("AlertId")
                    ? $"/security/alerts/{filters.GetFirstPropertyFilter("AlertId").Value}"
                    : $"/security/alerts{(!String.IsNullOrWhiteSpace(filteredQuery) ? $"?$filter={filteredQuery}" : string.Empty)}";

                Subscription subscription = new Subscription()
                {
                    ChangeType = changeType,
                    NotificationUrl = NotificationUri,
                    Resource = resource,
                    ExpirationDateTime = expirationDate,
                    ClientState = clientState
                };

                var result = await _graphClient.Subscriptions.Request().AddAsync(subscription);

                Debug.WriteLine($"GraphService/SubscribeAsync execution time: {DateTime.Now - startDateTime}");
                return new Tuple<Subscription, string>(result, filteredQuery);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
        }

        /// <summary>
        /// List existing active subscriptions for this application
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>The subscription collection page</returns>
        public async Task<IGraphServiceSubscriptionsCollectionPage> GetListSubscriptionsAsync()
        {
            return await _graphClient.Subscriptions.Request().GetAsync();
        }

        /// <summary>
        /// Get list of all existing providers
        /// </summary>
        /// <returns>List of providers</returns>
        public IEnumerable<string> GetProviderList(Tuple<IEnumerable<Alert>, string> alerts)
        {
            string[] empty = new string[1];
            return alerts != null && alerts.Item1 != null
                ? alerts.Item1.Select(alert => alert.VendorInformation.Provider).ToArray().Distinct()
                : empty;
        }

        /// <summary>
        /// Get information about alert statistics
        /// </summary>
        /// <returns>Statistics Data</returns>
        public async Task<AlertStatisticModel> GetStatisticAsync(int topAlertAmount)
        {
            try
            {
                var startDateTime = DateTime.Now;
                // Select all unresolved alerts
                var allAlertsFilter = new AlertFilterModel { Top = topAlertAmount };
                var allAlerts = await GetAlertsAsync(allAlertsFilter);
                var unresolvedAlerts = allAlerts?.Item1?.Where(alert => alert.Status != AlertStatus.Resolved);
                var newAlerts = allAlerts?.Item1?.Where(alert => alert.Status == AlertStatus.NewAlert);
                var inProgressAlerts = allAlerts?.Item1?.Where(alert => alert.Status == AlertStatus.InProgress);
                var resolvedAlerts = allAlerts?.Item1?.Where(alert => alert.Status == AlertStatus.Resolved);
                    //// Get top secure score
                var  secureScores = await GetSecureScoresAsync($"?$top={topAlertAmount}");
                var latestSecureScore = secureScores.OrderByDescending(rec => rec.CreatedDateTime).FirstOrDefault();
                Dictionary<string, int> res = new Dictionary<string, int>();
                foreach (var comparativeScore in latestSecureScore?.AverageComparativeScores)
                {
                    res.Add(comparativeScore.Basis, (int)comparativeScore.AverageScore);
                }

                var secureScoreModel = latestSecureScore != null
                    ? new SecureScoreStatisticModel
                    {
                        Current = latestSecureScore.CurrentScore ?? 0,
                        Max = latestSecureScore.MaxScore ?? 0,
                        ComparativeScores = res,
                    }
                    : null;

                var activeAlerts = new Dictionary<string, int>();
                var newAlertsD = new Dictionary<string, int>();
                var inProgressAlertsD = new Dictionary<string, int>();
                var resolcedAlertsD = new Dictionary<string, int>();
                var usersAlerts = new StatisticCollectionModel<SeveritySortOrder>();
                var hostsAlerts = new StatisticCollectionModel<SeveritySortOrder>();
                var providersAlerts = new StatisticCollectionModel<SeveritySortOrder>();
                var ipAlerts = new StatisticCollectionModel<SeveritySortOrder>();
                var domainAlerts = new StatisticCollectionModel<SeveritySortOrder>();

                if (unresolvedAlerts != null)
                {
                    foreach (var alert in unresolvedAlerts)
                    {
                        // Calculate users with the most alerts
                        var userPrincipalName = alert.UserStates?.FirstOrDefault()?.UserPrincipalName;
                        if (!string.IsNullOrWhiteSpace(userPrincipalName))
                        {
                            usersAlerts.Add(userPrincipalName, alert.Severity.ToString());
                        }

                        // Calculate destination ip address with the most alerts
                        var ipAddress = alert.NetworkConnections?.FirstOrDefault()?.DestinationAddress;
                        if (!string.IsNullOrWhiteSpace(ipAddress))
                        {
                            ipAlerts.Add(ipAddress, alert.Severity.ToString());
                        }

                        // Calculate hosts with the most alerts
                        var hostName = alert.HostStates?.FirstOrDefault()?.Fqdn;
                        if (!string.IsNullOrWhiteSpace(hostName))
                        {
                            hostsAlerts.Add(hostName, alert.Severity.ToString());
                        }

                        // Calculate providers with the most alerts
                        var provider = alert.VendorInformation.Provider;
                        if (!string.IsNullOrWhiteSpace(provider))
                        {
                            providersAlerts.Add(provider, alert.Severity.ToString());
                        }

                        // Calculate domain with the most alerts
                        var domainName = alert.NetworkConnections?.FirstOrDefault()?.DestinationDomain;
                        if (!string.IsNullOrWhiteSpace(domainName))
                        {
                            domainAlerts.Add(domainName, alert.Severity.ToString());
                        }
                    }
                }

                if (newAlerts != null)
                {
                    foreach (var alert in newAlerts)
                    {
                        // Calculate active alerts
                        if (!newAlertsD.ContainsKey(alert.Severity.ToString()))
                        {
                            newAlertsD.Add(alert.Severity.ToString(), 1);
                        }
                        else
                        {
                            ++newAlertsD[alert.Severity.ToString()];
                        }
                    }
                }

                if (inProgressAlerts != null)
                {
                    foreach (var alert in inProgressAlerts)
                    {
                        // Calculate active alerts
                        if (!inProgressAlertsD.ContainsKey(alert.Severity.ToString()))
                        {
                            inProgressAlertsD.Add(alert.Severity.ToString(), 1);
                        }
                        else
                        {
                            ++inProgressAlertsD[alert.Severity.ToString()];
                        }
                    }
                }

                if (resolvedAlerts != null)
                {
                    foreach (var alert in resolvedAlerts)
                    {
                        // Calculate active alerts
                        if (!resolcedAlertsD.ContainsKey(alert.Severity.ToString()))
                        {
                            resolcedAlertsD.Add(alert.Severity.ToString(), 1);
                        }
                        else
                        {
                            ++resolcedAlertsD[alert.Severity.ToString()];
                        }
                    }
                }

                // Get top of the sorted users with the most alerts
                var sortedTopUserAlertsWithPrincipalNames = usersAlerts.GetSortedTopValues(4);
                var sortedTopUserAlert = new Dictionary<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>>();

                var usersList = sortedTopUserAlertsWithPrincipalNames.Select(rec => rec.Key);
                // Get additional information about each user from top list
                var users = await GetUserDisplayNamesAsync(usersList);

                //// Replaced UserPrincipalName to DisplayName if it is possible
                if (users != null)
                {
                    foreach (var rec in sortedTopUserAlertsWithPrincipalNames)
                    {
                        var newKey = users.ContainsKey(rec.Key) && !users[rec.Key].Equals(rec.Key, StringComparison.CurrentCultureIgnoreCase) ? users[rec.Key] : rec.Key;
                        sortedTopUserAlert.Add(new KeyValuePair<string, string>(newKey, rec.Key), rec.Value);
                    }
                }

                Debug.WriteLine($"GraphService/GetStatisticAsync topAlertAmount: {topAlertAmount}, execution time: {DateTime.Now - startDateTime}");
                return new AlertStatisticModel
                {
                    NewAlerts = newAlertsD,
                    InProgressAlerts = inProgressAlertsD,
                    ResolvedAlerts = resolcedAlertsD,
                    SecureScore = secureScoreModel,
                    UsersWithTheMostAlerts = sortedTopUserAlert,
                    HostsWithTheMostAlerts = hostsAlerts.GetSortedTopValues(4)
                        .Select(rec => new KeyValuePair<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>>(
                            new KeyValuePair<string, string>(
                                rec.Key.Split('.').FirstOrDefault() ?? rec.Key,
                                rec.Key),
                            rec.Value)).ToDictionary(rec => rec.Key, rec => rec.Value),
                    ProvidersWithTheMostAlerts = providersAlerts.GetSortedTopValues(topAlertAmount).Select(rec => new KeyValuePair<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>>(
                            new KeyValuePair<string, string>(
                                rec.Key,
                                rec.Key),
                            rec.Value)).ToDictionary(rec => rec.Key, rec => rec.Value),
                    IPWithTheMostAlerts = ipAlerts.GetSortedTopValues(4)
                        .Select(rec => new KeyValuePair<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>>(
                            new KeyValuePair<string, string>(
                                rec.Key.Split('.').FirstOrDefault() ?? rec.Key,
                                rec.Key),
                            rec.Value)).ToDictionary(rec => rec.Key, rec => rec.Value),
                    DomainsWithTheMostAlerts = domainAlerts.GetSortedTopValues(4)
                        .Select(rec => new KeyValuePair<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>>(
                            new KeyValuePair<string, string>(
                                rec.Key.Split('.').FirstOrDefault() ?? rec.Key,
                                rec.Key),
                            rec.Value)).ToDictionary(rec => rec.Key, rec => rec.Value),
                };
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
        }

        /// <summary>
        /// List existing secure scores
        /// Secure scores is still in Beta. So This sample uses REST queries to get the secure scores, since the official SDK is only available for workloads in V1.0
        /// </summary>
        /// <returns>List of secure scores</returns>
        public async Task<IEnumerable<SecureScore>> GetSecureScoresAsync(string queryParameter)
        {
            try
            {
                var startDateTime = DateTime.Now;
                string endpoint = "https://graph.microsoft.com/beta/security/securescores";
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint + queryParameter))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                        using (var response = await client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                SecureScoreResult secureScoreResult = JsonConvert.DeserializeObject<SecureScoreResult>(result);
                                Debug.WriteLine($"GraphService/GetSecureScoresAsync execution time: {DateTime.Now - startDateTime}");
                                return secureScoreResult.Value;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// List existing secure scores
        /// Secure scores is still in Beta. So This sample uses REST queries to get the secure scores, since the official SDK is only available for workloads in V1.0
        /// </summary>
        /// <returns>List of secure scores</returns>
        public async Task<IEnumerable<SecurityActionResponse>> GetSecurityActionsAsync()
        {
            try
            {
                var startDateTime = DateTime.Now;
                string endpoint = "https://graph.microsoft.com/beta/security/securityActions";
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                        using (var response = await client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                SecurityActionResult securityActionResult = JsonConvert.DeserializeObject<SecurityActionResult>(result);

                                var responses = securityActionResult.Value.ToSecurityActionResponses();
                                //SecurityActionResponses securityActionResult = JsonConvert.DeserializeObject<SecurityActionResponses>(result);


                                Debug.WriteLine($"GraphService/GetSecurityActions execution time: {DateTime.Now - startDateTime}");
                                return responses;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<SecurityActionResponse> AddSecurityActionsAsync(SecurityAction action)
        {
            try
            {
                var startDateTime = DateTime.Now;
                string endpoint = "https://graph.microsoft.com/beta/security/securityActions";
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, endpoint))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                        request.Content = new StringContent(JsonConvert.SerializeObject(action));
                        using (var response = await client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                SecurityActionResponse securityActionResponse = JsonConvert.DeserializeObject<SecurityActionResponse>(result);
                                // SecureScoreResult secureScoreResult = JsonConvert.DeserializeObject<SecureScoreResult>(result);
                                Debug.WriteLine($"GraphService/GetSecurityActions execution time: {DateTime.Now - startDateTime}");
                                return securityActionResponse;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get filter proders
        /// </summary>
        /// <returns><Dictionary<string, IEnumerable<string>> of filter categories</returns>
        public Dictionary<string, IEnumerable<string>> GetCategoriesFilter(Tuple<IEnumerable<Alert>, string> alerts)
        {
            try
            {
                var filter = new Dictionary<string, IEnumerable<string>>();

                var categories = GetCategoriesList(alerts);

                filter.Add("AlertCategories", categories);

                return filter;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get filter proders
        /// </summary>
        /// <returns><Dictionary<string, IEnumerable<string>> of filter categories</returns>
        public Dictionary<string, IEnumerable<string>> GetCategoriesFilter(IEnumerable<string> categories)
        {
            try
            {
                var filter = new Dictionary<string, IEnumerable<string>>
                {
                    { "AlertCategories", categories }
                };

                return filter;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get filter proders
        /// </summary>
        /// <returns><Dictionary<string, IEnumerable<string>> of filter proders</returns>
        public Dictionary<string, IEnumerable<string>> GetProvidersFilter(Tuple<IEnumerable<Alert>, string> alerts, string name)
        {
            try
            {
                var filter = new Dictionary<string, IEnumerable<string>>();

                var providers = GetProviderList(alerts);

                filter.Add($"{name}Providers", providers);

                return filter;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get filter proders
        /// </summary>
        /// <returns><Dictionary<string, IEnumerable<string>> of filter proders</returns>
        public Dictionary<string, IEnumerable<string>> GetProvidersFilter(IEnumerable<string> providers, string name)
        {
            try
            {
                var filter = new Dictionary<string, IEnumerable<string>>
                {
                    { $"{name}Providers", providers }
                };

                return filter;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get User Principal Name Filter
        /// </summary>
        /// <returns><Dictionary<string, IEnumerable<string>> of filter User Principal Names</returns>
        public async Task<Dictionary<string, IEnumerable<string>>> GetUserPrincipalNameFilterAsync()
        {
            try
            {
                var filter = new Dictionary<string, IEnumerable<string>>();

                var users = await _graphClient.Users.Request().GetAsync();
                var upn = users.CurrentPage.Select(user => user.UserPrincipalName).ToList().Distinct().ToList();
                while (users.NextPageRequest != null)
                {
                    users = await users.NextPageRequest.GetAsync();
                    upn.AddRange(users.CurrentPage.Select(user => user.UserPrincipalName).ToList().Distinct().ToList());
                }

                upn.Distinct();

                filter.Add("AlertUserPrincipalName", upn);

                return filter;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get secure score control profiles
        /// Secure score control profiles is still in Beta.
        /// So This sample uses REST queries to get the secure score control profiles, since the official SDK is only available for workloads in V1.0
        /// </summary>
        /// <returns>List of secure scores</returns>
        public async Task<IEnumerable<SecureScoreControlProfileModel>> GetSecureScoreControlProfilesAsync()
        {
            var startDateTime = DateTime.Now;
            List<SecureScoreControlProfile> secureScoreControlProfiles = null;
            try
            {
                string endpoint = "https://graph.microsoft.com/beta/security/securescorecontrolprofiles";
                string queryParameter = "?$top=100";
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint + queryParameter))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                        using (var response = await client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                SecureScoreControlProfileResult secureScoreResult = JsonConvert.DeserializeObject<SecureScoreControlProfileResult>(result);
                                secureScoreControlProfiles = secureScoreResult.Value;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
            var secureProfiles = secureScoreControlProfiles?.Select(profile => new SecureScoreControlProfileModel
            {
                ControlCategory = profile.ControlCategory,
                Title = profile.AdditionalData.ContainsKey("title") ? profile.AdditionalData["title"].ToString() : string.Empty,
                Rank = profile.AdditionalData.ContainsKey("rank") ? (int?)int.Parse(profile.AdditionalData["rank"].ToString()) : null,
                ImplementationCost = profile.ImplementationCost,
                MaxScore = profile.MaxScore,
                UserImpact = profile.UserImpact,
                ActionUrl = profile.ActionUrl,
                Deprecated = profile.Deprecated,
                LastModifiedDateTime = profile.LastModifiedDateTime,
                AzureTenantId = profile.AzureTenantId,
                Remediation = profile.Remediation,
                RemediationImpact = profile.RemediationImpact,
                Service = profile.Service,
                TenantNote = profile.TenantNote,
                TenantSetState = profile.TenantSetState,
                Threats = profile.Threats,
                Tier = profile.Tier,
                SecureStateUpdates = profile.AdditionalData.ContainsKey("controlStateUpdates")
                            ? JsonConvert.DeserializeObject<IEnumerable<ControlStateUpdateModel>>(profile.AdditionalData["controlStateUpdates"].ToString())
                            : Enumerable.Empty<ControlStateUpdateModel>()
            })
                        ?? Enumerable.Empty<SecureScoreControlProfileModel>();
            _graphClient.BaseUrl = this.GraphUrl;

            //var usersAssignedTo = secureProfiles.SelectMany(
            //    profile => profile.SecureStateUpdates.SelectMany(
            //        update => new List<string>() { update.UpnAssignedTo, update.UpnUpdatedBy }).Where(upn => !string.IsNullOrWhiteSpace(upn)).Distinct());
            //var users = await GetUserDisplayNamesAsync(usersAssignedTo);
            Debug.WriteLine($"GraphService/GetSecureScoreControlProfilesAsync execution time: {DateTime.Now - startDateTime}");
            return secureProfiles;
        }

        /// <summary>
        /// Build Graph User Model
        /// </summary>
        /// <param name="device"></param>
        /// <returns>Graph User Model</returns>
        private static UserAccountDevice BuildGraphAccountDeviceModel(User user, string upn = null)
        {
            try
            {
                if (user == null)
                {
                    return null;
                }

                return new UserAccountDevice
                {
                    JobTitle = user?.JobTitle,
                    OfficeLocation = user?.OfficeLocation,
                    Email = upn ?? user?.UserPrincipalName,
                    ContactVia = user?.MobilePhone,
                    DisplayName = user?.DisplayName,
                    Manager = new UserAccountDevice() { Upn = user?.Manager?.Id },
                    Upn = upn ?? user?.UserPrincipalName,
                };
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Get user display names for a list of user principal names
        /// </summary>
        /// <returns>dictionary that maps user name to user principal name</returns>
        private async Task<IDictionary<string, string>> GetUserDisplayNamesAsync(IEnumerable<string> upns)
        {
            var startDateTime = DateTime.Now;
            //// Create filter for query
            var filterRequest = string.Join(
                $" {AlertFilterOperator.Or} ",
                upns.Select(upn => $"UserPrincipalName {AlertFilterOperator.Equals} '{upn}'"));
            Dictionary<string, string> dict = (await _graphClient.Users.Request().Filter(filterRequest).GetAsync()).ToDictionary(user => user.UserPrincipalName, user => user.DisplayName, StringComparer.InvariantCultureIgnoreCase);
            //// Get additional information about each user from top list
            Debug.WriteLine($"GraphService/GetUserDisplayNamesAsync {filterRequest} execution time: {DateTime.Now - startDateTime}");
            return dict;
        }

        /// <summary>
        /// Get access token for azure active directory application with scope for this application
        /// </summary>
        /// <returns>access token for azure active directory application</returns>
        private async Task<AuthResult> GetAccessTokenForApplicationAsync(string appId, string appSecret, string tenantId, string redirectUri)
        {
            AuthResult result = new AuthResult();
            try
            {
                ConfidentialClientApplication clientApp = new ConfidentialClientApplication(
                    appId,
                    $"https://login.microsoftonline.com/{tenantId}/v2.0",
                    redirectUri,
                    new ClientCredential(appSecret),
                    null,
                    new TokenCache());
                AuthenticationResult authResult = await clientApp.AcquireTokenForClientAsync(new string[] { "https://graph.microsoft.com/.default" });
                result.AccessToken = authResult?.AccessToken;
            }
            catch (Exception err)
            {
                result.Exception = err;
            }

            return result;
        }

        /// <summary>
        /// Get list of all existing categories
        /// </summary>
        /// <returns>List of categories</returns>
        private IEnumerable<string> GetCategoriesList(Tuple<IEnumerable<Alert>, string> alerts)
        {
            string[] empty = new string[1];
            return alerts != null && alerts.Item1 != null
                ? alerts.Item1.Select(alert => alert.Category).ToArray().Distinct()
                : empty;
        }
    }
}