// -----------------------------------------------------------------------
// <copyright file="SimplifiedLight.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.SupplyDrops.Data
{
    using AdminToys;
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// A simplified way to spawn a light.
    /// </summary>
    public class SimplifiedLight
    {
        private static LightSourceToy lightPrefab;
        private readonly Vector3 position;
        private readonly Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedLight"/> class.
        /// </summary>
        /// <param name="position">The position of the light.</param>
        /// <param name="color">The color of the light.</param>
        public SimplifiedLight(Vector3 position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        private static LightSourceToy LightPrefab
        {
            get
            {
                if (lightPrefab == null)
                {
                    foreach (var gameObject in NetworkClient.prefabs.Values)
                    {
                        if (gameObject.TryGetComponent(out LightSourceToy component))
                            lightPrefab = component;
                    }
                }

                return lightPrefab;
            }
        }

        /// <summary>
        /// Spawns the light.
        /// </summary>
        /// <param name="parent">The parent to the light.</param>
        /// <returns>The <see cref="GameObject"/> of the spawned light.</returns>
        public GameObject Spawn(Transform parent)
        {
            LightSourceToy light = Object.Instantiate(LightPrefab, parent);

            light.transform.localPosition = position;
            light.NetworkMovementSmoothing = 60;
            light.NetworkLightColor = color;

            NetworkServer.Spawn(light.gameObject);

            return light.gameObject;
        }
    }
}