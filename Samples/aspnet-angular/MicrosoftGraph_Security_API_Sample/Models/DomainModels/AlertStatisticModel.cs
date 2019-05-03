// -----------------------------------------------------------------------
// <copyright file="AlertStatisticModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public enum SeveritySortOrder
    {
        High,
        Medium,
        Low,
        Informational,
        Unknown
    }

    public class AlertStatisticModel
    {
        public SecureScoreStatisticModel SecureScore { get; set; }

        public Dictionary<string, int> NewAlerts { get; set; }

        public Dictionary<string, int> InProgressAlerts { get; set; }

        public Dictionary<string, int> ResolvedAlerts { get; set; }

        public Dictionary<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>> UsersWithTheMostAlerts { get; set; }

        public Dictionary<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>> HostsWithTheMostAlerts { get; set; }

        public Dictionary<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>> ProvidersWithTheMostAlerts { get; set; }

        public Dictionary<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>> IPWithTheMostAlerts { get; set; }

        public Dictionary<KeyValuePair<string, string>, Dictionary<SeveritySortOrder, int>> DomainsWithTheMostAlerts { get; set; }
    }

    public class SecureScoreStatisticModel
    {
        public double Current { get; set; }

        public double Max { get; set; }

        public Dictionary<string, int> ComparativeScores { get; set; }
    }

    public class StatisticCollectionModel<T> where T : struct, IConvertible // Enum Type implements IConvertible interface
    {
        private readonly Dictionary<string, Dictionary<T, int>> values = new Dictionary<string, Dictionary<T, int>>();

        public StatisticCollectionModel()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            this.values = new Dictionary<string, Dictionary<T, int>>();
        }

        public void Add(string key, string severity)
        {
            if (!this.values.ContainsKey(key))
            {
                this.values.Add(key, new Dictionary<T, int>());
            }
            var sortOrder = (T)Enum.Parse(typeof(T), severity);
            if (!this.values[key].ContainsKey(sortOrder))
            {
                this.values[key].Add(sortOrder, 1);
            }
            else
            {
                ++this.values[key][sortOrder];
            }
        }

        public int GetTotalAlertAmount(string key)
        {
            if (!this.values.ContainsKey(key) || this.values[key] == null)
            {
                throw new Exception("Key is incorrect.");
            }
            return this.values[key].Sum(rec => rec.Value);
        }

        public Dictionary<string, Dictionary<T, int>> GetSortedTopValues(int top)
        {
            var sortedValues = new Dictionary<string, Dictionary<T, int>>();

            // Sort users by amount of alerts and select top 4 records
            var selectedByAlertAmounts = this.values.OrderByDescending(rec => rec.Value, new AlertAmountComparer<T>()).Take(top).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var rec in selectedByAlertAmounts)
            {
                // Sort information about amounts of alerts for each user by severity levels
                sortedValues.Add(rec.Key, rec.Value.OrderBy(severity => severity.Key).ToDictionary(pair => pair.Key, pair => pair.Value));
            }

            return sortedValues;
        }
    }

    public class AlertAmountComparer<T> : IComparer<Dictionary<T, int>> where T : struct, IConvertible
    {
        public int Compare(Dictionary<T, int> x, Dictionary<T, int> y)
        {
            var compareResult = 0;

            foreach (T level in (T[])Enum.GetValues(typeof(T)))
            {
                if (compareResult != 0)
                {
                    break;
                }
                else
                {
                    var xAmount = x.ContainsKey(level) ? x[level] : 0;
                    var yAmount = y.ContainsKey(level) ? y[level] : 0;
                    compareResult = xAmount - yAmount;
                }
            }

            return compareResult;
        }
    }
}