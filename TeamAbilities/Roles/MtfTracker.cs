// -----------------------------------------------------------------------
// <copyright file="MtfTracker.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Roles
{
    using System.Collections.Generic;
    using Exiled.CustomRoles.API.Features;
    using TeamAbilities.Abilities;
    using TeamAbilities.API;

    public class MtfTracker : BasicCustomRole
    {
        /// <inheritdoc />
        public override uint Id { get; set; } = 1;

        /// <inheritdoc />
        public override string Name { get; set; } = "MTF Tracker";

        /// <inheritdoc />
        public override string Description { get; set; } = "Tracks nearby scientists.";

        /// <inheritdoc />
        public override RoleType[] ApplicableRoles { get; set; } =
        {
            RoleType.NtfCaptain,
            RoleType.NtfSergeant,
            RoleType.NtfSpecialist,
            RoleType.NtfPrivate,
        };

        /// <inheritdoc />
        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>
        {
            new ScientistLocator(),
        };
    }
}