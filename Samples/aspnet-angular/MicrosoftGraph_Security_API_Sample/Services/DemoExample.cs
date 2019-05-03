// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KeyValuePair = Microsoft.Graph.KeyValuePair;

namespace MicrosoftGraph_Security_API_Sample.Services
{
    public class DemoExample : IDemoExample
    {
        private MockData mockData = new MockData();

        public bool UseMockData { get; set; } = false;

        public DemoExample(bool useMockData)
        {
            try
            {
                UseMockData = useMockData;
                using (StreamReader r = new StreamReader("mockData.json"))
                {
                    string json = r.ReadToEnd();

                    JObject jsonObj = JObject.Parse(json);

                    foreach (var obj in jsonObj)
                    {
                        if (obj.Key.Equals("securityActions"))
                        {
                            mockData.SecurityActions = obj.Value.ToObject<List<SecurityAction>>();
                        }

                        if (obj.Key.Equals("devices"))
                        {
                            mockData.Devices = obj.Value.ToObject<List<Device>>();
                        }

                        if (obj.Key.Equals("actionFilters"))
                        {
                            mockData.ActionFilters = obj.Value.ToObject<ActionFilters>();
                        }

                        if (obj.Key.Equals("providers"))
                        {
                            mockData.Providers = obj.Value.ToObject<List<string>>();
                        }

                        if (obj.Key.Equals("categories"))
                        {
                            mockData.Categories = obj.Value.ToObject<List<string>>();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                mockData = new MockData()
                {
                    ActionFilters = new ActionFilters()
                    {
                        ActionNames = new List<string>() { "Block", "Allow" },
                        ActionTargets = new List<string>() { "destinationAddress", "destinationDomain", "destinationUrl", "destinationPort" },
                    },
                    SecurityActions = new List<SecurityAction>()
                    {
                        new SecurityAction(){ Name = "Block", CreatedDateTime = DateTimeOffset.Now.AddHours(-20), LastActionDateTime = DateTimeOffset.Now.AddHours(-19), ActionReason = "Received TI this is a suspected malicious command and control center",  Parameters = new List<KeyValuePair>(){ new KeyValuePair() { Name = "AlertId", Value = "53C7E3B0-8470-424C-BD40-2C9C3F9EFB81" }, new KeyValuePair() { Name = "BlockName", Value = "destinationAddress" },new KeyValuePair() { Name = "BlockValue", Value = "54.243.235.218" }}, Status = OperationStatus.Completed, Id = "1", VendorInformation = new Microsoft.Graph.SecurityVendorInformation(){ Provider = "Illumio VEN", Vendor = "Illumio", ProviderVersion = "18.02" }},
                        new SecurityAction(){ Name = "Block", CreatedDateTime = DateTimeOffset.Now.AddHours(-23), LastActionDateTime = DateTimeOffset.Now.AddHours(-22), ActionReason = "Received TI this is a suspected malicious command and control center",  Parameters = new List<KeyValuePair>(){ new KeyValuePair() { Name = "AlertId", Value = "53C7E3B0-8470-424C-BD40-2C9C3F9EFB81" }, new KeyValuePair() { Name = "BlockName", Value = "destinationAddress" },new KeyValuePair() { Name = "BlockValue", Value = "54.243.235.218" }}, Status = OperationStatus.Completed, Id = "2", VendorInformation = new Microsoft.Graph.SecurityVendorInformation(){ Provider = "Illumio VEN", Vendor = "Illumio", ProviderVersion = "18.02" }},
                        new SecurityAction(){ Name = "Block", CreatedDateTime = DateTimeOffset.Now.AddHours(-26), LastActionDateTime = DateTimeOffset.Now.AddHours(-25), ActionReason = "Received TI this is a suspected malicious command and control center",  Parameters = new List<KeyValuePair>(){ new KeyValuePair() { Name = "AlertId", Value = "53C7E3B0-8470-424C-BD40-2C9C3F9EFB81" }, new KeyValuePair() { Name = "BlockName", Value = "destinationDomain" }, new KeyValuePair() { Name = "BlockValue", Value = "willaimsclarke.com" } }, Status = OperationStatus.Completed, Id = "3", VendorInformation = new Microsoft.Graph.SecurityVendorInformation(){ Provider = "Illumio VEN", Vendor = "Illumio", ProviderVersion = "18.02" }},
                    },
                    Devices = new List<Device>()
                    {
                        new Device()
                        {
                            Id = "35888b53-23fd-4830-b526-d0df7ce8644e",
                            DisplayName = "Microsoft Lumia 950 XL",
                            OperatingSystem = "Windows 10 Mobile",
                            OperatingSystemVersion = "10.2.5.75",
                            IsManaged = true,
                            AccountEnabled = true,
                            ApproximateLastSignInDateTime = DateTime.Now,
                            DeviceId = "35888b53-23fd-4830-b526-d0df7ce8644e",
                            DeviceMetadata = "/metadata#device",
                            IsCompliant = true,
                            DeviceVersion = 3,
                            TrustType = "Workspace"
                        }
                    }
                };
            }
        }

        private Dictionary<string, List<AlertHistoryState>> alertHistoryStatesDictionary = new Dictionary<string, List<AlertHistoryState>>();

        public List<AlertHistoryState> GetAlertHistoryStates(Alert alert, UserAccountDevice assignedTo, UserAccountDevice user)
        {
            try
            {
                if (alertHistoryStatesDictionary.ContainsKey(alert.Id))
                {
                    return alertHistoryStatesDictionary[alert.Id];
                }

                if (user == null || assignedTo == null)
                    return new List<AlertHistoryState>();

                return new List<AlertHistoryState>(){
                    new AlertHistoryState()
                    {
                        AssignedTo = assignedTo,
                        Feedback = alert.Feedback,
                        Status = alert.Status,
                        UpdatedDateTime = alert?.LastModifiedDateTime,
                        User = user,
                        Comments = alert?.Comments
                    } };
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return new List<AlertHistoryState>();
            }
        }

        public List<AlertHistoryState> AddAlertHistoryState(string alertId, AlertHistoryState alertHistoryState)
        {
            if (alertHistoryStatesDictionary.ContainsKey(alertId))
            {
                List<AlertHistoryState> alertHistoryStatesFromDictionary = alertHistoryStatesDictionary[alertId];
                alertHistoryStatesFromDictionary.Add(alertHistoryState);
                alertHistoryStatesDictionary[alertId] = alertHistoryStatesFromDictionary;
                return alertHistoryStatesDictionary[alertId];
            }

            List<AlertHistoryState> alertHistoryStates = new List<AlertHistoryState>() { alertHistoryState };
            alertHistoryStatesDictionary.Add(alertId, alertHistoryStates);
            return alertHistoryStatesDictionary[alertId];
        }

        public async Task<IEnumerable<SecurityActionResponse>> GetSecurityActionsAsync()
        {
            return await Task.Run(() =>
            {
                var responses = mockData.SecurityActions.ToSecurityActionResponses();
                responses.ToList().OrderByDescending(x => x.StatusUpdateDateTime);
                return responses;
            });
        }

        public async Task<IEnumerable<SecurityActionResponse>> AddSecurityActionsAsync(SecurityAction action)
        {
            return await Task.Run(() =>
            {
                mockData.SecurityActions.Add(action);
                var responses = mockData.SecurityActions.ToSecurityActionResponses();
                responses.ToList().OrderByDescending(x => x.StatusUpdateDateTime);
                return responses;
            });
        }

        public IEnumerable<Device> GetDevices()
        {
            return mockData.Devices;
        }

        public IEnumerable<string> GetProviders()
        {
            return mockData.Providers;
        }

        public IEnumerable<string> GetCategories()
        {
            return mockData.Categories;
        }

        public Dictionary<string, IEnumerable<string>> GetActionFilters()
        {
            Dictionary<string, IEnumerable<string>> filters = new Dictionary<string, IEnumerable<string>>
            {
                { "actionNames", mockData.ActionFilters.ActionNames },
                { "actionTargets", mockData.ActionFilters.ActionTargets }
            };
            return filters;
        }
    }
}