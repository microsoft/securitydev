// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;

namespace MicrosoftGraph_Security_API_Sample.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static void AddCachingsProfiles(this MvcOptions mvcOptions, CacheTime cacheTime)
        {
            mvcOptions.CacheProfiles.Add("GetActions", new CacheProfile() { Duration = cacheTime.GetActions });
            mvcOptions.CacheProfiles.Add("ActionsFilters", new CacheProfile() { Duration = cacheTime.ActionsFilters });
            mvcOptions.CacheProfiles.Add("GetAlerts", new CacheProfile() { Duration = cacheTime.GetAlerts });
            mvcOptions.CacheProfiles.Add("GetAlertById", new CacheProfile() { Duration = cacheTime.GetAlertById });
            mvcOptions.CacheProfiles.Add("AlertsFilters", new CacheProfile() { Duration = cacheTime.AlertsFilters });
            mvcOptions.CacheProfiles.Add("Dashboard", new CacheProfile() { Duration = cacheTime.Dashboard });
            mvcOptions.CacheProfiles.Add("GetSecureScores", new CacheProfile() { Duration = cacheTime.GetSecureScores });
            mvcOptions.CacheProfiles.Add("GetSecureDetails", new CacheProfile() { Duration = cacheTime.GetSecureDetails });
            mvcOptions.CacheProfiles.Add("GetSubscriptions", new CacheProfile() { Duration = cacheTime.GetSubscriptions });
        }
    }
}
