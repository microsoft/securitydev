// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Authorize]
    [Route("api/subscriptions")]
    public class SubscriptionController : Controller
    {
        private readonly IGraphServiceProvider _graphServiceProvider;

        private readonly ITokenProvider _tokenProvider;

        private readonly IMemoryCache _cache;

        private IGraphService _graphService;

        public SubscriptionController(IGraphServiceProvider graphServiceProvider, ITokenProvider tokenProvider, IMemoryCache cache)
        {
            _graphServiceProvider = graphServiceProvider;
            _tokenProvider = tokenProvider;
            _cache = cache;
        }

        /// <summary>
        /// Gets subscriptions
        /// </summary>
        /// <returns>List of subscriptions with queries</returns>
        [HttpGet("")]
        [ResponseCache(CacheProfileName = "GetSubscriptions")]
        public async Task<ActionResult> GetSubscriptions()
        {
            try
            {
                var startDateTime = DateTime.Now;
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                IGraphServiceSubscriptionsCollectionPage subscriptions = await _graphService.GetListSubscriptionsAsync();

                var sdkQueryBuilder = new StringBuilder();
                var restQueryBuilder = new StringBuilder();

                sdkQueryBuilder.Append("graphClient.Subscriptions.Request().GetAsync()");

                restQueryBuilder.Append($"<a href=\"https://developer.microsoft.com/en-us/graph/graph-explorer?request=subscriptions&&method=GET&version={_graphService.GraphUrlVersion}&GraphUrl=https://graph.microsoft.com\" target=\"_blank\">https://graph.microsoft.com/{_graphService.GraphUrlVersion}/subscriptions</a>");

                var subscriptionResultModels = subscriptions?.ToList()?.Select(sa => new SubscriptionResultModel
                {
                    Id = sa.Id,
                    Resource = sa.Resource,
                    ExpirationDateTime = sa.ExpirationDateTime,
                    ClientState = sa.ClientState,
                    NotificationUrl = sa.NotificationUrl
                }) ?? Enumerable.Empty<SubscriptionResultModel>();

                // Save queries to session
                var resultQueries = new ResultQueriesViewModel(sdkQueryBuilder.ToString(), restQueryBuilder.ToString());

                SubscriptionResponse subscriptionResponse = new SubscriptionResponse(subscriptionResultModels, resultQueries);

                Debug.WriteLine($"SubscriptionController GetSubscriptions execution time: {DateTime.Now - startDateTime}");
                return Ok(subscriptionResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Create the subscription based on the subscription filters
        /// </summary>
        /// <param name="subscriptionFilters"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> Subscribe(AlertFilterViewModel actViewAlertFilter)
        {
            try
            {
                var startDateTime = DateTime.Now;
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                if (actViewAlertFilter != null && actViewAlertFilter.Filters.GetFilterValue("alert:category").Equals("Any")
                    && actViewAlertFilter.Filters.GetFilterValue("vendor:provider").Equals("Any")
                    && actViewAlertFilter.Filters.GetFilterValue("alert:severity").Equals("Any"))
                {
                    return BadRequest("Please select at least one property/criterion for subscribing to alert notifications");
                }
                else
                {
                    var filter = new AlertFilterModel(actViewAlertFilter);
                    var createSubscriptionResult = await _graphService.SubscribeAsync(filter);
                    var subscription = createSubscriptionResult.Item1;
                    Debug.WriteLine($"SubscriptionController Subscribe execution time: {DateTime.Now - startDateTime}");
                    return Ok(subscription);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}