// -----------------------------------------------------------------------
// <copyright file="AbilityConfigs.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Configs
{
    using TeamAbilities.Abilities;

    /// <summary>
    /// All ability configs.
    /// </summary>
    public class AbilityConfigs
    {
        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Abilities.Emp"/> ability.
        /// </summary>
        public Emp Emp { get; set; } = new Emp();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Abilities.Hack"/> ability.
        /// </summary>
        public Hack Hack { get; set; } = new Hack();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Abilities.Scan"/> ability.
        /// </summary>
        public Scan Scan { get; set; } = new Scan();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Abilities.SupplyDrop"/> ability.
        /// </summary>
        public SupplyDrop SupplyDrop { get; set; } = new SupplyDrop();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Abilities.TeslaToggle"/> ability.
        /// </summary>
        public TeslaToggle TeslaToggle { get; set; } = new TeslaToggle();

        /// <summary>
        /// Registers all abilities.
        /// </summary>
        public void RegisterAbilities()
        {
            Emp?.TryRegister();
            Hack?.TryRegister();
            Scan?.TryRegister();
            SupplyDrop?.TryRegister();
            TeslaToggle?.TryRegister();
        }

        /// <summary>
        /// Unregisters all abilities.
        /// </summary>
        public void UnregisterAbilities()
        {
            Emp?.TryUnregister();
            Hack?.TryUnregister();
            Scan?.TryUnregister();
            SupplyDrop?.TryUnregister();
            TeslaToggle?.TryUnregister();
        }
    }
}