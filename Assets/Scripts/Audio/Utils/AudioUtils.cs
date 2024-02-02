using System;
using System.Collections.Generic;
using Audio.Loading;
using UnityEngine;

namespace Audio.Utils
{
    public static class AudioUtils
    {
        #region Private Members

        private static readonly List<Tuple<string, AudioType>> ValidFiletypes = new()
        {
            new Tuple<string, AudioType>(".mp3", AudioType.MPEG),
            new Tuple<string, AudioType>(".wav", AudioType.WAV),
            new Tuple<string, AudioType>(".ogg", AudioType.OGGVORBIS),
            new Tuple<string, AudioType>(".mp2", AudioType.MPEG)
        };

        #endregion
        
        public static AudioType GetTypeFromFile(string audioFilePath)
        {
            foreach (var type in ValidFiletypes)
                if (audioFilePath.EndsWith(type.Item1))
                {
                    return type.Item2;
                }

            return AudioType.UNKNOWN;
        }
    }
}