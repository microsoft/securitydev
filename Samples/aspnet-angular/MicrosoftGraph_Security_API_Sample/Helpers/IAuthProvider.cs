// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public interface IAuthProvider
    {
        Task<string> GetUserAccessTokenAsync(AzureConfiguration azureConfiguration, string jwtToken);
    }
}