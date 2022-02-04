// -----------------------------------------------------------------------
// <copyright file="ScientistLocator.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using Exiled.API.Features;
    using MEC;
    using NorthwoodLib.Pools;
    using TeamAbilities.Abilities.Generics;
    using UnityEngine;

    /// <inheritdoc />
    public class ScientistLocator : PassiveAbilityResolvable
    {
        private CoroutineHandle coroutineHandle;

        /// <inheritdoc />
        public override string Name { get; set; } = nameof(ScientistLocator);

        /// <inheritdoc />
        public override string Description { get; set; } = "Guides the player to the nearest scientist.";

        /// <summary>
        /// Gets or sets a value indicating whether only scientists in the same zone will be tracked.
        /// </summary>
        [Description("Whether only scientists in the same zone will be tracked.")]
        public bool SameZoneOnly { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum distance that a scientist will appear on the tracker.
        /// </summary>
        public float MaximumRange { get; set; } = 100f;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, between updating a player's locator.
        /// </summary>
        [Description("The amount of time, in seconds, between updating a player's locator.")]
        public float RefreshRate { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the broadcast to send to players that pick up one or more scientists on their trackers.
        /// </summary>
        public string Broadcast { get; set; } = "<size=30><color=#c50000>Distance to nearest target: </color><color=#10F110>$Bar</color></size> \n" +
                                                "<size=25>Scientists In Range: <color=#c50000>$TargetCount</color></size>";

        /// <inheritdoc />
        protected override void AbilityAdded(Player player)
        {
            coroutineHandle = Timing.RunCoroutine(RunLocator(player));
            base.AbilityAdded(player);
        }

        /// <inheritdoc />
        protected override void AbilityRemoved(Player player)
        {
            Timing.KillCoroutines(coroutineHandle);
            base.AbilityRemoved(player);
        }

        private static Player ClosestTarget(Player player, IEnumerable<Player> targets, out float closestDistance)
        {
            Player closestTarget = null;
            closestDistance = float.MaxValue;

            foreach (Player target in targets)
            {
                float distance = Vector3.Distance(player.Position, target.Position);
                if (distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }

            return closestTarget;
        }

        private static string DrawBar(double percentage)
        {
            if (percentage == 0)
                return "(      )";

            StringBuilder barBuilder = StringBuilderPool.Shared.Rent().Append("<color=#ffffff>(</color>");

            percentage *= 100;
            for (double i = 0; i < 100; i += 5)
                barBuilder.Append(i < percentage ? "█" : "<color=#c50000>█</color>");

            barBuilder.Append("<color=#ffffff>)</color>");
            return StringBuilderPool.Shared.ToStringReturn(barBuilder);
        }

        private IEnumerator<float> RunLocator(Player player)
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(RefreshRate);
                List<Player> validTargets = ValidTargets(player);
                if (validTargets.Count == 0)
                    continue;

                Player target = ClosestTarget(player, validTargets, out float distance);
                if (target == null)
                    continue;

                double percentage = (MaximumRange - distance) / MaximumRange;
                string bar = DrawBar(percentage);

                string broadcast = Broadcast.Replace("$Bar", bar).Replace("$TargetCount", validTargets.Count.ToString());
                player.ClearBroadcasts();
                player.Broadcast(1, broadcast);
            }
        }

        private List<Player> ValidTargets(Player player)
        {
            List<Player> targets = new List<Player>();
            foreach (Player target in Player.Get(RoleType.Scientist))
            {
                if (SameZoneOnly && target.Zone != player.Zone)
                    continue;

                if ((player.Position - target.Position).sqrMagnitude < MaximumRange * MaximumRange)
                    continue;

                targets.Add(target);
            }

            return targets;
        }
    }
}