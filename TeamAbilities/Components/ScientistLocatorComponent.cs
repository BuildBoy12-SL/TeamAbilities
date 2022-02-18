// -----------------------------------------------------------------------
// <copyright file="ScientistLocatorComponent.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Components
{
    using System.Collections.Generic;
    using System.Linq;
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

        private IEnumerator<float> RunLocator()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(config.RefreshRate);
                List<float> targetDistances = TargetDistances();
                if (targetDistances.Count == 0)
                    continue;

                double percentage = (config.MaximumRange - targetDistances.Min()) / config.MaximumRange;
                string bar = DrawBar(percentage);

                string broadcast = config.Broadcast.Replace("$Bar", bar).Replace("$TargetCount", targetDistances.Count.ToString());
                player.Broadcast((ushort)(config.RefreshRate + 1), broadcast, shouldClearPrevious: true);
            }
        }

        private List<float> TargetDistances()
        {
            List<float> distances = new List<float>();
            foreach (Player target in Player.List)
            {
                if (target.Role != RoleType.Scientist)
                    continue;

                if (config.SameZoneOnly && target.Zone != player.Zone)
                    continue;

                float distance = Vector3.Distance(player.Position, target.Position);
                if (distance > config.MaximumRange)
                    continue;

                distances.Add(distance);
            }

            return distances;
        }
    }
}