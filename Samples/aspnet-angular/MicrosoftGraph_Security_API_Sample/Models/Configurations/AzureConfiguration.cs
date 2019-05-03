//// -----------------------------------------------------------------------
//// <copyright file="NotificationController.cs" company="Microsoft Corporation">
////     Copyright (c) Microsoft Corporation. All rights reserved.
//// </copyright>
//// -----------------------------------------------------------------------

namespace MicrosoftGraph_Security_API_Sample.Models.Configurations
{
    /// <summary>
    /// Settings relative to the AzureAD applications involved in this Web Application
    /// These are deserialized from the AzureAD section of the appsettings.json file
    /// </summary>
    public class AzureConfiguration
    {
        public string Audience { get; set; }

        /// <summary>
        /// ClientId (Application Id) of this Web Application
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// This is scope for Microsoft Graph
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Client Secret (Application password) added in the Azure portal in the Keys section for the application
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// This is the Redirect uri that is specified in the application authorization settings.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Notification Uri is uri in Web API where will notifications come
        /// </summary>
        public string NotificationUri { get; set; }

        /// <summary>
        /// Microsoft Graph Base Url
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Microsoft Graph Url version
        /// </summary>
        public string UrlVersion { get; set; }

        /// <summary>
        /// Azure AD Cloud instance
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Instance of the settings for this Web application (to be used in controllers)
        /// </summary>
        public static AzureConfiguration Settings { set; get; }
    }
}