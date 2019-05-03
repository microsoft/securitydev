// -----------------------------------------------------------------------
// <copyright file="SecureScoreDetailsViewModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.ViewModels
{
    public class SecureScoreDetailsViewModel
    {
        public SecureScoreModel TopSecureScore { get; set; }

        public IEnumerable<SecureScoreControlProfileModel> SecureScoreControlProfiles { get; set; }
    }
}