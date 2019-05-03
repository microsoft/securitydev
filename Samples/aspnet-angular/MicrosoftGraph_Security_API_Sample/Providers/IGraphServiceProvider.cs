// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Services.Interfaces;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public interface IGraphServiceProvider
    {
        IGraphService GetService(string token);
    }
}