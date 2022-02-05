// -----------------------------------------------------------------------
// <copyright file="SupplyDrop.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using TeamAbilities.API;
    using TeamAbilities.SupplyDrops.Data;
    using UnityEngine;

    public class SupplyDrop : Ability
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Supply Drop";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.NtfSergeant,
        };

        /// <inheritdoc />
        public override bool GlobalCooldown { get; set; } = true;

        /// <inheritdoc />
        public override int Cooldown { get; set; } = 180;

        /// <summary>
        /// Gets or sets a value indicating whether this ability may only be used by players on the surface.
        /// </summary>
        [Description("Whether this ability may only be used by players on the surface.")]
        public bool SurfaceOnly { get; set; } = true;

        /// <summary>
        /// Gets or sets the collision layer to use for the supply drop.
        /// </summary>
        public int DropLayer { get; set; } = 6;

        /// <summary>
        /// Gets or sets the items that can spawn in the drop.
        /// </summary>
        public List<ItemType> PossibleItems { get; set; } = new List<ItemType>
        {
            ItemType.GunCrossvec,
            ItemType.GunLogicer,
            ItemType.GunRevolver,
            ItemType.GunE11SR,
        };

        /// <summary>
        /// Gets or sets the amount of items to drop.
        /// </summary>
        public int DroppedItems { get; set; } = 1;

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public SupplyDropTranslations Translations { get; set; } = new SupplyDropTranslations();

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            if (SurfaceOnly && player.Zone != ZoneType.Surface)
            {
                response = Translations.SurfaceOnly;
                return false;
            }

            Vector3 spawnPosition = new Vector3(Random.Range(151.5f, 192f), 1000f + Random.Range(25f, 73f), Random.Range(-70f, -47.5f));
            new Drop(spawnPosition).Spawn();
            response = Translations.SpawnedSuccessfully;
            return true;
        }

        public class SupplyDropTranslations
        {
            public string SpawnedSuccessfully { get; set; } = "Supply drop spawned successfully.";

            public string SurfaceOnly { get; set; } = "This command may only be used on the surface.";
        }
    }
}