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

    public class SimplifiedToy
    {
        private static PrimitiveObjectToy toyPrefab;
        private readonly PrimitiveType type;
        private readonly Vector3 position;
        private readonly Vector3 rotation;
        private readonly Vector3 scale;
        private readonly Color color;

        public SimplifiedToy(PrimitiveType type, Vector3 position, Vector3 rotation, Vector3 scale, Color color)
        {
            this.type = type;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.color = color;
        }

        public GameObject Spawn(Transform parent)
        {
            var toy = Object.Instantiate(ToyPrefab, parent);

            toy.NetworkPrimitiveType = type;

            toy.transform.localPosition = position;
            toy.transform.localEulerAngles = rotation;
            toy.transform.localScale = scale;

            toy.NetworkScale = scale; // Fix collision smh (it needs both localScale and networkScale for some reason)
            toy.NetworkMaterialColor = color;
            toy.NetworkMovementSmoothing = 60;

            NetworkServer.Spawn(toy.gameObject);

            return toy.gameObject;
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
    }
}