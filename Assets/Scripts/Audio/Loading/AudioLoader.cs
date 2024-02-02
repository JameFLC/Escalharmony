using System;
using System.IO;
using System.Threading.Tasks;
using Audio.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Audio.Loading
{
    public static class AudioLoader
    {
        public static async Task<AudioClip?> LoadAudioClipAsync(string audioFilePath)
        {
            AudioClip? clip = null;

            AudioType audioType = AudioUtils.GetTypeFromFile(audioFilePath);

            if (audioType is AudioType.UNKNOWN && File.Exists(audioFilePath))
            {
                return clip;
            }
            
            using (UnityWebRequest? www = UnityWebRequestMultimedia.GetAudioClip(audioFilePath, audioType))
            {
                if (www is null) return clip;

                // Wait manually for the end of the WebRequest  
                {
                    www.SendWebRequest();
                    while (!www.isDone)
                    {
                        await Task.Delay(10);
                    }
                }

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // Can throw a FMOD exception but it cant be caught in try catch here 
                    clip = DownloadHandlerAudioClip.GetContent(www);
                }
            }

            return clip;
        }
    }
}