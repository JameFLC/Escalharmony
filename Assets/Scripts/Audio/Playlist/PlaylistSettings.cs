namespace Audio.Playlist
{
    public struct PlaylistSettings
    {
        public float AttackDuration;

        public float ReleaseDuration;

        public bool AudioIsLooping;

        public PlaylistSettings(float attackDuration = 0.1f, float releaseDuration = 0.1f, bool audioIsLooping = false)
        {
            AttackDuration = attackDuration;
            ReleaseDuration = releaseDuration;
            AudioIsLooping = audioIsLooping;
        }
    }
}