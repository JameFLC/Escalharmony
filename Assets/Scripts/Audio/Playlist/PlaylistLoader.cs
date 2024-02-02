using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Audio.Loading;
using Audio.Utils;
using UnityEngine;

namespace Audio.Playlist
{
    public static class PlaylistLoader
    {
        public static async Task<PlaylistData?> ImportPlaylist(string PlaylistPath)
        {
            var pathInfo = new DirectoryInfo(PlaylistPath);
            if (!pathInfo.Exists)
            {
                return null;
            }

            // Create a dictionary to store the tracks, manage order and eventual duplicates
            Dictionary<int, Track> tracks = new();

            foreach (FileInfo? file in pathInfo.EnumerateFiles())
            {
                // Check if the file is an audio file
                if (AudioUtils.GetTypeFromFile(file.FullName) is AudioType.UNKNOWN)
                {
                    continue;
                }

                // Check if the name of the file is a number
                var fileName = Path.GetFileNameWithoutExtension(file.Name);
                var isSuccess = int.TryParse(fileName, out var parsedIndex);
                if (!isSuccess)
                {
                    continue;
                }
                
                // Try loading the audio clip
                Task<AudioClip?> task = AudioLoader.LoadAudioClipAsync(file.FullName);
                await task;

                // Check if the AudioClip exist
                if (task.Result is null)
                {
                    continue;
                }

                tracks.TryAdd(parsedIndex, new(task.Result, parsedIndex));
            }
            
            // Check if there is at least track that has been imported
            if (tracks.Count < 1)
            {
                return null;
            }

            // Setup the playlist for exporting
            var playlist = new PlaylistData(
                pathInfo.Name,
                pathInfo.FullName,
                tracks.Select(e => e.Value).ToList()
                );

            return playlist;
        }
    }
}