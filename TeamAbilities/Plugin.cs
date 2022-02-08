// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using System;
    using Exiled.API.Features;
    using TeamAbilities.FacilityGlitches;
    using UnityEngine;
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config, Translation>
    {
        private EventHandlers eventHandlers;
        private FacilityGlitchHandler facilityGlitchHandler;

        /// <summary>
        /// Gets the only existing instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <inheritdoc />
        public override string Author => "Build";

        /// <inheritdoc />
        public override string Name => "TeamAbilities";

        /// <inheritdoc />
        public override string Prefix => "TeamAbilities";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(4, 2, 3);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;

            Config.AbilityConfigs.RegisterAbilities();
            Config.CommandConfigs.RegisterCommands();

            eventHandlers = new EventHandlers(this);
            eventHandlers.Subscribe();

            facilityGlitchHandler = new FacilityGlitchHandler(this);
            facilityGlitchHandler.Start();

            Physics.IgnoreLayerCollision(Config.AbilityConfigs.SupplyDrop.DropLayer, 16);
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Physics.IgnoreLayerCollision(Config.AbilityConfigs.SupplyDrop.DropLayer, 16, false);

            facilityGlitchHandler.Stop();
            facilityGlitchHandler = null;

            eventHandlers.Unsubscribe();
            eventHandlers = null;

            Config.AbilityConfigs.UnregisterAbilities();
            Config.CommandConfigs.UnregisterCommands();

            Instance = null;

            base.OnDisabled();
        }
    }
}