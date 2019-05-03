// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using KeyValuePair = Microsoft.Graph.KeyValuePair;

namespace MicrosoftGraph_Security_API_Sample.Extensions
{
    public static class SecurityActionExtensions
    {
        public static IEnumerable<SecurityActionResponse> ToSecurityActionResponses(this IEnumerable<SecurityAction> securityActions)
        {
            List<SecurityActionResponse> responses = new List<SecurityActionResponse>();
            foreach (var action in securityActions)
            {


                var alertId = action?.Parameters.Where(x => x.Name.Equals("AlertId"))?.FirstOrDefault()?.Value;
                var name = action?.Parameters.Where(x => x.Name.Equals("targetName"))?.FirstOrDefault()?.Value;
                var value = action?.Parameters.Where(x => x.Name.Equals("targetValue"))?.FirstOrDefault()?.Value;

                var response = new SecurityActionResponse()
                {
                    Id = string.IsNullOrWhiteSpace(action.Id) ? string.Empty : action.Id,
                    Name = action?.Name,
                    SecurityVendorInformation = action?.VendorInformation ?? null,
                    Status = action.Status,
                    SubmittedDateTime = action?.CreatedDateTime,
                    StatusUpdateDateTime = action?.LastActionDateTime,
                    Reason = new Reason()
                    {
                        Comment = action?.ActionReason,
                        AlertId = alertId,
                    },
                    Target = new Target()
                    {
                        Name = name,
                        Value = value,
                    },
                };

                responses.Add(response);
            }

            return responses;
        }

        public static SecurityActionResponse ToSecurityActionResponse(this SecurityAction securityAction)
        {
            return new SecurityActionResponse()
            {
                Id = securityAction?.Id,
                Name = securityAction?.Name,
                SecurityVendorInformation = securityAction?.VendorInformation,
                Status = OperationStatus.Completed,
                SubmittedDateTime = securityAction?.CreatedDateTime,
                StatusUpdateDateTime = securityAction?.LastActionDateTime,
                Reason = new Reason()
                {
                    Comment = securityAction?.ActionReason,
                    AlertId = securityAction.Parameters.Where(x => x.Name.Equals("AlertId")).FirstOrDefault().Value,
                },
                Target = new Target()
                {
                    Name = securityAction.Parameters.Where(x => x.Name.Equals("targetName")).FirstOrDefault().Value,
                    Value = securityAction.Parameters.Where(x => x.Name.Equals("targetValue")).FirstOrDefault().Value,
                },
            };
        }

        public static SecurityAction ToSecurityAction(this SecurityActionRequest securityActionRequest)
        {
            return new SecurityAction()
            {
                Name = securityActionRequest?.Name,
                ActionReason = securityActionRequest?.Reason?.Comment,
                Parameters = new List<KeyValuePair>()
                {
                    new KeyValuePair() {Name = "AlertId", Value = securityActionRequest?.Reason?.AlertId},
                    new KeyValuePair() {Name = "targetName", Value = securityActionRequest?.Target?.Name,},
                    new KeyValuePair() {Name = "targetValue", Value = securityActionRequest?.Target?.Value,}
                },
           
                VendorInformation = new SecurityVendorInformation()
                {
                    Provider = securityActionRequest?.Vendor,
                    Vendor = "Microsoft"
                },

                User = securityActionRequest?.User,

                LastActionDateTime = DateTimeOffset.UtcNow,
                CreatedDateTime = DateTimeOffset.UtcNow
            };
        }
    }
}