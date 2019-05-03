// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Requests;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Authorize]
    [Route("api/alerts")]
    public class AlertController : Controller
    {
        private readonly IGraphServiceProvider _graphServiceProvider;

        private readonly IAlertService _alertService;

        private IMemoryCacheHelper _memoryCacheHelper;

        private IGraphService _graphService;

        private IDemoExample _demoExample;

        public AlertController(IAlertService alertService, IDemoExample demoExample, IGraphServiceProvider graphServiceProvider, IMemoryCacheHelper memoryCacheHelper)
        {
            _alertService = alertService;
            _demoExample = demoExample;
            _memoryCacheHelper = memoryCacheHelper;
            _graphServiceProvider = graphServiceProvider;
        }

        /// <summary>
        /// Updates specific fields of alert
        /// </summary>
        /// <param name="updateAlertModel"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAlert([FromBody]AlertUpdateRequest updateAlertModel, string id)
        {
            try
            {
                var start = DateTime.Now;
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var sdkQueryBuilder = new StringBuilder();
                var restQueryBuilder = new StringBuilder();

                sdkQueryBuilder.Append($"await graphClient.Security.Alerts[\"{id}\"].Request().UpdateAsync(updatedAlert)");

                restQueryBuilder.Append($"PATCH <a>https://graph.microsoft.com/{_graphService.GraphUrlVersion}/security/alerts/{id}</a>");

                var email = $"\"{await _graphService.GetMyEmailAddressAsync()}\"";

                if (!string.IsNullOrEmpty(updateAlertModel.AssignedTo))
                {
                    email = $" \"assignedTo\" = \"{updateAlertModel.AssignedTo}\" ";
                }

                if (!Enum.TryParse<AlertStatus>(updateAlertModel.Status, true, out var status))
                {
                    throw new ArgumentOutOfRangeException(nameof(updateAlertModel.Status));
                }

                if (!Enum.TryParse<AlertFeedback>(updateAlertModel.Feedback, true, out var feedback))
                {
                    throw new ArgumentOutOfRangeException(nameof(updateAlertModel.Feedback));
                }

                UserAccountDevice userUpn = _memoryCacheHelper.GetUserAccountDevice(updateAlertModel.UserUpn);

                if (userUpn == null)
                {
                    userUpn = await _graphService.GetUserDetailsAsync(updateAlertModel.UserUpn, populatePicture: true, populateManager: true, populateDevices: true);
                    _memoryCacheHelper.SetUserAccountDevice(updateAlertModel.UserUpn, userUpn);
                }

                UserAccountDevice assignedTo = _memoryCacheHelper.GetUserAccountDevice(updateAlertModel.AssignedTo);

                if (assignedTo == null)
                {
                    assignedTo = await _graphService.GetUserDetailsAsync(updateAlertModel.AssignedTo, populatePicture: true, populateManager: true, populateDevices: true);
                    _memoryCacheHelper.SetUserAccountDevice(updateAlertModel.AssignedTo, assignedTo);
                }

                _demoExample.AddAlertHistoryState(id, new AlertHistoryState() { Status = status, Feedback = feedback, Comments = new List<string>() { updateAlertModel.Comments.Last() }, AssignedTo = assignedTo, UpdatedDateTime = DateTimeOffset.UtcNow, User = userUpn });

                restQueryBuilder.Append($" Request Body: {{ \"status\" = \"{updateAlertModel?.Status}\", {email} alert.Feedback = {updateAlertModel?.Feedback}; alert.Comments = {updateAlertModel?.Comments} ");

                var resultQueriesViewModel = new ResultQueriesViewModel(sdkQueryBuilder.ToString(), restQueryBuilder.ToString());

                var alert = await _graphService.GetAlertDetailsAsync(id);

                if (alert == null)
                {
                    return NotFound();
                }

                await _graphService.UpdateAlertAsync(alert, updateAlertModel);

                alert = await _graphService.GetAlertDetailsAsync(id);

                var alertModel = alert.ToAlertDetailsViewModel();

                await AddAdditionalInformationAboutAlert(alert, alertModel);

                //Only for demo
                if (alertModel.HistoryStates == null || alertModel.HistoryStates?.Count() == 0)
                {
                    alertModel.HistoryStates = _demoExample.GetAlertHistoryStates(alert, alertModel.AssignedTo, alertModel?.UserAccountDevices?.FirstOrDefault());
                }

                AlertDetailsResponse response = new AlertDetailsResponse(alertModel, resultQueriesViewModel);

                Debug.WriteLine($"Executing time AlertController UpdateAlert: {DateTime.Now - start}");
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        ///  Get the alerts based on filters
        /// </summary>
        /// <param name="alertFilter"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ResponseCache(CacheProfileName = "GetAlerts")]
        public async Task<ActionResult> GetAlerts([FromBody]AlertFilterViewModel viewAlertFilter)
        {
            try
            {
                var startGetAlerts = DateTime.Now;
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                AlertFilterModel filter = new AlertFilterModel(viewAlertFilter);

                var startGetAlertsfromGraph = DateTime.Now;

                var securityAlertsResult = await _graphService.GetAlertsAsync(filter);

                var filterQuery = securityAlertsResult?.Item2 ?? string.Empty;

                Debug.WriteLine($"Get Alerts from Graph: {DateTime.Now - startGetAlertsfromGraph}");

                // Generate queries
                var sdkQueryBuilder = new StringBuilder();
                var restQueryBuilder = new StringBuilder();
                sdkQueryBuilder.Append("await graphClient.Security.Alerts.Request()");
                if (!string.IsNullOrEmpty(filterQuery))
                {
                    sdkQueryBuilder.Append($".Filter(\"{filterQuery}\")");
                }
                sdkQueryBuilder.Append($".Top({viewAlertFilter.Top}).GetAsync()");

                if (!string.IsNullOrEmpty(filterQuery))
                {
                    restQueryBuilder.Append($"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=security/alerts?$filter={HttpUtility.UrlEncode(filterQuery)}%26$top={viewAlertFilter.Top}&&method=GET&version={_graphService.GraphUrlVersion}&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/{_graphService.GraphUrlVersion}/security/alerts?");

                    restQueryBuilder.Append($"$filter={HttpUtility.UrlEncode(filterQuery)}&");
                    restQueryBuilder.Append($"$top={viewAlertFilter.Top}</a>");
                }
                else
                {
                    restQueryBuilder.Append($"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=security/alerts?$top={viewAlertFilter.Top}&&method=GET&version={_graphService.GraphUrlVersion}&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/{_graphService.GraphUrlVersion}/security/alerts?");
                    restQueryBuilder.Append($"$top={viewAlertFilter.Top}</a>");
                }

                ResultQueriesViewModel resultQueriesViewModel = new ResultQueriesViewModel(sdkQueryBuilder.ToString(), restQueryBuilder.ToString());

                var alertSearchResult = securityAlertsResult?.Item1?.Select(sa => new AlertResultItemModel
                {
                    Id = sa.Id,
                    Title = sa.Title,
                    Status = sa.Status,
                    Provider = sa.VendorInformation?.Provider,
                    CreatedDateTime = sa.CreatedDateTime,
                    AssignedTo = sa.AssignedTo,
                    Severity = sa.Severity.ToString(),
                    Category = sa.Category
                }) ?? Enumerable.Empty<AlertResultItemModel>();

                var alertsResponse = new AlertsResponse(alertSearchResult, resultQueriesViewModel);

                Debug.WriteLine($"Executionf time AlertController GetAlerts: {DateTime.Now - startGetAlerts}");
                return Ok(alertsResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Gets the alert by alert id
        /// </summary>
        /// <param name="id">Id of the alert</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "GetAlertById")]
        public async Task<ActionResult> GetAlertById(string id)
        {
            try
            {
                var start = DateTime.Now;
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var startAll = DateTime.Now;
                var alert = await _graphService.GetAlertDetailsAsync(id);
                Debug.WriteLine($"Executing time GetAlertDetailsFromGraph: {DateTime.Now - startAll}");
                if (alert == null)
                {
                    return NotFound();
                }

                var sdkQueryBuilder = new StringBuilder();
                var restQueryBuilder = new StringBuilder();
                sdkQueryBuilder.Append($"await graphClient.Security.Alerts[\"{id}\"].Request().GetAsync()");

                restQueryBuilder.Append($"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=security/alerts/{id}&method=GET&version={_graphService.GraphUrlVersion}&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/{_graphService.GraphUrlVersion}/security/alerts/{id}/</a>");

                var alertModel = alert.ToAlertDetailsViewModel();

                await AddAdditionalInformationAboutAlert(alert, alertModel);

                //Only for demo
                if (alertModel.HistoryStates == null || alertModel.HistoryStates?.Count() == 0)
                {
                    alertModel.HistoryStates = _demoExample.GetAlertHistoryStates(alert, alertModel.AssignedTo, alertModel?.UserAccountDevices?.FirstOrDefault());
                }

                ResultQueriesViewModel resultQueriesViewModel = new ResultQueriesViewModel(sdkQueryBuilder.ToString(), restQueryBuilder.ToString());

                AlertDetailsResponse response = new AlertDetailsResponse(alertModel, resultQueriesViewModel);

                Debug.WriteLine($"Executing time AlertController GetAlertsById: {DateTime.Now - start}");
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        ///  Get the alerts based on single filter
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> GetAlertsByFilter([FromQuery]string key, [FromQuery]string value)
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                {
                    return BadRequest(new ArgumentNullException(value, "value and key can't be null"));
                }

                var viewAlertFilter = new AlertFilterViewModel { Top = 50, Filters = new AlertFilterCollection() };
                viewAlertFilter.Filters.Add(key, (new List<string>() { value }));

                var orderByOarams = new Dictionary<string, string>();

                switch (key)
                {
                    case "alert:severity":
                        {
                            orderByOarams.Add("createdDateTime", "desc");
                        }
                        break;

                    default:
                        {
                            orderByOarams.Add("severity", "desc");
                            orderByOarams.Add("createdDateTime", "desc");
                        }
                        break;
                }

                var filter = new AlertFilterModel(viewAlertFilter);
                var securityAlertsResult = await _graphService.GetAlertsAsync(filter, orderByOarams);
                var filterQuery = securityAlertsResult?.Item2 ?? string.Empty;

                // Generate queries
                var sdkQueryBuilder = new StringBuilder();
                var restQueryBuilder = new StringBuilder();
                sdkQueryBuilder.Append("await graphClient.Security.Alerts.Request()");
                if (!string.IsNullOrEmpty(filterQuery))
                {
                    sdkQueryBuilder.Append($".Filter(\"{filterQuery}\")");
                }

                sdkQueryBuilder.Append($".Top({filter.Top}).GetAsync()");

                if (!string.IsNullOrEmpty(filterQuery))
                {
                    restQueryBuilder.Append(
                        $"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=security/alerts?$filter={HttpUtility.UrlEncode(filterQuery)}%26$top={filter.Top}&&method=GET&version={_graphService.GraphUrlVersion}&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/{_graphService.GraphUrlVersion}/security/alerts?");

                    restQueryBuilder.Append($"$filter={HttpUtility.UrlEncode(filterQuery)}&");
                    restQueryBuilder.Append($"$top={filter.Top}</a>");
                }
                else
                {
                    restQueryBuilder.Append(
                        $"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=security/alerts?$top={filter.Top}&&method=GET&version={_graphService.GraphUrlVersion}&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/{_graphService.GraphUrlVersion}/security/alerts?");
                    restQueryBuilder.Append($"$top={filter.Top}</a>");
                }

                var alerts = securityAlertsResult?.Item1?.Select(sa => new AlertResultItemModel
                {
                    Id = sa.Id,
                    Title = sa.Title,
                    Status = sa.Status,
                    Provider = sa.VendorInformation?.Provider,
                    CreatedDateTime = sa.CreatedDateTime,
                    Severity = sa.Severity.ToString(),
                    Category = sa.Category
                }) ?? Enumerable.Empty<AlertResultItemModel>();

                // Save queries to session
                var queries = new ResultQueriesViewModel(sdkQueryBuilder.ToString(), restQueryBuilder.ToString());

                var alertsResponse = new AlertsResponse(alerts, queries);

                return Ok(alertsResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Gets Alert Filters
        /// </summary>
        /// <returns>Dictionary<string, IEnumerable<string>> of filters for Alerts</returns>
        [HttpGet("[action]")]
        [ResponseCache(CacheProfileName = "AlertsFilters")]
        public async Task<ActionResult> Filters()
        {
            try
            {
                var start = DateTime.Now;
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var filters = new Dictionary<string, IEnumerable<string>>();

                var statusesFilter = _alertService.GetStatuses();

                if (statusesFilter != null && statusesFilter.ContainsKey("AlertStatuses"))
                {
                    filters.Add("AlertStatuses", statusesFilter["AlertStatuses"]);
                }

                var severitiesFilter = _alertService.GetSeverities();

                if (severitiesFilter != null && severitiesFilter.ContainsKey("AlertSeverities"))
                {
                    filters.Add("AlertSeverities", severitiesFilter["AlertSeverities"]);
                }

                var feedbackFilter = _alertService.GetFeedbacks();

                if (feedbackFilter != null && feedbackFilter.ContainsKey("AlertFeedbacks"))
                {
                    filters.Add("AlertFeedbacks", feedbackFilter["AlertFeedbacks"]);
                }

                Dictionary<string, IEnumerable<string>> providerFilter = null;

                Dictionary<string, IEnumerable<string>> categoryFilter = null;

                if (!_demoExample.UseMockData)
                {
                    var alerts = await _graphService.GetAlertsAsync(new AlertFilterModel(200));

                    providerFilter = _graphService.GetProvidersFilter(alerts, "Alert");

                    categoryFilter = _graphService.GetCategoriesFilter(alerts);
                }
                else
                {
                    providerFilter = _graphService.GetProvidersFilter(_demoExample.GetProviders(), "Alert");

                    categoryFilter = _graphService.GetCategoriesFilter(_demoExample.GetCategories());

                }

                if (providerFilter != null && providerFilter.ContainsKey("AlertProviders"))
                {
                    filters.Add("AlertProviders", providerFilter["AlertProviders"]);
                }

                if (categoryFilter != null && categoryFilter.ContainsKey("AlertCategories"))
                {
                    filters.Add("AlertCategories", categoryFilter["AlertCategories"]);
                }
                Debug.WriteLine($"Executing time AlertController Filters: {DateTime.Now - start}");
                return Ok(filters);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Get Statistics
        /// </summary>
        /// <returns>Alert statistic model</returns>
        [HttpGet("[action]/{count}")]
        [ResponseCache(CacheProfileName = "Dashboard")]
        public async Task<ActionResult> Statistics(int count = 1000)
        {
            try
            {
                var start = DateTime.Now;
                var token = string.Empty;
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var statistic = await _graphService.GetStatisticAsync(count);

                var alertStatisticResponse = statistic.ToAlertStatisticResponse();

                Debug.WriteLine($"Executing time AlertController Statistics: {DateTime.Now - start}");
                return Ok(alertStatisticResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // Microsoft.Graph.Device
        private async Task AddAdditionalInformationAboutAlert(Alert alert, AlertDetailsViewModel alertModel)
        {
            try
            {
                var startAdd = DateTime.Now;

                if (!string.IsNullOrWhiteSpace(alert.AssignedTo))
                {
                    UserAccountDevice assignedTo = _memoryCacheHelper.GetUserAccountDevice(alert.AssignedTo);

                    if (assignedTo == null)
                    {
                        assignedTo = await _graphService.GetUserDetailsAsync(alert.AssignedTo, populatePicture: true, populateManager: true, populateDevices: true);
                        alertModel.AssignedTo = assignedTo;
                        _memoryCacheHelper.SetUserAccountDevice(alert.AssignedTo, assignedTo);
                    }

                    alertModel.AssignedTo = assignedTo;
                }

                List<UserAccountDevice> userAccountDevices = new List<UserAccountDevice>();

                foreach (var userState in alert?.UserStates)
                {
                    // Get info about user
                    var principalName = userState.UserPrincipalName;
                    if (!string.IsNullOrWhiteSpace(principalName))
                    {
                        UserAccountDevice userAccountDevice = _memoryCacheHelper.GetUserAccountDevice(principalName);

                        if (userAccountDevice == null)
                        {
                            userAccountDevice = await _graphService.GetUserDetailsAsync(principalName, populatePicture: true, populateManager: true, populateDevices: true, populateRiskyUser: true);

                            if (!string.IsNullOrWhiteSpace(userAccountDevice.Manager.Upn))
                            {
                                userAccountDevice.Manager = await _graphService.GetUserDetailsAsync(userAccountDevice.Manager.Upn, populatePicture: false, populateManager: false, populateDevices: false);
                            }

                            if (!string.IsNullOrWhiteSpace(userState.DomainName))
                            {
                                userAccountDevice.DomainName = userState.DomainName;
                            }

                            userAccountDevice.RiskScore = userState?.RiskScore;
                            userAccountDevice.LogonId = userState?.LogonId;
                            userAccountDevice.EmailRole = userState?.EmailRole.ToString();

                            if (userAccountDevice?.RegisteredDevices?.Count() == 0 && userAccountDevice?.OwnedDevices?.Count() == 0)
                            {
                                userAccountDevice.RegisteredDevices = _demoExample.GetDevices();
                                userAccountDevice.OwnedDevices = _demoExample.GetDevices();
                            }
                            _memoryCacheHelper.SetUserAccountDevice(principalName, userAccountDevice);
                        }
                        if (userAccountDevice != null)
                        {
                            userAccountDevices.Add(userAccountDevice);
                        }
                    }
                }

                alertModel.UserAccountDevices = userAccountDevices;

                Debug.WriteLine($"Executing time AddAdditionalInformationAboutAlert: {DateTime.Now - startAdd}");
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }
        }
    }
}