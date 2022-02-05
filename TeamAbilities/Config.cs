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
    using TeamAbilities.Abilities;
    using TeamAbilities.Configs;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug messages should be shown.
        /// </summary>
        [Description("Indicates whether debug messages should be shown.")]
        public bool ShowDebug { get; set; } = false;

        /// <summary>
        /// Gets or sets all configs used for the <see cref="Components.ScientistLocatorComponent"/>.
        /// </summary>
        public ScientistLocatorConfig ScientistLocator { get; set; } = new ScientistLocatorConfig();

        public SupplyDrop SupplyDrop { get; set; } = new SupplyDrop();
    }
}