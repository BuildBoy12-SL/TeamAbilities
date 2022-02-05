// -----------------------------------------------------------------------
// <copyright file="SerializableColor.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.API
{
    using System;
    using UnityEngine;

    /// <summary>
    /// A serializable middleman for the <see cref="Color"/> class.
    /// </summary>
    [Serializable]
    public class SerializableColor
    {
        private float r;
        private float g;
        private float b;
        private float a;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableColor"/> class.
        /// </summary>
        public SerializableColor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableColor"/> class.
        /// </summary>
        /// <param name="r"><inheritdoc cref="R"/></param>
        /// <param name="g"><inheritdoc cref="G"/></param>
        /// <param name="b"><inheritdoc cref="B"/></param>
        /// <param name="a"><inheritdoc cref="A"/></param>
        public SerializableColor(float r, float g, float b, float a = 1f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Gets or sets the red portion of the color.
        /// </summary>
        public float R
        {
            get => r;
            set => r = Mathf.Clamp(value, 0f, 1f);
        }

        /// <summary>
        /// Gets or sets the green portion of the color.
        /// </summary>
        public float G
        {
            get => g;
            set => g = Mathf.Clamp(value, 0f, 1f);
        }

        /// <summary>
        /// Gets or sets the blue portion of the color.
        /// </summary>
        public float B
        {
            get => b;
            set => b = Mathf.Clamp(value, 0f, 1f);
        }

        /// <summary>
        /// Gets or sets the alpha portion of the color.
        /// </summary>
        public float A
        {
            get => a;
            set => a = Mathf.Clamp(value, 0f, 1f);
        }

        /// <summary>
        /// Converts a <see cref="SerializableColor"/> to a <see cref="Color"/>.
        /// </summary>
        /// <param name="serializableColor">The <see cref="SerializableColor"/> to convert.</param>
        /// <returns>The generated <see cref="Vector3"/>.</returns>
        public static implicit operator Color(SerializableColor serializableColor)
        {
            return new Color(serializableColor.R, serializableColor.G, serializableColor.B, serializableColor.A);
        }

        /// <summary>
        /// Converts a <see cref="Color"/> to a <see cref="SerializableColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        /// <returns>The generated <see cref="SerializableColor"/>.</returns>
        public static implicit operator SerializableColor(Color color)
        {
            return new SerializableColor(color.r, color.g, color.b, color.a);
        }

        /// <inheritdoc />
        public override string ToString() => $"({R}, {G}, {B}, {A})";
    }
}