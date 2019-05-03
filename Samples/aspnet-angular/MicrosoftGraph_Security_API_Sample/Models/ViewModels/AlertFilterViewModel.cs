// -----------------------------------------------------------------------
// <copyright file="AlertFilterViewModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Providers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Models.ViewModels
{
    [ModelBinder(BinderType = typeof(AlertFilterValueEntityBinder))]
    public class AlertFilterViewModel
    {
        public int? Top { get; set; }

        public AlertFilterCollection Filters { get; set; }
    }

    public class AlertFilterCollection : IEnumerable
    {
        public AlertFilterCollection()
        {
            Filters = new Dictionary<string, IEnumerable<string>>();
        }

        public AlertFilterCollection(Dictionary<string, IEnumerable<string>> filters)
        {
            Filters = filters;
        }

        public Dictionary<string, IEnumerable<string>> Filters { get; set; }

        public int Count
        {
            get { return this.Filters.Count; }
        }

        public IEnumerable<string> GetFilterValue(string filterKey)
        {
            var key = filterKey.ToLower();
            return Filters.ContainsKey(key) ? Filters[key] : Enumerable.Empty<string>();
        }

        public void Add(string key, IEnumerable<string> value)
        {
            this.Filters.Add(key.ToLower(), value);
        }

        public IEnumerator GetEnumerator()
        {
            return this.Filters.GetEnumerator();
        }
    }
}