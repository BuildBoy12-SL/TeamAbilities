// -----------------------------------------------------------------------
// <copyright file="ScientistLocatorConfig.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// The configs required to modify the behavior of the <see cref="Components.ScientistLocatorComponent"/>.
    /// </summary>
    public class ScientistLocatorConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether only scientists in the same zone will be tracked.
        /// </summary>
        [Description("Whether only scientists in the same zone will be tracked.")]
        public bool SameZoneOnly { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum distance that a scientist will appear on the tracker.
        /// </summary>
        public float MaximumRange { get; set; } = 100f;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, between updating a player's locator.
        /// </summary>
        [Description("The amount of time, in seconds, between updating a player's locator.")]
        public float RefreshRate { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the broadcast to send to players that pick up one or more scientists on their trackers.
        /// </summary>
        public string Broadcast { get; set; } = "<size=30><color=#c50000>Distance to nearest target: </color><color=#10F110>$Bar</color></size> \n" +
                                                "<size=25>Scientists In Range: <color=#c50000>$TargetCount</color></size>";
    }
}