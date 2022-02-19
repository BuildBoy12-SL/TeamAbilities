// -----------------------------------------------------------------------
// <copyright file="TeslaToggle.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using TeamAbilities.API;

    /// <inheritdoc />
    public class TeslaToggle : Ability
    {
        private bool teslasDisabled;

        /// <inheritdoc />
        public override string Command { get; set; } = "Tesla Toggle";

        /// <inheritdoc />
        public override string[] Aliases { get; set; } = Array.Empty<string>();

        /// <inheritdoc />
        public override string Description { get; set; } = "Toggles the tesla gates.";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.NtfCaptain,
        };

        /// <inheritdoc />
        public override bool GlobalCooldown { get; set; } = false;

        /// <inheritdoc />
        public override int Cooldown { get; set; } = 120;

        /// <summary>
        /// Gets or sets a value indicating whether tesla gates will enable when the captain dies.
        /// </summary>
        [Description("Whether tesla gates will enable when the captain dies.")]
        public bool RestoreOnDeath { get; set; } = true;

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public TeslaToggleTranslations Translations { get; set; } = new TeslaToggleTranslations();

        /// <inheritdoc />
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Player.TriggeringTesla += OnTriggeringTesla;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            base.SubscribeEvents();
        }

        /// <inheritdoc />
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.TriggeringTesla -= OnTriggeringTesla;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            base.UnsubscribeEvents();
        }

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            teslasDisabled = !teslasDisabled;
            if (!teslasDisabled)
            {
                foreach (TeslaGate teslaGate in TeslaGate.List)
                    teslaGate.Trigger(true);
            }

            response = teslasDisabled ? Translations.TeslasDisabled : Translations.TeslasEnabled;
            return true;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.IsAllowed && ev.Player.Role == RoleType.NtfCaptain && RestoreOnDeath)
                teslasDisabled = false;
        }

        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (teslasDisabled)
            {
                ev.IsTriggerable = false;
                ev.IsInIdleRange = false;
            }
        }

        private void OnRoundStarted()
        {
            teslasDisabled = false;
        }

        /// <summary>
        /// Contains translatable strings for the <see cref="TeslaToggle"/> ability.
        /// </summary>
        public class TeslaToggleTranslations
        {
            /// <summary>
            /// Gets or sets the response to send to a player when they enable the tesla gates.
            /// </summary>
            public string TeslasEnabled { get; set; } = "Tesla gates have been enabled.";

            /// <summary>
            /// Gets or sets the response to send to a player when they disable the tesla gates.
            /// </summary>
            public string TeslasDisabled { get; set; } = "Tesla gates have been disabled.";
        }
    }
}