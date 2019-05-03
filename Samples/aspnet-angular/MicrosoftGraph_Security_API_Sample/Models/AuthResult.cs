// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models
{
    /// <summary>
    /// Class to carrry result from authentication
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Gets or sets the access token retrieved from AAD
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the exceptions happen during authentication
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the list of logs during authentication
        /// </summary>
        public List<string> Logs { get; set; } = new List<string>();
    }
}