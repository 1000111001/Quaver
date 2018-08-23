﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Quaver.Config;
using Quaver.Screens.Select.UI.MapInfo.Leaderboards.Scores;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Screens;

namespace Quaver.Screens.Select.UI.MapInfo.Leaderboards
{
    public class Leaderboard : Sprite
    {
        /// <summary>
        ///     Reference to the select screen itself
        /// </summary>
        public SelectScreen Screen { get; }

        /// <summary>
        ///     Reference to the select screen's view.
        /// </summary>
        public SelectScreenView View { get; }

        /// <summary>
        ///    The line that divides the leaderboard section from the content.
        /// </summary>
        public Sprite DividerLine { get; private set; }

        /// <summary>
        ///     The list of leaderboard sections.
        /// </summary>
        public Dictionary<LeaderboardRankingSection, LeaderboardSection> Sections { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="view"></param>
        /// <param name="container"></param>
        public Leaderboard(SelectScreen screen, SelectScreenView view, MapInfoContainer container)
        {
            Screen = screen;
            View = view;

            var banner = container.Banner;
            Size = new ScalableVector2(banner.Width - 2, 372);
            Tint = Color.Black;
            Alpha = 0;

            Y = banner.Y + banner.Height;
            X = banner.X;
            Alignment = Alignment.TopCenter;

            CreateDividerLine();
            CreateSections();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Sections[ConfigManager.SelectLeaderboardSection.Value].Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        ///     The divider line for the leaderboard container.
        /// </summary>
        private void CreateDividerLine() => DividerLine = new Sprite()
        {
            Parent = this,
            Size = new ScalableVector2(Width, 1),
            Y = 25,
            Alpha = 0.25f
        };

        /// <summary>
        ///     Creates the different leaderboard sections.
        /// </summary>
        private void CreateSections()
        {
            Sections = new Dictionary<LeaderboardRankingSection, LeaderboardSection>
            {
                [LeaderboardRankingSection.Local] = new LeaderboardSectionLocal(this),
                [LeaderboardRankingSection.Global] = new LeaderboardSection(LeaderboardRankingSection.Global, this, "Global")
            };

            for (var i = 0; i < Sections.Count; i++)
            {
                var rankingSection = (LeaderboardRankingSection) i;
                var section = Sections[rankingSection];

                if (i > 0)
                {
                    var previousSection = Sections[(LeaderboardRankingSection) i - 1];
                    section.Button.X = previousSection.Button.X + previousSection.Button.Width + 10;
                }
            }
        }

        /// <summary>
        ///     Updates the leaderboard section.
        /// </summary>
        public void UpdateLeaderboard()
        {
            switch (ConfigManager.SelectLeaderboardSection.Value)
            {
                case LeaderboardRankingSection.Local:
                    var localLeaderboard = (LeaderboardSectionLocal) Sections[LeaderboardRankingSection.Local];
                    localLeaderboard.FetchAndUpdateLeaderboards();
                    break;
                // Ignore.
                case LeaderboardRankingSection.Global:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}