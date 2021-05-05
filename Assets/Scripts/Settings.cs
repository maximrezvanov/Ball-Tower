using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle fxToggle;

    public void ToggleMusic(bool enabled)
    {
        if (enabled)
        {
            SoundController.Instance.OnMusic();
        }
        else
        {
            SoundController.Instance.OffMusic();
        }
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }

    public void ToggleFx(bool enabled)
    {
        if (enabled)
        {
            SoundController.Instance.OnFx();
        }
        else
        {
            SoundController.Instance.OffFx();
        }
    }

    private void Start()
    {
        StartCoroutine(CheckToggleStatus());
    }

    private IEnumerator CheckToggleStatus()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (SoundController.Instance.isMusicOff)
            {
                musicToggle.isOn = false;
            }
            if (SoundController.Instance.isFxOff)
            {
                fxToggle.isOn = false;
            }
        }
    }
}
