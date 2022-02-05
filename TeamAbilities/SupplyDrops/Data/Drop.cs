// -----------------------------------------------------------------------
// <copyright file="Drop.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.SupplyDrops.Data
{
    using System.Collections.Generic;
    using TeamAbilities.SupplyDrops.Controllers;
    using UnityEngine;

    public class Drop
    {
        private readonly GameObject gameObject;
        private readonly List<GameObject> faces = new List<GameObject>();
        private GameObject balloon;

        public Drop(Vector3 position)
        {
            gameObject = new GameObject("Drop");
            gameObject.transform.position = position;
        }

        public void Spawn()
        {
            var transform = gameObject.transform;

            for (float i = -0.5f; i < 1; i++)
            {
                faces.Add(new Face(new Vector3(i, 0f, 0f), Vector3.up * 90f, transform).GameObject);
                faces.Add(new Face(new Vector3(0f, i, 0f), Vector3.right * 90f, transform).GameObject);
                faces.Add(new Face(new Vector3(0f, 0f, i), Vector3.zero, transform).GameObject);
            }

            var rndColor = Random.ColorHSV();
            balloon = new SimplifiedToy(PrimitiveType.Sphere, Vector3.up * 2.125f, Vector3.zero, Vector3.one * -2, rndColor).Spawn(transform);
            new SimplifiedLight(Vector3.zero, rndColor).Spawn(balloon.transform).transform.SetParent(balloon.transform);

            var scale = new Vector3(-0.01f, -1f, -0.01f);

            for (float i = -0.5f; i < 1; i++)
            {
                for (float j = -0.5f; j < 1; j++)
                    new SimplifiedToy(PrimitiveType.Cylinder, new Vector3(i, 1f, j), new Vector3(0f, 100f * j * (i * -2f), -20f * i), scale, Color.white).Spawn(transform).transform.SetParent(balloon.transform);
            }

            var controller = gameObject.AddComponent<DropController>();
            controller.Balloon = balloon;
            controller.Faces = faces;
        }

        private class Face
        {
            private readonly List<GameObject> childs = new List<GameObject>();

            public GameObject GameObject { get; }

            public Face(Vector3 pos, Vector3 rot, Transform drop)
            {
                GameObject = new GameObject("Face");
                GameObject.transform.SetParent(drop, false);

                GameObject.transform.localPosition = pos;
                GameObject.transform.localEulerAngles = rot;

                var parent = GameObject.transform;

                var scale = new Vector3(1, 0.2f, 0.125f);

                for (float i = -0.5f; i < 1; i++)
                {
                    childs.Add(new SimplifiedToy(PrimitiveType.Cube, new Vector3(i, 0, 0), Vector3.forward * 90, scale, Color.black).Spawn(parent));
                    childs.Add(new SimplifiedToy(PrimitiveType.Cube, new Vector3(0, i, 0), Vector3.zero, scale, Color.black).Spawn(parent));
                }

                childs.Add(new SimplifiedToy(PrimitiveType.Cube, Vector3.zero, Vector3.zero, new Vector3(1,1,0.1f), Color.gray).Spawn(parent));
                childs.Add(new SimplifiedToy(PrimitiveType.Cube, Vector3.zero, Vector3.forward * 45, new Vector3(1.2f, 0.2f, 0.125f), Color.black).Spawn(parent));
            }
        }
    }
}