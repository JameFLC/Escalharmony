using Audio.Playlist;
using UnityEngine;

namespace Audio.Loading
{
    public class AudioImporter : MonoBehaviour
    {
        [SerializeField] private string ClipPath = "C:\\Users\\Jame\\Music\\test.mp3";

        // Start is called before the first frame update
        private async void Start()
        {
            var task = PlaylistLoader.ImportPlaylist(ClipPath);

            await task;

            PlaylistData? playlist = task.Result;

            if (playlist is null)
            {
                return;
            }
            
            Debug.Log(playlist.ToString());

            foreach (var track in playlist?.Tracks)
            {
                GetComponent<AudioSource>().PlayOneShot(track.Clip);
                
            }
        }
    }
}