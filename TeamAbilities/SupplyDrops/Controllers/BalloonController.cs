// -----------------------------------------------------------------------
// <copyright file="BalloonController.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.SupplyDrops.Controllers
{
    using UnityEngine;

    /// <summary>
    /// The controller of a drop's balloon.
    /// </summary>
    public class BalloonController : MonoBehaviour
    {
        private float startPos;

        private void Start() => startPos = transform.position.y;

        private void FixedUpdate()
        {
            if (transform.position.y - startPos < 15)
            {
                transform.position += Vector3.up * Time.deltaTime * 10;
                transform.localScale += Vector3.one * Time.deltaTime * 1.25f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}