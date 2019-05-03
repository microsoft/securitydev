// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public static class GraphQueryProvider
    {
        public static string GetQueryByAlertFilter(AlertFilterModel filter)
        {
            var filteredQuery = string.Empty;

            foreach (KeyValuePair<string, List<AlertFilterProperty>> property in filter)
            {
                var filtersForKey = string.Empty;
                if (property.Key.Equals("createdDateTime"))
                {
                    filtersForKey = $"({string.Join($" {AlertFilterOperator.And} ", property.Key.Trim().EndsWith(")") ? property.Value.Select(item => $"{property.Key.Substring(0, property.Key.Length - 1)} {item.PropertyDescription.Operator} {item.Value})") : property.Value.Select(item => $"{property.Key} {item.PropertyDescription.Operator} {item.Value}"))})";
                }
                else
                {
                    filtersForKey = $"({string.Join($" {AlertFilterOperator.Or} ", property.Key.Trim().EndsWith(")") ? property.Value.Select(item => $"{property.Key.Substring(0, property.Key.Length - 1)} {item.PropertyDescription.Operator} {item.Value})") : property.Value.Select(item => $"{property.Key} {item.PropertyDescription.Operator} {item.Value}"))})";
                }

                filteredQuery += $"{(filteredQuery.Length != 0 ? $" {AlertFilterOperator.And} " : string.Empty)}{filtersForKey}";
            }

            return filteredQuery;
        }
    }
}