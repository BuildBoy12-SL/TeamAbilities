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
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
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
            Config.LoadRoleConfigs();
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            base.OnDisabled();
        }
    }
}