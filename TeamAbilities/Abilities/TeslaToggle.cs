// -----------------------------------------------------------------------
// <copyright file="TeslaToggle.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Features;
    using TeamAbilities.API;
    using YamlDotNet.Serialization;

    public class TeslaToggle : Ability
    {
        /// <summary>
        /// Gets or sets a value indicating whether tesla gates should be disabled.
        /// </summary>
        [YamlIgnore]
        public static bool TeslasDisabled { get; set; }

        /// <inheritdoc />
        public override string Name { get; set; } = "Tesla Toggle";

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

        public TeslaToggleTranslations Translations { get; set; } = new TeslaToggleTranslations();

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            TeslasDisabled = !TeslasDisabled;
            response = TeslasDisabled ? Translations.TeslasDisabled : Translations.TeslasEnabled;
            return true;
        }

        public class TeslaToggleTranslations
        {
            public string TeslasEnabled { get; set; } = "Tesla gates have been reenabled.";

            public string TeslasDisabled { get; set; } = "Tesla gates have been disabled.";
        }
    }
}