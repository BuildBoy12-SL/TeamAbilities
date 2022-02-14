// -----------------------------------------------------------------------
// <copyright file="Scan.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using Exiled.API.Features;
    using MEC;
    using NorthwoodLib.Pools;
    using TeamAbilities.API;

    /// <inheritdoc />
    public class Scan : Ability
    {
        private CoroutineHandle scanCoroutine;

        /// <inheritdoc />
        public override string Name { get; set; } = "Scan";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.NtfCaptain,
        };

        /// <inheritdoc />
        public override bool GlobalCooldown { get; set; } = false;

        /// <inheritdoc />
        public override int Cooldown { get; set; } = 240;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, between the player executing the ability and the announcement playing.
        /// </summary>
        [Description("The amount of time, in seconds, between the player executing the ability and the announcement playing.")]
        public int ScanDelay { get; set; } = 60;

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public ScanTranslations Translations { get; set; } = new ScanTranslations();

        /// <inheritdoc />
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;
            base.SubscribeEvents();
        }

        /// <inheritdoc />
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayers;
            base.UnsubscribeEvents();
        }

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            if (!string.IsNullOrEmpty(Translations.PreppingScanAnnouncement))
                Cassie.Message(Translations.PreppingScanAnnouncement);

            scanCoroutine = Timing.CallDelayed(ScanDelay, () => Cassie.Message(FormatAnnouncement()));
            response = Translations.ScanPrepped.Replace("$delay", ScanDelay.ToString());
            return true;
        }

        private static Dictionary<Team, int> GenerateRoles()
        {
            Dictionary<Team, int> teams = new Dictionary<Team, int>();
            foreach (Player player in Player.List)
            {
                if (!teams.ContainsKey(player.Team))
                    teams.Add(player.Team, 0);

                teams[player.Team]++;
            }

            return teams;
        }

        private static int GetTeamCount(Dictionary<Team, int> teams, Team team)
        {
            teams.TryGetValue(team, out int count);
            return count;
        }

        private void OnWaitingForPlayers()
        {
            if (scanCoroutine.IsRunning)
                Timing.KillCoroutines(scanCoroutine);
        }

        private string FormatAnnouncement()
        {
            Dictionary<Team, int> roles = GenerateRoles();
            int classDCount = GetTeamCount(roles, Team.CDP);
            int scientistCount = GetTeamCount(roles, Team.RSC);
            int mtfCount = GetTeamCount(roles, Team.MTF);
            int chaosInsurgencyCount = GetTeamCount(roles, Team.CHI);
            int scpCount = GetTeamCount(roles, Team.SCP);

            StringBuilder scanResultBuilder = StringBuilderPool.Shared.Rent();

            scanResultBuilder.Append(classDCount > 0 && Translations.ScanIdentifiers.TryGetValue(Team.CDP, out string cdpMessage)
                ? cdpMessage.Replace("$classd", classDCount.ToString())
                : string.Empty);

            scanResultBuilder.Append(scientistCount > 0 && Translations.ScanIdentifiers.TryGetValue(Team.RSC, out string rscMessage)
                ? rscMessage.Replace("$scientists", scientistCount.ToString())
                : string.Empty);

            scanResultBuilder.Append(mtfCount > 0 && Translations.ScanIdentifiers.TryGetValue(Team.MTF, out string mtfMessage)
                ? mtfMessage.Replace("$mtf", mtfCount.ToString())
                : string.Empty);

            scanResultBuilder.Append(chaosInsurgencyCount > 0 && Translations.ScanIdentifiers.TryGetValue(Team.CHI, out string chiMessage)
                ? chiMessage.Replace("$chaos", chaosInsurgencyCount.ToString())
                : string.Empty);

            scanResultBuilder.Append(scpCount > 0 && Translations.ScanIdentifiers.TryGetValue(Team.SCP, out string scpMessage)
                ? scpMessage.Replace("$scps", scpCount.ToString())
                : string.Empty);

            string scanResults = StringBuilderPool.Shared.ToStringReturn(scanResultBuilder);
            return Translations.ScanAnnouncement.Replace("$ScanResults", scanResults);
        }

        /// <summary>
        /// Contains translatable strings for the <see cref="Scan"/> ability.
        /// </summary>
        public class ScanTranslations
        {
            /// <summary>
            /// Gets or sets the message to send to a player that successfully executed the ability.
            /// </summary>
            public string ScanPrepped { get; set; } = "The scan results will be announced in T-$delay seconds.";

            /// <summary>
            /// Gets or sets the announcement to play when the scan is initializing.
            /// </summary>
            public string PreppingScanAnnouncement { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the announcement to play for the scan.
            /// </summary>
            public string ScanAnnouncement { get; set; } = "personnel identification sequence . completed . $ScanResults";

            /// <summary>
            /// Gets or sets a collection of teams and the announcements to insert into <see cref="ScanAnnouncement"/>'s scan result variable.
            /// </summary>
            public Dictionary<Team, string> ScanIdentifiers { get; set; } = new Dictionary<Team, string>
            {
                { Team.CDP, ". $classd ClassD" },
                { Team.RSC, ". $scientists scientists" },
                { Team.MTF, ". $mtf m t f" },
                { Team.CHI, ". $chaos ChaosInsurgency" },
                { Team.SCP, ". $scps ScpSubjects" },
            };
        }
    }
}