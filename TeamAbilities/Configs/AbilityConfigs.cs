// -----------------------------------------------------------------------
// <copyright file="AbilityConfigs.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Configs
{
    using TeamAbilities.Abilities;

    public class AbilityConfigs
    {
        /// <summary>
        /// Gets or sets an instance of the <see cref="Abilities.Hack"/> ability.
        /// </summary>
        public Hack Hack { get; set; } = new Hack();

        /// <summary>
        /// Gets or sets an instance of the <see cref="Abilities.SupplyDrop"/> ability.
        /// </summary>
        public SupplyDrop SupplyDrop { get; set; } = new SupplyDrop();

        /// <summary>
        /// Gets or sets an instance of the <see cref="Abilities.TeslaToggle"/> ability.
        /// </summary>
        public TeslaToggle TeslaToggle { get; set; } = new TeslaToggle();

        public void RegisterAbilities()
        {
            Hack?.TryRegister();
            SupplyDrop?.TryRegister();
            TeslaToggle?.TryRegister();
        }

        public void UnregisterAbilities()
        {
            Hack?.TryUnregister();
            SupplyDrop?.TryUnregister();
            TeslaToggle?.TryUnregister();
        }
    }
}