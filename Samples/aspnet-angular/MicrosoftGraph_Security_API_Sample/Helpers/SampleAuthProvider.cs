// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Identity.Client;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public sealed class SampleAuthProvider : IAuthProvider
    {
        // Properties used to get and manage an access token.
        private static readonly SampleAuthProvider SampleInstance = new SampleAuthProvider();

        public SampleAuthProvider()
        {
        }

        private SessionTokenCache TokenCache { get; set; }

        public SampleAuthProvider Instance
        {
            get
            {
                return SampleInstance;
            }
        }

        // Gets an access token. First tries to get the token from the token cache.
        public async Task<string> GetUserAccessTokenAsync(AzureConfiguration azureConfiguration, string jwtToken)
        {
            try
            {
                ConfidentialClientApplication cca = new ConfidentialClientApplication(
                azureConfiguration.ClientId,
                azureConfiguration.RedirectUri,
                new ClientCredential(azureConfiguration.ClientSecret),
                null,
                null);

                AuthenticationResult result = await cca.AcquireTokenOnBehalfOfAsync(azureConfiguration.Scope.Split(" "), new UserAssertion(jwtToken));
                return result.AccessToken;
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                return null;
            }
        }
    }
}