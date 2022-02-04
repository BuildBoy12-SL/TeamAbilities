// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using Exiled.API.Extensions;
    using Exiled.Events.EventArgs;
    using TeamAbilities.API;
    using TeamAbilities.Components;
    using TeamAbilities.Events.EventArgs;
    using UnityEngine;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        public void Subscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Player.TogglingNoClip += OnTogglingNoclip;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.TogglingNoClip -= OnTogglingNoclip;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.GameObject.TryGetComponent(out ScientistLocatorComponent scientistLocatorComponent))
                Object.Destroy(scientistLocatorComponent);

            if (ev.IsAllowed && ev.NewRole != RoleType.FacilityGuard && ev.NewRole.GetTeam() == Team.MTF)
                ev.Player.GameObject.AddComponent<ScientistLocatorComponent>();
        }

        private void OnTogglingNoclip(TogglingNoClipEventArgs ev)
        {
            Events.Handlers.Player.OnSentKeypress(new SentKeypressEventArgs(ev.Player, DetectableKey.LAlt));
        }

        private void OnRoundStarted()
        {
        }
    }
}