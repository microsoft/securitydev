// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ModelBinding;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public class AlertFilterValueEntityBinder : IModelBinder
    {
        public bool ContainsPrefix(string prefix)
        {
            return string.Compare("filters", prefix, true) == 0;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            try
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }
                MemoryStream ms = new MemoryStream();

                bindingContext.HttpContext.Request.Body.CopyTo(ms);

                ms.Position = 0;

                var streamReader = new StreamReader(ms);

                var json = streamReader.ReadToEnd();

                JObject jsonObj = JObject.Parse(json);

                AlertFilterViewModel alertFilterViewModel = new AlertFilterViewModel();
                foreach (var obj in jsonObj)
                {
                    if (obj.Key.Equals("Top"))
                    {
                        alertFilterViewModel.Top = obj.Value.Value<int>();
                    }

                    if (obj.Key.Equals("Filters"))
                    {
                        //var d = obj.Value.ToObject<Dictionary<string, IEnumerable<string>>>();

                        alertFilterViewModel.Filters = new AlertFilterCollection(obj.Value.ToObject<Dictionary<string, IEnumerable<string>>>());
                    }
                }

                bindingContext.Result = ModelBindingResult.Success(alertFilterViewModel);
                return Task.CompletedTask;
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
        }
    }
}