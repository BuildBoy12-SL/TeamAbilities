// -----------------------------------------------------------------------
// <copyright file="Ability.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using UnityEngine;

    /// <summary>
    /// The ability base class.
    /// </summary>
    public abstract class Ability
    {
        private readonly Dictionary<Player, float> cooldowns = new Dictionary<Player, float>();
        private float globalCooldown;

        /// <summary>
        /// Gets a <see cref="List{T}"/> of all registered abilities.
        /// </summary>
        public static List<Ability> Registered { get; } = new List<Ability>();

        /// <summary>
        /// Gets or sets the name of the ability.
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of roles that can use the ability.
        /// </summary>
        public abstract List<RoleType> RequiredRoles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cooldown will apply to all users instead of individuals.
        /// </summary>
        public abstract bool GlobalCooldown { get; set; }

        /// <summary>
        /// Gets or sets the duration, in seconds, of the ability's cooldown.
        /// </summary>
        public abstract int Cooldown { get; set; }

        /// <summary>
        /// Clears the cooldowns on all abilities.
        /// </summary>
        public static void ClearAllCooldowns()
        {
            foreach (Ability ability in Registered)
                ability.ClearCooldowns();
        }

        /// <summary>
        /// Registers an <see cref="Ability"/>.
        /// </summary>
        /// <returns>A value indicating whether the <see cref="Ability"/> was registered or not.</returns>
        public bool TryRegister()
        {
            if (Registered.Contains(this))
            {
                Log.Warn($"Couldn't register the ability '{Name}' as it already exists.");
                return false;
            }

            if (Registered.Any(ability => string.Equals(ability.Name, Name, StringComparison.OrdinalIgnoreCase)))
            {
                Log.Warn($"Attempted to add an ability with a duplicate name of {Name}.");
                return false;
            }

            Registered.Add(this);
            return true;
        }

        /// <summary>
        /// Unregisters an <see cref="Ability"/>.
        /// </summary>
        /// <returns>A value indicating whether the <see cref="Ability"/> was unregistered or not.</returns>
        public bool TryUnregister()
        {
            if (!Registered.Remove(this))
            {
                Log.Warn($"Cannot unregister an ability with the name of {Name} as it was not registered!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles commands for the ability.
        /// </summary>
        /// <param name="sender">The sender of the command.</param>
        /// <param name="response">The message to return to the sender.</param>
        /// <returns>A value indicating whether the ability was executed.</returns>
        public virtual bool Execute(Player sender, out string response)
        {
            if (RequiredRoles == null || !RequiredRoles.Contains(sender.Role))
            {
                response = Plugin.Instance.Translation.IncorrectRole;
                return false;
            }

            if (!sender.IsBypassModeEnabled && IsOnCooldown(sender, out float remainingTime))
            {
                response = Plugin.Instance.Translation.OnCooldown.Replace("{duration}", ((int)remainingTime).ToString());
                return false;
            }

            if (!RunAbility(sender, out response))
                return false;

            if (sender.IsBypassModeEnabled)
                return true;

            SetCooldown(sender);
            return true;
        }

        /// <summary>
        /// Runs the main method of the ability.
        /// </summary>
        /// <param name="player">The player who ran the ability.</param>
        /// <param name="response">The message to return to the sender.</param>
        /// <returns>A value indicating whether the ability was executed successfully.</returns>
        protected abstract bool RunAbility(Player player, out string response);

        /// <summary>
        /// Checks the time before a player can execute the ability.
        /// </summary>
        /// <param name="sender">The player to check the time of.</param>
        /// <param name="remainingTime">The remaining time left before the cooldown expires.</param>
        /// <returns>A value indicating whether the player is on cooldown.</returns>
        protected virtual bool IsOnCooldown(Player sender, out float remainingTime)
        {
            if (GlobalCooldown)
            {
                remainingTime = globalCooldown - Time.time;
                return remainingTime > 0f;
            }

            if (cooldowns.TryGetValue(sender, out float cooldown))
            {
                remainingTime = cooldown - Time.time;
                return remainingTime > 0f;
            }

            remainingTime = 0f;
            return false;
        }

        private void SetCooldown(Player sender)
        {
            cooldowns[sender] = Time.time + Cooldown;
            globalCooldown = Time.time + Cooldown;
        }

        /// <summary>
        /// Clears the cooldowns globally.
        /// </summary>
        private void ClearCooldowns()
        {
            cooldowns.Clear();
            globalCooldown = 0f;
        }
    }
}