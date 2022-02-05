// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;
    using TeamAbilities.Configs;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether only one NTF Captain can be on the map at a given time.
        /// </summary>
        [Description("Whether only one NTF Captain can be on the map at a given time.")]
        public bool SingleCommander { get; set; } = true;

        /// <summary>
        /// Gets or sets all configs used for the <see cref="Components.ScientistLocatorComponent"/>.
        /// </summary>
        public ScientistLocatorConfig ScientistLocator { get; set; } = new ScientistLocatorConfig();

        public AbilityConfigs AbilityConfigs { get; set; } = new AbilityConfigs();

        public CommandConfigs CommandConfigs { get; set; } = new CommandConfigs();
    }
}