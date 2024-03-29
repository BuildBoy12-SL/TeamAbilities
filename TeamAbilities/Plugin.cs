﻿// -----------------------------------------------------------------------
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
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;

            Config.AbilityConfigs.RegisterAbilities();

            eventHandlers = new EventHandlers(this);
            eventHandlers.Subscribe();

            facilityGlitchHandler = new FacilityGlitchHandler(this);
            facilityGlitchHandler.Start();

            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            facilityGlitchHandler.Stop();
            facilityGlitchHandler = null;

            eventHandlers.Unsubscribe();
            eventHandlers = null;

            Config.AbilityConfigs.UnregisterAbilities();

            Instance = null;

            base.OnDisabled();
        }
    }
}