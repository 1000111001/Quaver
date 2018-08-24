﻿using Microsoft.Xna.Framework;
using Quaver.API.Enums;
using Quaver.Assets;
using Quaver.Database.Maps;
using Quaver.Graphics;
using Quaver.Graphics.Notifications;
using Quaver.Skinning;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.UI.Buttons;
using Wobble.Input;

namespace Quaver.Screens.Select.UI.MapInfo.Leaderboards.Difficulty
{
    public class LeaderboardDifficultyButton : Button
    {
        /// <summary>
        ///     Reference to the parent difficulty section.
        /// </summary>
        public LeaderboardSectionDifficulty Section { get; }

        /// <summary>
        ///     The associated map with this difficulty button.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        ///     The grade achieved on the map.
        /// </summary>
        public Sprite GradeAchieved { get; private set; }

        /// <summary>
        ///     The name of the difficulty.
        /// </summary>
        public SpriteText DifficultyName { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="map"></param>
        public LeaderboardDifficultyButton(LeaderboardSectionDifficulty section, Map map)
        {
            Section = section;
            Map = map;

            Size = new ScalableVector2(Section.ScrollContainer.Width, 54);
            Tint = Colors.DarkGray;
            Alpha = 0.65f;

            CreateGradeAchieved();
            CreateDifficultyName();
            Clicked += (sender, args) => Section.SelectDifficulty(Section.Mapset, Map);

            Section.ScrollContainer.AddContainedDrawable(this);
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            FadeToColor(Map == MapManager.Selected.Value ? Color.LightBlue : Colors.DarkGray, gameTime.ElapsedGameTime.Milliseconds, 60);
            base.Update(gameTime);
        }

        /// <summary>
        ///     Creates the GradeAchieved text.
        /// </summary>
        private void CreateGradeAchieved() => GradeAchieved = new Sprite
        {
            Parent = this,
            Alignment = Alignment.MidLeft,
            Size = new ScalableVector2(Height * 0.75f, Height * 0.75f),
            Image = Map.HighestRank == Grade.None ? SkinManager.Skin.Grades[Grade.A] : SkinManager.Skin.Grades[Map.HighestRank],
            X = 8,
        };

        /// <summary>
        ///     Creates the difficulty name text.
        /// </summary>
        private void CreateDifficultyName()
        {
            DifficultyName = new SpriteText(Fonts.AllerRegular16, Map.DifficultyName)
            {
                Parent = this,
                TextScale = 0.65f,
                TextColor = Color.White,
                Alignment = Alignment.MidLeft,
            };

            DifficultyName.X += GradeAchieved.X + GradeAchieved.Width + DifficultyName.MeasureString().X / 2f + 8f;
        }

        /// <inheritdoc />
        /// <summary>
        ///     In this case, we only want buttons to be clickable if they're in the bounds of the scroll container.
        /// </summary>
        /// <returns></returns>
        protected override bool IsMouseInClickArea()
        {
            var newRect = Rectangle.Intersect(ScreenRectangle.ToRectangle(), Section.ScrollContainer.ScreenRectangle.ToRectangle());
            return GraphicsHelper.RectangleContains(newRect, MouseManager.CurrentState.Position);
        }
    }
}