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
        /// Gets or sets a value indicating whether only one NTF Captain can be on the map at a given time.
        /// </summary>
        [Description("Whether only one NTF Captain can be on the map at a given time.")]
        public bool SingleCommander { get; set; } = true;

        /// <summary>
        /// Gets or sets all configs used for the <see cref="Components.ScientistLocatorComponent"/>.
        /// </summary>
        public ScientistLocatorConfig ScientistLocator { get; set; } = new ScientistLocatorConfig();

        /// <summary>
        /// Gets or sets an instance of the <see cref="Abilities.SupplyDrop"/> ability.
        /// </summary>
        public SupplyDrop SupplyDrop { get; set; } = new SupplyDrop();

        /// <summary>
        /// Gets or sets an instance of the <see cref="Abilities.TeslaToggle"/> ability.
        /// </summary>
        public TeslaToggle TeslaToggle { get; set; } = new TeslaToggle();
    }
}