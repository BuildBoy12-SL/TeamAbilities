// -----------------------------------------------------------------------
// <copyright file="Hack.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using TeamAbilities.API;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    public class Hack : Ability
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Hack";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.ChaosConscript,
            RoleType.ChaosMarauder,
            RoleType.ChaosRepressor,
            RoleType.ChaosRifleman,
        };

        /// <inheritdoc />
        [YamlIgnore]
        public override bool GlobalCooldown { get; set; } = true;

        /// <inheritdoc />
        [YamlIgnore]
        public override int Cooldown { get; set; }

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public HackTranslations Translations { get; set; } = new HackTranslations();

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            response = Translations.AbilityNotFound;
            return false;
        }

        /// <summary>
        /// Contains translatable strings for the <see cref="Hack"/> ability.
        /// </summary>
        public class HackTranslations
        {
            /// <summary>
            /// Gets or sets the response to give when an ability that does not exist is attempted to be used.
            /// </summary>
            public string AbilityNotFound { get; set; } = "That ability does not exist.";
        }
    }
}