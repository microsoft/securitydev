// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Extensions
{
    public static class DeviceExtensions
    {
        public static IEnumerable<Device> ToDevices(this IUserRegisteredDevicesCollectionWithReferencesPage page)
        {
            List<Device> devices = new List<Device>();

            return devices;
        }

        public static IEnumerable<Device> ToDevices(this IUserOwnedDevicesCollectionWithReferencesPage page)
        {
            List<Device> devices = new List<Device>();

            return devices;
        }
    }
}