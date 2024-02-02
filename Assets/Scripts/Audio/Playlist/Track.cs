using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Track
{
    #region Members

    [SerializeField] public AudioClip Clip;

    [SerializeField] public int Index;

    // Check if it is useful by tracks and not by playlists
    [SerializeField] public bool IsLooping;

    #endregion

    public Track(AudioClip clip, int index, bool isLooping = false)
    {
        Clip = clip;
        Index = index;
        IsLooping = isLooping;
    }
}
