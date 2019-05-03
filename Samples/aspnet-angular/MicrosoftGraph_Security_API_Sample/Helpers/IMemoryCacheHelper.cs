// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using MicrosoftGraph_Security_API_Sample.Models;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public interface IMemoryCacheHelper
    {
        UserAccountDevice GetUserAccountDevice(string userUpn);

        void SetUserAccountDevice(string userUpn, UserAccountDevice userAccountDevice);

        List<string> GetFilters(string name);

        void SetFilter(string name, List<string> filters);

        Task<bool> Initialization(IGraphService graphService);

        void SetSecureScores(string filter, IEnumerable<SecureScore> secureScores);

        IEnumerable<SecureScore> GetSecureScores(string filter);

        void SetSecureScoreControlProfiles(IEnumerable<SecureScoreControlProfileModel> secureScoresControlProfile);

        IEnumerable<SecureScoreControlProfileModel> GetSecureScoreControlProfiles();
    }
}
