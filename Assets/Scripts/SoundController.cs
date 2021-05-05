using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip shootSound;
    public AudioClip destroyRing;
    public AudioClip openedBox;
    public AudioClip collisionSound;
    public AudioClip shootSuperBall;
    public AudioClip clickSound;
    public static float musicLevel = 0.25f;
    public static float fxLevel = 1.0f;
    public bool isFxOff;
    public bool isMusicOff;

    [SerializeField] private AudioMixerGroup mixer;


    public static SoundController Instance
    {
        get
        {
            return instance;
        }
    }

    private static SoundController instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
            DontDestroyOnLoad(gameObject);

        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    private void Update()
    {
        mixer.audioMixer.SetFloat("Music", musicLevel);
        mixer.audioMixer.SetFloat("Effects", fxLevel);

        if (fxLevel  == - 80f)
        {
            isFxOff = true;
        }
         
        if (musicLevel == -80f)
        {
            isMusicOff = true;
        }
    }

    public void OffMusic()
    {
        musicLevel = -80f;
        isMusicOff = true;
    }

    public void OffFx()
    {
        fxLevel = -80f;
        isFxOff = true;
    }

    public void OnMusic()
    {
        musicLevel = 0f;
        isMusicOff = false;
    }

    public void OnFx()
    {
        fxLevel = 0f;
        isFxOff = false;
    }
}
