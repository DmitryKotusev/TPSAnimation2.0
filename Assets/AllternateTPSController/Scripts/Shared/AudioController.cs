using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;
    AudioSource source;
    bool canPlay = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (!canPlay)
        {
            return;
        }
        GameManager.Instance.Timer.Add(() =>
        {
            canPlay = true;
        },
        delayBetweenClips);
        canPlay = false;
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        source.PlayOneShot(clip);
    }
}
