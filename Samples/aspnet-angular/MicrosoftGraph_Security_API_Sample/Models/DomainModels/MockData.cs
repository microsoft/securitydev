// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class MockData
    {
        public MockData()
        {
        }

        public List<SecurityAction> SecurityActions { get; set; }

        public List<Device> Devices { get; set; }

        public ActionFilters ActionFilters { get; set; }

        public List<string> Providers { get; set; }

        public List<string> Categories { get; set; }
    }

    public class ActionFilters
    {
        public IEnumerable<string> ActionNames { get; set; }

        public IEnumerable<string> ActionTargets { get; set; }
    }
}