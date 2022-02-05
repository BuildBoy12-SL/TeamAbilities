// -----------------------------------------------------------------------
// <copyright file="Emp.cs" company="Build">
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
    public class Emp : Ability
    {
        private int uses;

        /// <inheritdoc />
        public override string Name { get; set; } = "EMP";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.ChaosConscript,
            RoleType.ChaosMarauder,
            RoleType.ChaosRepressor,
            RoleType.ChaosRifleman,
        };

        /// <summary>
        /// Gets or sets the amount of times the ability can be used per round.
        /// </summary>
        public int UsesPerRound { get; set; } = 1;

        /// <inheritdoc />
        [YamlIgnore]
        public override bool GlobalCooldown { get; set; }

        /// <inheritdoc />
        [YamlIgnore]
        public override int Cooldown { get; set; }

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public EmpTranslations Translations { get; set; } = new EmpTranslations();

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            if (uses >= UsesPerRound)
            {
                response = Translations.ExceededUses;
                return false;
            }

            uses++;
            response = Translations.UsedSuccessfully;
            return true;
        }

        /// <summary>
        /// Contains translatable strings for the <see cref="Emp"/> ability.
        /// </summary>
        public class EmpTranslations
        {
            /// <summary>
            /// Gets or sets the response to send to players when the maximum amount of uses for the ability has already been reached.
            /// </summary>
            public string ExceededUses { get; set; } = "The round limit on this command has already been reached.";

            /// <summary>
            /// Gets or sets the response to send to players when the ability is used successfully.
            /// </summary>
            public string UsedSuccessfully { get; set; } = "Facility blacked out.";
        }
    }
}