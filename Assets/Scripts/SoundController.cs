using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip destroyRing;
    public AudioClip openedBox;
    public AudioClip collisionSound;
    public AudioClip shootSuperBall;
    public AudioClip clickSound;
    public AudioClip coinSound;
    public static float musicLevel = 0.25f;
    public static float fxLevel = 1.0f;
    public bool isFxOff;
    public bool isMusicOff;
    [SerializeField] private AudioMixerGroup mixer;
    private AudioSource audio;

    public static SoundController Instance
    {
        get
        {
            return instance;
        }
    }

    private static SoundController instance = null;

    public void PlaySound(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    public void OffMusic()
    {
        musicLevel = -80f;
        isMusicOff = true;
        PlayerPrefs.SetInt("music", 0);
    }

    public void OnMusic()
    {
        musicLevel = 0f;
        isMusicOff = false;
        PlayerPrefs.SetInt("music", 1);
    }

    public void OffFx()
    {
        fxLevel = -80f;
        isFxOff = true;
        PlayerPrefs.SetInt("sound", 0);
    }

    public void OnFx()
    {
        fxLevel = 0f;
        isFxOff = false;
        PlayerPrefs.SetInt("sound", 1);
    }

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        Init();
    }

    private void Init()
    {
        StartWithMusic();
        StartWithFx();
    }

    private void Update()
    {
        mixer.audioMixer.SetFloat("Music", musicLevel);
        mixer.audioMixer.SetFloat("Effects", fxLevel);

        if (fxLevel == -80f)
        {
            isFxOff = true;
        }

        if (musicLevel == -80f)
        {
            isMusicOff = true;
        }
    }

    private void StartWithMusic()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            OnMusic();
            PlayerPrefs.SetInt("music", 1);
        }

        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                OffMusic();
            }
        }
    }

    private void StartWithFx()
    {
        if (!PlayerPrefs.HasKey("sound"))
        {
            OnFx();
        }

        else
        {
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                OffFx();
            }
        }
    }
}
