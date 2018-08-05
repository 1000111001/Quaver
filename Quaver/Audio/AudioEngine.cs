using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quaver.API.Maps;
using Quaver.Config;
using Quaver.Database.Maps;
using Wobble.Audio;
using Wobble.Audio.Tracks;
using Wobble.Graphics;

namespace Quaver.Audio
{
    public static class AudioEngine
    {
        /// <summary>
        ///     The AudioTrack for the currently selected map.
        /// </summary>
        public static AudioTrack Track { get; internal set; }

        /// <summary>
        ///     Loads the track for the currently selected map.
        /// </summary>
        public static void LoadCurrentTrack()
        {
            if (Track != null && !Track.IsDisposed)
                Track.Dispose();

            if (!File.Exists(MapManager.CurrentAudioPath))
                throw new FileNotFoundException($"The audio file at path: {MapManager.CurrentAudioPath} could not be found.");

            Track = new AudioTrack(MapManager.CurrentAudioPath)
            {
                Volume = ConfigManager.VolumeMusic.Value
            };
        }

        /// <summary>
        ///     Seeks to the nearest snap(th) beat in the audio based on the
        ///     current timing point's snap.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="direction"></param>
        /// <param name="snap"></param>
        public static void SeekTrackToNearestSnap(Qua map, Direction direction, int snap)
        {
            if (Track == null || Track.IsDisposed || Track.IsStopped)
                throw new AudioEngineException("Cannot seek to nearest snap if a track isn't loaded");

            if (map == null)
                throw new ArgumentNullException(nameof(map));

            // Get the current timing point
            var point = map.GetTimingPointAt(Track.Time);

            // Get the amount of milliseconds that each snap takes in the beat.
            var snapTimePerBeat = 60000 / point.Bpm / snap;

            // The point in the music that we want to snap to pre-rounding.
            double pointToSnap;

            switch (direction)
            {
                case Direction.Forward:
                    pointToSnap = Track.Time + snapTimePerBeat;
                    break;
                case Direction.Backward:
                    pointToSnap = Track.Time - snapTimePerBeat;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            // Snap the value and seek to it.
            var seekTime = Math.Round((pointToSnap - point.StartTime) / snapTimePerBeat) * snapTimePerBeat + point.StartTime;
            Track.Seek(seekTime);
        }
    }
}
