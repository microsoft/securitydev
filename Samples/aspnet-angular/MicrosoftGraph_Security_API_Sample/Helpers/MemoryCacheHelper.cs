using System;
using System.Collections.Generic;
// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public class MemoryCacheHelper : IMemoryCacheHelper
    {
        private IMemoryCache _cache;
        private IDemoExample _demoExample;

        public MemoryCacheHelper(IMemoryCache memoryCache, IDemoExample demoExample)
        {
            _cache = memoryCache;
            _demoExample = demoExample;
        }
        
        public UserAccountDevice GetUserAccountDevice(string userUpn)
        {
            _cache.TryGetValue(userUpn, out UserAccountDevice userAccountDevice);

            return userAccountDevice;
        }

        public void SetUserAccountDevice(string userUpn, UserAccountDevice userAccountDevice)
        {
            _cache.Set(userUpn, userAccountDevice);
        }

        public IEnumerable<SecureScoreControlProfileModel> GetSecureScoreControlProfiles()
        {
            _cache.TryGetValue($"SecureScoreControlProfiles", out IEnumerable<SecureScoreControlProfileModel> secureScoresControlProfile);

            return secureScoresControlProfile;
        }

        public void SetSecureScoreControlProfiles(IEnumerable<SecureScoreControlProfileModel> secureScoresControlProfile)
        {
            _cache.Set($"SecureScoreControlProfiles", secureScoresControlProfile);
        }

        public IEnumerable<SecureScore> GetSecureScores(string filter)
        {
            _cache.TryGetValue($"SecureScore{filter}", out IEnumerable<SecureScore> secureScores);

            return secureScores;
        }

        public void SetSecureScores(string filter, IEnumerable<SecureScore> secureScores)
        {
            _cache.Set($"SecureScore{filter}", secureScores);
        }

        public void SetFilter(string name, List<string> filters)
        {
            _cache.Set(name, filters);
        }

        public List<string> GetFilters(string name)
        {
            _cache.TryGetValue(name, out List<string> filters);
            return filters;
        }

        public async Task<bool> Initialization(IGraphService graphService)
        {
            try
            {
                var categories = new List<string>();
                var providers = new List<string>();
                var alerts = await graphService.GetAlertsAsync(new AlertFilterModel(1000));
                foreach (var alert in alerts?.Item1)
                {
                    if (!string.IsNullOrWhiteSpace(alert.AssignedTo))
                    {
                        if (GetUserAccountDevice(alert.AssignedTo) == null)
                        {
                            UserAccountDevice userAccountDevice = await graphService.GetUserDetailsAsync(alert.AssignedTo, true, true, true);
                            SetUserAccountDevice(alert.AssignedTo, userAccountDevice);
                        }
                    }

                    if (alert.UserStates != null)
                    {
                        foreach (var userState in alert.UserStates)
                        {
                            if (!string.IsNullOrWhiteSpace(userState.UserPrincipalName) && GetUserAccountDevice(userState.UserPrincipalName) == null)
                            {
                                UserAccountDevice userAccountDevice = await graphService.GetUserDetailsAsync(userState.UserPrincipalName, populatePicture: true, populateManager: true, populateDevices: true);

                                if (!string.IsNullOrWhiteSpace(userAccountDevice.Manager.Upn))
                                {
                                    userAccountDevice.Manager = await graphService.GetUserDetailsAsync(userAccountDevice.Manager.Upn, populatePicture: false, populateManager: false, populateDevices: false);
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

                                if (userAccountDevice != null)
                                {
                                    SetUserAccountDevice(userState.UserPrincipalName, userAccountDevice);
                                }
                                
                            }
                        }
                    }

                    if (alert.VendorInformation != null && !string.IsNullOrWhiteSpace(alert.VendorInformation.Provider))
                    {
                        providers.Add(alert.VendorInformation.Provider);
                    }

                    if (!string.IsNullOrWhiteSpace(alert.Category))
                    {
                        categories.Add(alert.Category);
                    } 
                }

                if (categories.Count > 0)
                {
                    SetFilter("Categories", categories.Distinct().ToList());
                }

                if (providers.Count > 0)
                {
                    SetFilter("Providers", providers.Distinct().ToList());
                }

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
