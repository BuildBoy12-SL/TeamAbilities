// -----------------------------------------------------------------------
// <copyright file="Scan.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using TeamAbilities.API;

    /// <inheritdoc />
    public class Scan : Ability
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "scan";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.NtfCaptain,
        };

        /// <inheritdoc />
        public override bool GlobalCooldown { get; set; } = false;

        /// <inheritdoc />
        public override int Cooldown { get; set; } = 180;

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            throw new System.NotImplementedException();
        }
    }
}