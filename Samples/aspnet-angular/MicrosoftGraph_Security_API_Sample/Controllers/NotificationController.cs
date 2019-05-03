// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Route("notifications")]
    public class NotificationController : Controller
    {
        private readonly IGraphServiceProvider _graphServiceProvider;

        private readonly INotificationService _notificationService;

        public NotificationController(IGraphServiceProvider graphServiceProvider, INotificationService notificationService)
        {
            _graphServiceProvider = graphServiceProvider;
            _notificationService = notificationService;
        }

        [Route("list")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index()
        {
            return this.View("Notification");
        }

        /// <summary>
        /// Listener for the notifications
        /// </summary>
        /// <returns></returns>
        [HttpPost("listen")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Listen()
        {
            // Validate the new subscription by sending the token back to Microsoft Graph.
            // This response is required for each subscription.
            // Parse the received notifications.
            if (!string.IsNullOrWhiteSpace(Request.QueryString.Value))
            {
                var token = HttpUtility.UrlDecode(Request.QueryString.Value.Split("validationToken=")?[1]);
                return this.Content(token, "plain/text");
            }
            else
            {
                List<NotificationViewModel> messages = new List<NotificationViewModel>();
                try
                {
                    string documentContents;
                    using (var inputStream = Request.Body)
                    {
                        using (StreamReader readStream = new StreamReader(inputStream, Encoding.UTF8))
                        {
                            documentContents = readStream.ReadToEnd();
                        }

                        var notifications = JsonConvert.DeserializeObject<NotificationCollection>(documentContents);
                        foreach (Notification notification in notifications?.Value)
                        {
                            notification.EntityId = notification.ResourceData.Id;
                            if (notification.Resource.StartsWith("https://"))
                            {
                                notification.Resource = notification.Resource.Replace("https://", "");
                                var firshSlash = notification.Resource.IndexOf('/');
                                if (firshSlash > -1)
                                {
                                    notification.Resource = notification.Resource.Substring(firshSlash);
                                }
                            }
                            NotificationViewModel messageViewModel = new NotificationViewModel(notification, notification.SubscriptionId);
                            messages.Add(messageViewModel);
                        }

                        if (messages.Count > 0)
                        {
                            _notificationService.SendNotificationToClient(messages);
                        }
                    }
                }
                catch (Exception exception)
                {
                    return BadRequest(exception.Message);
                }

                return await Task.FromResult<ActionResult>(new StatusCodeResult(202));
            }
        }
    }
}