// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Caching.Memory;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public class TokenProvider : ITokenProvider
    {
        public string GetGraphTokenByIdToken(string idToken, IMemoryCache cache)
        {
            try
            {
                if (idToken.StartsWith("Bearer"))
                {
                    idToken = idToken.Split(" ")?[1];
                }

                if (!string.IsNullOrWhiteSpace(idToken))
                {
                    string token = cache.Get<string>(idToken);

                    return token;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}