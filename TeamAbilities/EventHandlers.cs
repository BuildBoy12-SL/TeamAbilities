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
    using MEC;
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
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            Exiled.Events.Handlers.Player.TriggeringTesla += OnTriggeringTesla;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Exiled.Events.Handlers.Player.TriggeringTesla -= OnTriggeringTesla;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.GameObject.TryGetComponent(out ScientistLocatorComponent scientistLocatorComponent))
                Object.Destroy(scientistLocatorComponent);

            if (ev.IsAllowed && ev.Player.Role == RoleType.NtfCaptain && plugin.Config.AbilityConfigs.TeslaToggle.RestoreOnDeath)
                plugin.Config.AbilityConfigs.TeslaToggle.TeslasDisabled = false;

            if (ev.NewRole == RoleType.NtfCaptain && plugin.Config.SingleCommander && Player.Get(RoleType.NtfCaptain).Any())
                ev.NewRole = RoleType.NtfSergeant;

            if (ev.IsAllowed && plugin.Config.ScientistLocator.Roles.Contains(ev.NewRole))
                ev.Player.GameObject.AddComponent<ScientistLocatorComponent>();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (plugin.Config.AbilityConfigs.Emp.EmpEnabled && !plugin.Config.AbilityConfigs.Emp.ImmuneDoors.Contains(ev.Door.Type))
                ev.IsAllowed = true;
        }

        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (plugin.Config.AbilityConfigs.TeslaToggle.TeslasDisabled)
                ev.IsTriggerable = false;
        }

        private void OnRoundStarted()
        {
            Ability.ClearAllCooldowns();
            plugin.Config.AbilityConfigs.TeslaToggle.TeslasDisabled = false;
            if (plugin.Config.AbilityConfigs.Emp.EmpCoroutine.IsRunning)
                Timing.KillCoroutines(plugin.Config.AbilityConfigs.Emp.EmpCoroutine);
        }
    }
}