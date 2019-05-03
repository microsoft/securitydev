// -----------------------------------------------------------------------
// <copyright file="UpdateAlertResultModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MicrosoftGraph_Security_API_Sample.Models
{
    public class UpdateAlertResultModel
    {
        public string Error { get; set; }

        public string Id { get; set; }

        public UpdateAlertResultItemModel Before { get; set; }

        public UpdateAlertResultItemModel After { get; set; }
    }
}