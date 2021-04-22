using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip shootSound;
    public AudioClip destroyRing;
    public AudioClip openedBox;
    public AudioClip collisionSound;


    public static SoundController Instance;

    private void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();


    }
}
