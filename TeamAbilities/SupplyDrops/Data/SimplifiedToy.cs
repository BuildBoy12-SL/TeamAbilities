// -----------------------------------------------------------------------
// <copyright file="SimplifiedToy.cs" company="Build">
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
    /// A simplified way to create a toy.
    /// </summary>
    public class SimplifiedToy
    {
        private static PrimitiveObjectToy toyPrefab;
        private readonly PrimitiveType type;
        private readonly Vector3 position;
        private readonly Vector3 rotation;
        private readonly Vector3 scale;
        private readonly Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedToy"/> class.
        /// </summary>
        /// <param name="type">The type of primitive.</param>
        /// <param name="position">The position of the toy.</param>
        /// <param name="rotation">The rotation of the toy.</param>
        /// <param name="scale">The scale of the toy.</param>
        /// <param name="color">The color of the toy.</param>
        public SimplifiedToy(PrimitiveType type, Vector3 position, Vector3 rotation, Vector3 scale, Color color)
        {
            this.type = type;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.color = color;
        }

        private static PrimitiveObjectToy ToyPrefab
        {
            get
            {
                if (toyPrefab == null)
                {
                    foreach (var gameObject in NetworkClient.prefabs.Values)
                    {
                        if (gameObject.TryGetComponent(out PrimitiveObjectToy component))
                            toyPrefab = component;
                    }
                }

                return toyPrefab;
            }
        }

        /// <summary>
        /// Spawns the toy.
        /// </summary>
        /// <param name="parent">The parent to the toy.</param>
        /// <returns>The <see cref="GameObject"/> of the spawned toy.</returns>
        public GameObject Spawn(Transform parent)
        {
            PrimitiveObjectToy toy = Object.Instantiate(ToyPrefab, parent);

            toy.NetworkPrimitiveType = type;

            toy.transform.localPosition = position;
            toy.transform.localEulerAngles = rotation;
            toy.transform.localScale = scale;

            toy.NetworkScale = scale;
            toy.NetworkMaterialColor = color;
            toy.NetworkMovementSmoothing = 60;

            NetworkServer.Spawn(toy.gameObject);

            return toy.gameObject;
        }
    }
}