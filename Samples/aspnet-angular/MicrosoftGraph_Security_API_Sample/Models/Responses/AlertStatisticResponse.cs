// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class AlertStatisticResponse
    {
        public SecureScoreStatistic SecureScore { get; set; }

        public List<AlertsByStatus> AlertsByStatus { get; set; }

        public AlertsByEntity AlertsByEntity { get; set; }

        public List<Info> AlertsByProvider { get; set; }
    }

    public class SecureScoreStatistic
    {
        public SecureScoreStatistic()
        {
        }

        public SecureScoreStatistic(double current, double max, Dictionary<string, int> values)
        {
            Current = current;
            Max = max;
            Values = new List<Value>();
            foreach (var val in values)
            {
                Values.Add(new Value(val.Key, val.Value));
            }
        }

        public SecureScoreStatistic(double current, double max)
        {
            Current = current;
            Max = max;
            Values = new List<Value>() { new Value("High"), new Value("Medium"), new Value("Low"), new Value("Informational") };
        }

        public SecureScoreStatistic(double current, double max, List<Value> values)
        {
            Current = current;
            Max = max;
            Values = values;
        }

        public double Current { get; set; }

        public double Max { get; set; }

        public List<Value> Values { get; set; }
    }

    public class AlertsByEntity
    {
        public List<Info> UsersWithTheMostAlerts { get; set; }

        public List<Info> HostsWithTheMostAlerts { get; set; }

        public List<Info> DomainsWithTheMostAlerts { get; set; }

        public List<Info> IPWithTheMostAlerts { get; set; }
    }

    public class AlertsByStatus
    {
        public AlertsByStatus()
        {
        }

        public AlertsByStatus(string statusName)
        {
            StatusName = statusName;
            Values = new List<Value>() { new Value("High"), new Value("Medium"), new Value("Low"), new Value("Informational") };
        }

        public AlertsByStatus(string statusName, List<Value> values)
        {
            StatusName = statusName;
            Values = values;
        }

        public string StatusName { get; set; }
        public List<Value> Values { get; set; }
    }

    public class Info
    {
        public Info()
        {
        }

        public Info(Specification specification)
        {
            Specification = specification;
            Values = new List<Value>() { new Value("High"), new Value("Medium"), new Value("Low"), new Value("Informational") };
        }

        public Info(Specification specification, List<Value> values)
        {
            Specification = specification;
            Values = values;
        }

        public Specification Specification { get; set; }

        public List<Value> Values { get; set; }
    }

    public class Specification
    {
        public Specification()
        {
        }

        public Specification(string title, string filterValue)
        {
            Title = title;
            FilterValue = filterValue;
        }

        public string Title { get; set; }

        public string FilterValue { get; set; }
    }

    public class Value
    {
        public Value(string name, int amount = 0)
        {
            Name = name;
            Amount = amount;
        }

        public Value()
        {
            Amount = 0;
        }

        public string Name { get; set; }

        public int Amount { get; set; }
    }
}