using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fxSlider;

    void Start()
    {
        musicSlider.value = SoundController.musicLevel;
        fxSlider.value = SoundController.fxLevel;
    }


    void Update()
    {
        SoundController.musicLevel = musicSlider.value;
        SoundController.fxLevel = fxSlider.value;
    }
}
