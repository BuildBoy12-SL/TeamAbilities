// -----------------------------------------------------------------------
// <copyright file="BasicCustomRole.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exiled.API.Features;
    using Exiled.CustomRoles.API.Features;
    using YamlDotNet.Serialization;

    public abstract class BasicCustomRole
    {
        private readonly List<Player> trackedPlayers = new List<Player>();

        public abstract uint Id { get; set; }

        public abstract string Name { get; set; }

        public abstract string Description { get; set; }

        public abstract RoleType[] ApplicableRoles { get; set; }

        /// <summary>
        /// Gets a collection of tracked players.
        /// </summary>
        [YamlIgnore]
        public ReadOnlyCollection<Player> TrackedPlayers => trackedPlayers.AsReadOnly();

        /// <summary>
        /// Gets or sets a list of the roles custom abilities.
        /// </summary>
        public virtual List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>();

        /// <summary>
        /// Checks if a player is considered to belong to the role.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>A value indicating whether the checked player is a ghost.</returns>
        public bool Check(Player player) => TrackedPlayers.Contains(player);

        public virtual void AddRole(Player player)
        {
            foreach (CustomAbility ability in CustomAbilities)
                ability.AddAbility(player);

            trackedPlayers.Add(player);
        }

        public virtual void RemoveRole(Player player)
        {
            trackedPlayers.Remove(player);
            foreach (CustomAbility ability in CustomAbilities)
                ability.RemoveAbility(player);
        }
    }
}