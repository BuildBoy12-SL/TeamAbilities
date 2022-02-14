// -----------------------------------------------------------------------
// <copyright file="DropController.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.SupplyDrops.Controllers
{
    using System.Collections.Generic;
    using Exiled.API.Features.Items;
    using MEC;
    using TeamAbilities.Abilities;
    using UnityEngine;

    /// <summary>
    /// The controller of a drop.
    /// </summary>
    public class DropController : MonoBehaviour
    {
        private Rigidbody rigidbody;
        private bool collided;
        private bool crateOpened;
        private SupplyDrop config;

        /// <summary>
        /// Gets or sets the balloon of the drop.
        /// </summary>
        public GameObject Balloon { get; set; }

        /// <summary>
        /// Gets or sets all faces of the drop.
        /// </summary>
        public List<GameObject> Faces { get; set; }

        private void Start()
        {
            config = Plugin.Instance.Config.AbilityConfigs.SupplyDrop;

            ChangeLayers(transform, config.DropLayer);

            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.mass = 20;
            rigidbody.drag = 3;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }

        private void Update()
        {
            if (!collided)
                transform.localEulerAngles += Vector3.up * Time.deltaTime * 30;
        }

        private void OnCollisionEnter()
        {
            if (collided) // The box has 4 simultaneous collisions
                return;

            collided = true;
            Destroy(gameObject.GetComponent<Rigidbody>());

            Balloon.AddComponent<BalloonController>();

            AddTrigger();
        }

        private void AddTrigger()
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = Vector3.one * 7.5f;
            collider.center = Vector3.up;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!collided || crateOpened || other.gameObject.name != "Player")
                return;

            crateOpened = true;

            if (config.PossibleItems != null)
            {
                for (int i = 0; i < config.DroppedItems; i++)
                    new Item(config.PossibleItems[Random.Range(0, config.PossibleItems.Count)]).Spawn(transform.position);
            }

            foreach (GameObject face in Faces)
            {
                Rigidbody r = face.AddComponent<Rigidbody>();
                r.AddExplosionForce(20, transform.position, 1);
            }

            Timing.CallDelayed(5f, () => Destroy(gameObject));
        }

        private void ChangeLayers(Transform t, int layer)
        {
            t.gameObject.layer = layer;
            foreach (Transform child in t)
            {
                ChangeLayers(child, layer);
            }
        }
    }
}