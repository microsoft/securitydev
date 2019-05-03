// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public class JsonStringResult : ContentResult
    {
        public JsonStringResult(string json)
        {
            Content = json;
            ContentType = "application/json";
        }
    }
}