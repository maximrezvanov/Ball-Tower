using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button onMusic;
    public Button offMusic;
    public Button onFx;
    public Button offFx;
    public Animator clickStart;
    public Text bestScoreText;
    private ShopPanelHandler shopPanel;

    public void MuteMusic()
    {
        SoundController.Instance.OffMusic();
    }

    public void OnMusic()
    {
        SoundController.Instance.OnMusic();
    }

    public void MuteFx()
    {
        SoundController.Instance.OffFx();
    }

    public void OnFx()
    {
        SoundController.Instance.OnFx();
    }

    public void StartGame()
    {
        clickStart.SetBool("onClickPlayButton", true);
        Invoke("StartFirstLevel", 1.2f);
    }

    public void ShowShopPanel()
    {
       shopPanel.ShowShopPanel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        shopPanel = FindObjectOfType<ShopPanelHandler>();

        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore").ToString();
        }
    }

    private void Start()
    {
        StartCoroutine(CheckSoundButtonStatus());
    }

    private IEnumerator CheckSoundButtonStatus()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (SoundController.Instance.isMusicOff)
            {
               offMusic.gameObject.SetActive(false);
            }
            if (SoundController.Instance.isFxOff)
            {
                offFx.gameObject.SetActive(false);
            }
        }
    }

    private void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}






