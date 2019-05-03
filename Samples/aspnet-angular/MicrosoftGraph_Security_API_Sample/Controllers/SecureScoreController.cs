// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Authorize]
    [Route("api/securescores")]
    public class SecureScoreController : Controller
    {
        private readonly IGraphServiceProvider _graphServiceProvider;

        private IGraphService _graphService;

        private IMemoryCacheHelper _memoryCacheHelper;

        public SecureScoreController(IGraphServiceProvider graphServiceProvider, IMemoryCacheHelper memoryCacheHelper)
        {
            _graphServiceProvider = graphServiceProvider;
            _memoryCacheHelper = memoryCacheHelper;
        }

        /// <summary>
        /// List existing secure scores
        /// Secure scores is still in Beta. So This sample uses REST queries to get the secure scores, since the official SDK is only available for workloads in V1.0
        /// </summary>
        /// <returns>List of secure scores</returns>
        [HttpGet("")]
        [ResponseCache(CacheProfileName = "GetSecureScores")]
        public async Task<ActionResult> GetSecureScores(string queryParameter)
        {
            var startDateTime = DateTime.Now;
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var securityScores = await _graphService.GetSecureScoresAsync(queryParameter);
                Debug.WriteLine($"Secure Score Controller GetSecureScores execution time: {DateTime.Now - startDateTime}");
                return Ok(securityScores);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Gets secure scores
        /// </summary>
        /// <returns>List of secure scores</returns>
        [HttpGet("[action]")]
        [ResponseCache(CacheProfileName = "GetSecureDetails")]
        public async Task<ActionResult> GetSecureDetails()
        {
            var startDateTime = DateTime.Now;
            try
            {
                var token = string.Empty;
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var secureScores = _memoryCacheHelper.GetSecureScores("?$top=200");

                if (secureScores == null || secureScores?.ToList().Count() == 0)
                {
                    //// Get top secure score
                    secureScores = await _graphService.GetSecureScoresAsync("?$top=200");
                    _memoryCacheHelper.SetSecureScores("?$top=200", secureScores);
                }

                if (secureScores == null || secureScores?.ToList().Count == 0)
                {
                    return NotFound();
                }

                var topSecureScore = secureScores.FirstOrDefault();

                var controlProfiles = _memoryCacheHelper.GetSecureScoreControlProfiles();

                if (controlProfiles == null || controlProfiles?.ToList().Count() == 0)
                {
                    //// Get top secure score
                    controlProfiles = await _graphService.GetSecureScoreControlProfilesAsync();

                    _memoryCacheHelper.SetSecureScoreControlProfiles(controlProfiles);
                }

                var restQueryBuilder = new StringBuilder();

                restQueryBuilder.Append($"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=security/securescores?$top=1&&method=GET&version=beta&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/beta/security/securescores?$top=1</a>");

                var secureScoreDetails = new SecureScoreDetailsViewModel
                {
                    TopSecureScore = new SecureScoreModel
                    {
                        CreatedDateTime = topSecureScore.CreatedDateTime,
                        CurrentScore = topSecureScore.CurrentScore,
                        MaxScore = topSecureScore.MaxScore,
                        EnabledServices = topSecureScore.EnabledServices.Select(service => service.StartsWith("Has", StringComparison.CurrentCultureIgnoreCase) ? service.Substring(3) : service),
                        LicensedUserCount = topSecureScore.LicensedUserCount ?? 0,
                        ActiveUserCount = topSecureScore.ActiveUserCount ?? 0,
                        Id = topSecureScore.Id,
                        ControlScores = (topSecureScore.ControlScores?.Select(controlScore => new ControlScoreModel
                        {
                            ControlCategory = controlScore.ControlCategory,
                            Score = controlScore.Score ?? 0,
                            Name = controlScore.ControlName,
                            Description = controlScore.Description,
                            Count = controlScore.AdditionalData.ContainsKey("count")
                                ? controlScore.AdditionalData["count"].ToString()
                                : controlScore.AdditionalData.ContainsKey("on")
                                    ? controlScore.AdditionalData["on"].ToString()
                                    : "0",
                            Total = controlScore.AdditionalData.ContainsKey("total")
                                ? controlScore.AdditionalData["total"].ToString()
                                : controlScore.AdditionalData.ContainsKey("IntuneOn")
                                    ? controlScore.AdditionalData["IntuneOn"].ToString()
                                    : "0",
                        }) ?? Enumerable.Empty<ControlScoreModel>())
                            .GroupBy(controlScoreModel=>controlScoreModel.ControlCategory)
                            .Select(groupControlScore => new ControlScoreModel
                            {
                                ControlCategory = groupControlScore.Key.ToString(),
                                Score = groupControlScore.Sum(x => x.Score),
                                Description = groupControlScore.FirstOrDefault().Description,
                                Name = groupControlScore.FirstOrDefault().Name,
                                Count = groupControlScore.Count().ToString(),

                                // it was in version 2
                                // Total = groupControlScore.Sum(x => x.Total.AsEnumerable().Any(ch => char.IsLetter(ch)) ? 0 : Convert.ToInt32(x.Total)).ToString(),

                                // it was in version 2
                                // Count = groupControlScore.Sum(x=> x.Count.AsEnumerable().Any(ch => char.IsLetter(ch)) ? 0 : Convert.ToInt32(x.Count)).ToString(),
                            }).ToList(),
                        AverageComparativeScores = topSecureScore.AverageComparativeScores?.Select(score =>
                        {
                            string Value = string.Empty;
                            switch (score.Basis)
                            {
                                case "Industry":
                                    {
                                        if (score.AdditionalData.ContainsKey("industryName"))
                                        {
                                            Value = score.AdditionalData["industryName"].ToString();
                                        }
                                    }
                                    break;

                                case "SeatCategory":
                                    {
                                        if (score.AdditionalData.ContainsKey("SeatLowerLimit") && score.AdditionalData.ContainsKey("SeatUpperLimit"))
                                        {
                                            Value = $"{score.AdditionalData["SeatLowerLimit"]} - {score.AdditionalData["SeatUpperLimit"]}";
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        Value = string.Join(", ", score.AdditionalData.Select(item => item.Value));
                                    }
                                    break;
                            }

                            return new AverageComparativeScoreModel
                            {
                                ComparedBy = score.Basis,
                                // if null or negative AverageScore set 0.0
                                AverageScore = score.AverageScore.HasValue 
                                    ? score.AverageScore.Value > 0 
                                        ? score.AverageScore.Value 
                                        : 0.0 
                                    : 0.0,
                                Value = Value
                            };
                        }) ?? Enumerable.Empty<AverageComparativeScoreModel>()
                    },
                    SecureScoreControlProfiles = controlProfiles?.Select(profile => profile) ?? Enumerable.Empty<SecureScoreControlProfileModel>()
                };

                // Save queries to session
                var resultQueries = new ResultQueriesViewModel(null, restQueryBuilder.ToString());

                var response = new SecureScoreDetailsResponse(secureScoreDetails, resultQueries);

                var responseString = JsonConvert.SerializeObject(
                    response,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                Debug.WriteLine($"Secure Score Controller GetSecureDetails execution time: {DateTime.Now - startDateTime}");
                return new JsonStringResult(responseString);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}