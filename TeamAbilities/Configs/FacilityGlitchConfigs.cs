// -----------------------------------------------------------------------
// <copyright file="FacilityGlitchConfigs.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Configs
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using UnityEngine;

    /// <summary>
    /// All facility glitch configs.
    /// </summary>
    public class FacilityGlitchConfigs
    {
        private int glitchChance = 50;
        private float minimumGlitchDuration = 5f;
        private float maximumGlitchDuration = 10f;

        /// <summary>
        /// Gets or sets the time, in seconds, between glitch attempts.
        /// </summary>
        public float Interval { get; set; } = 300f;

        /// <summary>
        /// Gets or sets the chance that a glitch can occur each interval. Value will be clamped between 0 - 100.
        /// </summary>
        public int GlitchChance
        {
            get => glitchChance;
            set => glitchChance = Mathf.Clamp(value, 0, 100);
        }

        /// <summary>
        /// Gets or sets the minimum duration, in seconds, that a glitch can occur for.
        /// </summary>
        public float MinimumGlitchDuration
        {
            get => minimumGlitchDuration;
            set
            {
                if (value < 0f)
                    value = 0f;

                minimumGlitchDuration = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum duration, in seconds, that a glitch can occur for.
        /// </summary>
        public float MaximumGlitchDuration
        {
            get => maximumGlitchDuration;
            set
            {
                if (minimumGlitchDuration >= value)
                    value = minimumGlitchDuration + 1f;

                maximumGlitchDuration = value;
            }
        }

        /// <summary>
        /// Gets or sets the rooms where all doors will be glitched.
        /// </summary>
        public List<RoomType> AdditionalRooms { get; set; } = new List<RoomType>
        {
            RoomType.Lcz914,
        };
    }
}