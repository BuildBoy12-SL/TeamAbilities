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
        public override bool GlobalCooldown { get; set; } = true;

        /// <inheritdoc />
        public override int Cooldown { get; set; } = 0;

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            throw new System.NotImplementedException();
        }

        public class HackTranslations
        {
        }
    }
}