using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Quaver.API.Maps.Parsers;
using Quaver.Config;
using Wobble.Bindables;

namespace Quaver.Database.Maps
{
    public static class MapManager
    {
        /// <summary>
        ///     The currently selected map.
        /// </summary>
        public static Bindable<Map> Selected { get; set; }

        /// <summary>
        ///     The list of mapsets that are currently loaded.
        /// </summary>
        public static List<Mapset> Mapsets { get; set; }

        /// <summary>
        ///     The osu! Songs folder path
        /// </summary>
        public static string OsuSongsFolder { get; set; }

        /// <summary>
        ///     The Etterna parent folder.
        ///     NOTE: The Map directory themselves have /songs/ already on it.
        ///     Thank you SM guys!
        /// </summary>
        public static string EtternaFolder { get; set; }

        /// <summary>
        ///     The current path of the selected map's audio file
        /// </summary>
        public static string CurrentAudioPath
        {
            get
            {
                switch (Selected.Value.Game)
                {
                    case MapGame.Osu:
                        return OsuSongsFolder + "/" + Selected.Value.Directory + "/" + Selected.Value.AudioPath;
                    case MapGame.Quaver:
                        return ConfigManager.SongDirectory + "/" + Selected.Value.Directory + "/" + Selected.Value.AudioPath;
                    case MapGame.Etterna:
                        return EtternaFolder + "/" + Selected.Value.Directory + "/" + Selected.Value.AudioPath;
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        ///     The current background of the map.
        /// </summary>
        public static Texture2D CurrentBackground { get; set; }

        /// <summary>
        ///     The current path of the selected map's background path.
        /// </summary>
        public static string CurrentBackgroundPath => GetBackgroundPath(Selected.Value);

        /// <summary>
        ///     Gets the background path for a given map.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static string GetBackgroundPath(Map map)
        {
            switch (map.Game)
            {
                case MapGame.Osu:
                    // Parse the map and get the background
                    var osu = new OsuBeatmap(OsuSongsFolder + map.Directory + "/" + map.Path);
                    return $@"{OsuSongsFolder}/{map.Directory}/{osu.Background}";
                case MapGame.Quaver:
                    return ConfigManager.SongDirectory + "/" + map.Directory + "/" + map.BackgroundPath;
                case MapGame.Etterna:
                    return EtternaFolder + "/" + map.Directory + "/" + map.BackgroundPath;
                default:
                    return "";
            }
        }
    }
}