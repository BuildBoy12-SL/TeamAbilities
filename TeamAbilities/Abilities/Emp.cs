// -----------------------------------------------------------------------
// <copyright file="Emp.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using MEC;
    using TeamAbilities.API;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    public class Emp : Ability
    {
        private int uses;

        /// <summary>
        /// Gets or sets a value indicating whether the emp is enabled.
        /// </summary>
        [YamlIgnore]
        public bool EmpEnabled { get; set; }

        /// <summary>
        /// Gets the coroutine controlling the delay before the emp is disabled.
        /// </summary>
        [YamlIgnore]
        public CoroutineHandle EmpCoroutine { get; private set; }

        /// <inheritdoc />
        public override string Name { get; set; } = "EMP";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.ChaosConscript,
            RoleType.ChaosMarauder,
            RoleType.ChaosRepressor,
            RoleType.ChaosRifleman,
        };

        /// <summary>
        /// Gets or sets the amount of times the ability can be used per round.
        /// </summary>
        public int UsesPerRound { get; set; } = 1;

        /// <inheritdoc />
        [YamlIgnore]
        public override bool GlobalCooldown { get; set; }

        /// <inheritdoc />
        [YamlIgnore]
        public override int Cooldown { get; set; }

        /// <summary>
        /// Gets or sets the duration, in seconds, of the blackout.
        /// </summary>
        public float Duration { get; set; } = 60f;

        /// <summary>
        /// Gets or sets the doors which will be immune to the emp's effects.
        /// </summary>
        public List<DoorType> ImmuneDoors { get; set; } = new List<DoorType>
        {
            DoorType.NukeSurface,
            DoorType.Scp079First,
            DoorType.Scp079Second,
        };

        /// <summary>
        /// Gets or sets the color of the lights when they're affected by the EMP.
        /// </summary>
        public SerializableColor Color { get; set; } = new SerializableColor(0.6f, 0.1f, 0.1f);

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public EmpTranslations Translations { get; set; } = new EmpTranslations();

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            if (uses >= UsesPerRound)
            {
                response = Translations.ExceededUses;
                return false;
            }

            uses++;
            EmpEnabled = true;
            EmpCoroutine = Timing.RunCoroutine(RunEmp());
            response = Translations.UsedSuccessfully;
            return true;
        }

        private IEnumerator<float> RunEmp()
        {
            foreach (Room room in Map.Rooms)
                room.Color = Color;

            yield return Timing.WaitForSeconds(Duration);
            EmpEnabled = false;
            foreach (Room room in Map.Rooms)
                room.ResetColor();
        }

        /// <summary>
        /// Contains translatable strings for the <see cref="Emp"/> ability.
        /// </summary>
        public class EmpTranslations
        {
            /// <summary>
            /// Gets or sets the response to send to players when the maximum amount of uses for the ability has already been reached.
            /// </summary>
            public string ExceededUses { get; set; } = "The round limit on this command has already been reached.";

            /// <summary>
            /// Gets or sets the response to send to players when the ability is used successfully.
            /// </summary>
            public string UsedSuccessfully { get; set; } = "Facility blacked out.";
        }
    }
}