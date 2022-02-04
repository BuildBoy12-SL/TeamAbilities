// -----------------------------------------------------------------------
// <copyright file="PassiveAbilityResolvable.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities.Generics
{
    using Exiled.CustomRoles.API.Features;

    /// <inheritdoc cref="PassiveAbility"/>
    public abstract class PassiveAbilityResolvable : PassiveAbility, IResolvableAbility
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassiveAbilityResolvable"/> class.
        /// </summary>
        protected PassiveAbilityResolvable()
        {
            AbilityType = GetType().Name;
        }

        /// <inheritdoc/>
        public string AbilityType { get; }
    }
}