using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Public references
    [SerializeField]
    [NotNull]
    private AudioSource audioSource;

    public void PlayClip(AudioClip clip)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(clip);
    }

    public void PlayClipRandom(AudioClip clip, float pitchChangeLimit = 0.1F)
    {
        audioSource.pitch = 1 + UnityEngine.Random.Range(-pitchChangeLimit, pitchChangeLimit);

        audioSource.PlayOneShot(clip);
    }

    public void PlayClipDelayed(AudioClip clip, float delaySeconds)
    {
        StartCoroutine(Sleep(() =>
        {
            PlayClip(clip);
        }, delaySeconds));
    }

    public void PlayClipRandomDelayed(AudioClip clip, float delaySeconds, float pitchChangeLimit = 0.1F)
    {
        StartCoroutine(Sleep(() =>
        {
            PlayClipRandom(clip, pitchChangeLimit);
        }, delaySeconds));
    }

    private IEnumerator Sleep(Action callback, float sleepTimeSeconds)
    {
        yield return new WaitForSeconds(sleepTimeSeconds);

        callback();
    }
}