using System;
using System.Collections.Generic;

namespace Audio.Playlist
{
    /// <summary>
    /// All data for any playlist including: its settings for the mixer, its attack and release timing, etc. 
    /// </summary>
    [Serializable]
    public struct PlaylistData
    {
        public string Name;

        public string Path;

        public List<Track> Tracks;

        public PlaylistData(string name, string path, List<Track> tracks)
        {
            Name = name;
            Path = path;
            Tracks = tracks;
        }

        public override string ToString()
        {
            return $"Playlist \"{Name}\" stored at \"{Path}\" containing {Tracks.Count} tracks";
        }
    }
}
