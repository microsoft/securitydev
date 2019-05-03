// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using MicrosoftGraph_Security_API_Sample.Services;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public class GraphServiceProvider : IGraphServiceProvider
    {
        private readonly string _urlVersion;
        private readonly string _baseUrl;
        private readonly string _notificationUri;
        private readonly AzureConfiguration _azureConfiguration;

        public GraphServiceProvider(AzureConfiguration azureConfiguration)
        {
            _urlVersion = azureConfiguration.UrlVersion;
            _baseUrl = azureConfiguration.BaseUrl;
            _notificationUri = azureConfiguration.NotificationUri;
            _azureConfiguration = azureConfiguration;
        }

        public IGraphService GetService(string token)
        {
            return new GraphService(_azureConfiguration, token);
        }
    }
}