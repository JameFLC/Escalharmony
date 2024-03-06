using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio.Playlist
{
    /// <summary>
    ///     All data for any playlist including: its settings for the mixer, its attack and release timing, etc.
    /// </summary>
    [Serializable]
    public struct PlaylistData
    {
        public string Name;

        public string Path;

        public List<Track> Tracks;

        public PlaylistSettings Settings;

        public PlaylistData(string name, string path, List<Track> tracks, PlaylistSettings settings)
        {
            Name = name;
            Path = path;
            Tracks = tracks;
            Settings = settings;
        }

        public override string ToString()
        {
            return $"Playlist \"{Name}\" \n" +
                   $"Stored at \"{Path}\" \n" +
                   $"Containing {Tracks.Count} tracks\n" +
                   "Using these settings:\n" +
                   $"{JsonUtility.ToJson(Settings, true)}";
        }
    }
}