// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using System.Linq;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using TeamAbilities.Abilities;
    using TeamAbilities.API;
    using TeamAbilities.Components;
    using UnityEngine;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Player.TriggeringTesla += OnTriggeringTesla;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.TriggeringTesla -= OnTriggeringTesla;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.GameObject.TryGetComponent(out ScientistLocatorComponent scientistLocatorComponent))
                Object.Destroy(scientistLocatorComponent);

            if (ev.IsAllowed && ev.Player.Role == RoleType.NtfCaptain && plugin.Config.AbilityConfigs.TeslaToggle.RestoreOnDeath)
                TeslaToggle.TeslasDisabled = false;

            if (ev.NewRole == RoleType.NtfCaptain && plugin.Config.SingleCommander && Player.Get(RoleType.NtfCaptain).Any())
                ev.NewRole = RoleType.NtfSergeant;

            if (ev.IsAllowed && plugin.Config.ScientistLocator.Roles.Contains(ev.NewRole))
                ev.Player.GameObject.AddComponent<ScientistLocatorComponent>();
        }

        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (TeslaToggle.TeslasDisabled)
                ev.IsTriggerable = false;
        }

        private void OnRoundStarted()
        {
            Ability.ClearAllCooldowns();
            TeslaToggle.TeslasDisabled = false;
        }
    }
}