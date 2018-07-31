using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quaver.Database.Scores;
using Wobble.Screens;

namespace Quaver.Screens.Loading
{
    public class MapLoadingScreen : Screen
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public sealed override ScreenView View { get; protected set; }

        /// <summary>
        ///     The local scores from the leaderboard that'll be used during gameplay.
        /// </summary>
        private List<LocalScore> Scores { get; }

        /// <summary>
        /// </summary>
        public MapLoadingScreen(List<LocalScore> scores)
        {
            Scores = scores;
            View = new MapLoadingScreenView(this);
        }
    }
}
