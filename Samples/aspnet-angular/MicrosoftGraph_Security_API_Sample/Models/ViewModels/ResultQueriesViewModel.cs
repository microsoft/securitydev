// -----------------------------------------------------------------------
// <copyright file="ResultQueriesViewModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Web;

namespace MicrosoftGraph_Security_API_Sample.Models.ViewModels
{
    public class ResultQueriesViewModel
    {
        public ResultQueriesViewModel(string sdkQuery, string restQuery)
        {
            SDKQuery = HttpUtility.HtmlEncode(sdkQuery);
            RESTQuery = HttpUtility.HtmlEncode(restQuery);
        }

        public string SDKQuery { get; set; }

        public string RESTQuery { get; set; }
    }
}