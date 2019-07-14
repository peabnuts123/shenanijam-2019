using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Public config
    public AudioClip[] musicTracks;

    // Public references
    [NotNull]
    public AudioSource audioSource;

    // Private state
    private bool hasStopped;
    private float timerUntilNextSong;
    private int songIndex;

    void OnEnable()
    {
        hasStopped = true;
        timerUntilNextSong = 0;
    }

    void Update()
    {
        if (hasStopped)
        {
            timerUntilNextSong -= Time.deltaTime;

            if (timerUntilNextSong < 0)
            {
                audioSource.PlayOneShot(GetNextAudioClip());
                hasStopped = false;
            }
        }
        else if (!audioSource.isPlaying)
        {
            hasStopped = true;
            timerUntilNextSong = Random.Range(10F, 30F);
        }
    }

    AudioClip GetNextAudioClip()
    {
        AudioClip clip = this.musicTracks[songIndex];
        this.songIndex = (songIndex + 1) % this.musicTracks.Length;
        return clip;
    }
}
