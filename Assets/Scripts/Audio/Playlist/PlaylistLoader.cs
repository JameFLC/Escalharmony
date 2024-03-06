using System;
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
        public static async Task<PlaylistData?> LoadPlaylist(string playlistPath)
        {
            var pathInfo = new DirectoryInfo(playlistPath);
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
                var nameIsNumber = int.TryParse(fileName, out var parsedIndex);
                if (!nameIsNumber)
                {
                    continue;
                }
                
                // Try loading the audio clip
                AudioClip? task = await AudioLoader.LoadAudioClipAsync(file.FullName);

                // Check if the AudioClip exist
                if (task is null)
                {
                    continue;
                }

                tracks.TryAdd(parsedIndex, new(task, parsedIndex));
            }
            
            // Check if there is at least track that has been imported
            if (tracks.Count < 1)
            {
                return null;
            }

            // Import settings or give a default one
            PlaylistSettings settings = await LoadPlaylistSettings(pathInfo.FullName) ?? new();
            
            // Setup the playlist for exporting
            var playlist = new PlaylistData(
                pathInfo.Name,
                pathInfo.FullName,
                tracks.Select(e => e.Value).ToList(),
                settings
                );

            return playlist;
        }

        public static async Task<PlaylistSettings?> LoadPlaylistSettings(string playlistPath, string settingsName = "settings")
        {
            var settingsPath = $"{playlistPath}\\{settingsName}.json";

            if (!File.Exists(settingsPath))
            {
                return null;
            }
            
            var settingsText = await File.ReadAllTextAsync(settingsPath);
            try
            {
                return JsonUtility.FromJson<PlaylistSettings>(settingsText);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
    }
}