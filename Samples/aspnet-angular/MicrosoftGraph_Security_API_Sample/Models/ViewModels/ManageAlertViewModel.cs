// -----------------------------------------------------------------------
// <copyright file="ManageAlertViewModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MicrosoftGraph_Security_API_Sample.Models.ViewModels
{
    public class ManageAlertViewModel
    {
        public AlertDetailsViewModel CurrentAlert { get; set; }

        public UpdateAlertResultModel UpdateAlertResult { get; set; }
    }
}