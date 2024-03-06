using Audio.Playlist;
using UnityEngine;

namespace Audio.Loading
{
    
    public class AudioImporter : MonoBehaviour
    {
        [SerializeField] private string ClipPath = "";

        // Start is called before the first frame update
        private async void Start()
        {
            PlaylistData? playlist = await PlaylistLoader.LoadPlaylist(ClipPath);

            if (playlist is null)
                return;
            
            Debug.Log(playlist);    

            var baba = GetComponent<AudioSource>();

            baba.clip = playlist.Value.Tracks[0].Clip;
            baba.Play();
        }
    }
}