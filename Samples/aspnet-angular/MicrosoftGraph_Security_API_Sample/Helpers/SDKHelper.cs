// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using System.Net.Http.Headers;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public class SDKHelper
    {
        private static GraphServiceClient graphClient = null;

        // Get an authenticated Microsoft Graph Service client.
        public static GraphServiceClient GetAuthenticatedClient(AzureConfiguration azureConfiguration, string jwtToken)
        {
            try
            {
                graphClient = new GraphServiceClient(
                    new DelegateAuthenticationProvider(
                        async (requestMessage) =>
                        {
                            SampleAuthProvider sampleAuthProvider = new SampleAuthProvider();

                            var accessToken = await sampleAuthProvider.GetUserAccessTokenAsync(azureConfiguration, jwtToken);
                            // Append the access token to the request.
                            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                            // This header has been added to identify our sample in the Microsoft Graph service. If extracting this code for your project please remove.
                            requestMessage.Headers.Add("SampleID", "aspnet-connect-sample");
                            //});
                        }));

                return graphClient;
            }
            catch
            {
                throw;
            }
        }

        public static string GetAccessToken(AzureConfiguration azureConfiguration, string jwtToken)
        {
            SampleAuthProvider sampleAuthProvider = new SampleAuthProvider();

            return sampleAuthProvider.GetUserAccessTokenAsync(azureConfiguration, jwtToken).Result;
        }

        public static void SignOutClient()
        {
            graphClient = null;
        }
    }
}