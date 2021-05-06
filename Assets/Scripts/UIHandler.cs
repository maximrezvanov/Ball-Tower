using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    public GameObject lastBullPanel;
    public GameObject losingPanel;
    public GameObject cannonBallsPanel;
    public GameObject coinPanel;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject musicOffPanel;
    public GameObject musicOnPanel;
    public GameObject soundOffPanel;
    public GameObject soundOnPanel;
    private float showTime = 2.5f;
    private static int totalCoins;

    [SerializeField] private Text counterText;
    [SerializeField] private Text coinText;


    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(false);
        cannonBallsPanel.SetActive(true);
    }

    private void Start()
    {
        SceneController.Instance.BullCount += ShowBullCount;
        BoxHandler.CoinCount += CoinCounterHandler;
        StartCoroutine(GetMusicIcon());
        StartCoroutine(GetSoundIcon());
    }

    public void ShowLastBullPanel()
    {
        lastBullPanel.SetActive(true);
        StartCoroutine(HidePanel(lastBullPanel));
    }

    public void ShowlosingPanel()
    {
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(true);
    }

    public void HideBulletsPanel()
    {
        cannonBallsPanel.SetActive(false);
    }

    public IEnumerator HidePanel(GameObject panel)
    {
        yield return new WaitForSeconds(showTime);
        panel.SetActive(false);
    }

    public void ShowBullCount(int number)
    {
        counterText.text = number.ToString();
    }

    private void CoinCounterHandler(int number)
    {
        totalCoins += number;
        coinText.text = totalCoins.ToString();
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void HidePauseMenuAndPlay()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OnMusic()
    {
        SoundController.Instance.OnMusic();
    }

    public void OnFx()
    {
        SoundController.Instance.OnFx();
    }

    public void OffMusic()
    {
        SoundController.Instance.OffMusic();

    }

    public void OffFx()
    {
        SoundController.Instance.OffFx();

    }

    private IEnumerator GetMusicIcon()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (SoundController.Instance.isMusicOff)
            {
                musicOffPanel.gameObject.SetActive(false);
            }
            else
            {
                musicOffPanel.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator GetSoundIcon()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (SoundController.Instance.isFxOff)
            {
                soundOffPanel.gameObject.SetActive(false);
            }
            else
            {
                soundOffPanel.gameObject.SetActive(true);

            }
        }
    }
}
