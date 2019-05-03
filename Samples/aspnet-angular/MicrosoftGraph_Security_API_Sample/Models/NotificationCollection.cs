// -----------------------------------------------------------------------
// <copyright file="NotificationCollection.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models
{
    public class NotificationCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationCollection" /> class.
        /// </summary>
        public NotificationCollection()
        {
            this.Value = new List<Notification>();
        }

        /// <summary>
        /// Gets or sets the top level element that contains the array
        /// </summary>
        public List<Notification> Value { get; set; }
    }
}