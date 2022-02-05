// -----------------------------------------------------------------------
// <copyright file="Emp.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using TeamAbilities.API;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    public class Emp : Ability
    {
        private int uses;
        private CoroutineHandle empCoroutine;
        private bool empEnabled;

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
        [Description("The duration, in seconds, of the balackout.")]
        public float Duration { get; set; } = 60f;

        /// <summary>
        /// Gets or sets the doors which will be immune to the EMP's effects.
        /// </summary>
        [Description("The doors which will be immune to the EMP's effects.")]
        public List<DoorType> ImmuneDoors { get; set; } = new List<DoorType>
        {
            DoorType.NukeSurface,
            DoorType.Scp079First,
            DoorType.Scp079Second,
        };

        /// <summary>
        /// Gets or sets the color of the lights when they're affected by the EMP.
        /// </summary>
        [Description("The color of the lights when they're affected by the EMP.")]
        public SerializableColor Color { get; set; } = new SerializableColor(0.6f, 0.1f, 0.1f);

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public EmpTranslations Translations { get; set; } = new EmpTranslations();

        /// <inheritdoc />
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            base.SubscribeEvents();
        }

        /// <inheritdoc />
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            base.UnsubscribeEvents();
        }

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            if (uses >= UsesPerRound)
            {
                response = Translations.ExceededUses;
                return false;
            }

            uses++;
            empEnabled = true;
            empCoroutine = Timing.RunCoroutine(RunEmp());
            response = Translations.UsedSuccessfully;
            return true;
        }

        private IEnumerator<float> RunEmp()
        {
            foreach (Room room in Map.Rooms)
                room.Color = Color;

            yield return Timing.WaitForSeconds(Duration);
            empEnabled = false;
            foreach (Room room in Map.Rooms)
                room.ResetColor();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (empEnabled && !ImmuneDoors.Contains(ev.Door.Type))
                ev.IsAllowed = true;
        }

        private void OnRoundStarted()
        {
            if (empCoroutine.IsRunning)
                Timing.KillCoroutines(empCoroutine);
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