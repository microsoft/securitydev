// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Caching.Memory;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public interface ITokenProvider
    {
        string GetGraphTokenByIdToken(string idToken, IMemoryCache cache);
    }
}