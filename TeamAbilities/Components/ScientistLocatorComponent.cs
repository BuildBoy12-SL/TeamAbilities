// -----------------------------------------------------------------------
// <copyright file="ScientistLocatorComponent.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Components
{
    using System.Collections.Generic;
    using System.Text;
    using Exiled.API.Features;
    using MEC;
    using NorthwoodLib.Pools;
    using TeamAbilities.Configs;
    using UnityEngine;

    /// <summary>
    /// Shows the distance between a mtf and the nearest scientist.
    /// </summary>
    public class ScientistLocatorComponent : MonoBehaviour
    {
        private Player player;
        private ScientistLocatorConfig config;
        private CoroutineHandle coroutineHandle;

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

        private void Awake()
        {
            player = Player.Get(gameObject);
            config = Plugin.Instance.Config.ScientistLocator;
            coroutineHandle = Timing.RunCoroutine(RunLocator());
        }

        private void OnDestroy()
        {
            Timing.KillCoroutines(coroutineHandle);
        }

        private Player ClosestTarget(IEnumerable<Player> targets, out float closestDistance)
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

        private IEnumerator<float> RunLocator()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(config.RefreshRate);
                List<Player> validTargets = ValidTargets();
                if (validTargets.Count == 0)
                    continue;

                Player target = ClosestTarget(validTargets, out float distance);
                if (target == null)
                    continue;

                double percentage = (config.MaximumRange - distance) / config.MaximumRange;
                string bar = DrawBar(percentage);

                string broadcast = config.Broadcast.Replace("$Bar", bar).Replace("$TargetCount", validTargets.Count.ToString());
                player.ClearBroadcasts();
                player.Broadcast((ushort)(config.RefreshRate + 1), broadcast);
            }
        }

        private List<Player> ValidTargets()
        {
            List<Player> targets = new List<Player>();
            foreach (Player target in Player.Get(RoleType.Scientist))
            {
                if (config.SameZoneOnly && target.Zone != player.Zone)
                    continue;

                if ((player.Position - target.Position).sqrMagnitude > config.MaximumRange * config.MaximumRange)
                    continue;

                targets.Add(target);
            }

            return targets;
        }
    }
}