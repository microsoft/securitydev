// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.Requests;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Authorize]
    [Route("api/actions")]
    public class ActionController : Controller
    {
        private IDemoExample _demoExample;

        private IActionService _actionService;

        private readonly IGraphServiceProvider _graphServiceProvider;

        private IGraphService _graphService;

        public ActionController(IGraphServiceProvider graphServiceProvider, IDemoExample demoExample, IActionService actionService)
        {
            _graphServiceProvider = graphServiceProvider;
            _actionService = actionService;
            _demoExample = demoExample;
        }

        /// <summary>
        ///  Get the alerts based on filters
        /// </summary>
        /// <param name="alertFilter"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ResponseCache(CacheProfileName = "GetActions")]
        public async Task<ActionResult> GetActions()
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

                var securityActions = await _graphService.GetSecurityActionsAsync();
                return Ok(securityActions);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        ///  Get the alerts based on filters
        /// </summary>
        /// <param name="alertFilter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> AddAction([FromBody]SecurityActionRequest securityActionRequest)
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);
                var securityActions = await _graphService.AddSecurityActionsAsync(securityActionRequest.ToSecurityAction());
                return Ok(securityActions);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        ///  Get the alerts based on filters
        /// </summary>
        /// <param name="alertFilter"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ResponseCache(CacheProfileName = "ActionsFilters")]
        public async Task<ActionResult> Filters()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var filters = new Dictionary<string, IEnumerable<string>>();

                Dictionary<string, IEnumerable<string>> providerFilter = null;

                /// if in appsettings.json UseMockData == true
                if (!_demoExample.UseMockData)
                {
                    var alerts = await _graphService.GetAlertsAsync(new AlertFilterModel(200));

                    providerFilter = _graphService.GetProvidersFilter(alerts, "Action");
                }
                else
                {
                    providerFilter = _graphService.GetProvidersFilter(_demoExample.GetProviders(), "Action");
                }

                filters.AddRange(providerFilter);

                var statuses = _actionService.GetStatuses();

                filters.AddRange(statuses);

                var demoFilters = _demoExample.GetActionFilters();

                filters.AddRange(demoFilters);

                return Ok(filters);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}