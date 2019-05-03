// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using System;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public class AlertFilterValueBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(AlertFilterViewModel))
            {
                return new BinderTypeModelBinder(typeof(AlertFilterValueEntityBinder));
            }

            return null;
        }
    }
}