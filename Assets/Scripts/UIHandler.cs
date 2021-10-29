using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [HideInInspector] public static int totalCoins;
    public static UIHandler Instance;
    public GameObject lastBullPanel;
    public GameObject losingPanel;
    public GameObject coinPanel;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject musicOffPanel;
    public GameObject musicOnPanel;
    public GameObject soundOffPanel;
    public GameObject soundOnPanel;
    public bool isPause;
    public Animator settingsPanel;
    public Animator timerAnimator;
    [SerializeField] private Text coinText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text timeIsOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider timeSlider;
    private float showTime = 2.5f;
    ShopPanelHandler shopPanel;

    public void Init()
    {
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(false);
        timeIsOverText.gameObject.SetActive(false);
        timerText.color = Color.white;
        timerAnimator.SetBool("isTimeOver", false);
        timeSlider.maxValue = SceneController.Instance.startTime;
        if (PlayerPrefs.HasKey("CoinCount"))
        {
            totalCoins = PlayerPrefs.GetInt("CoinCount");
        }
        coinText.text = totalCoins.ToString();
    }

    private void ScoreHandler()
    {
        scoreText.text = BrickBehavior.scoreCounter.ToString();
    }

    private void TimerHandler()
    {
        timeSlider.value = SceneController.Instance.timer;
        if (timeSlider.value <= 0)
        {
            timeIsOverText.gameObject.SetActive(true);
        }
        if (timeSlider.value <= 10)
        {
            timerAnimator.SetBool("isTimeOver", true);
            timerText.color = Color.red;
        }
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

    public IEnumerator HidePanel(GameObject panel)
    {
        yield return new WaitForSeconds(showTime);
        panel.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        settingsPanel.SetBool("isHidden", false);
        isPause = true;
    }

    public void HidePauseMenuAndPlay()
    {
        settingsPanel.SetBool("isHidden", true);
        isPause = false;
        StartCoroutine(GetPauseButton());
    }

    public void ShowShopPanel()
    {
        shopPanel.ShowShopPanel();
    }

    public void HideShopPanel()
    {
        shopPanel.HideShopPanel();
    }

    public void GoToMainMenu()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
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

    private void Awake()
    {
        Instance = this;
        shopPanel = FindObjectOfType<ShopPanelHandler>();
    }

    private void Start()
    {
        SceneController.Instance.OnRoundTimer += UpdateTimer;
        BoxHandler.OnCoinCount += CoinCounterHandler;
        ShopPanel.OnSubtractCannonCost += CoinCounterAfterCannonBought;
        StartCoroutine(GetMusicIcon());
        StartCoroutine(GetSoundIcon());
        //scroll.normalizedPosition = new Vector2(scroll.normalizedPosition.y, 1);
        timerAnimator.SetBool("isTimeOver", false);
        timeSlider.maxValue = SceneController.Instance.startTime;
    }

    private void Update()
    {
        TimerHandler();
        ScoreHandler();
        if (totalCoins < 0) totalCoins = 0;
        PlayerPrefs.SetInt("CoinCount", totalCoins);

    }

    private void CoinCounterHandler(int number)
    {
        totalCoins += number;
        coinText.text = totalCoins.ToString();
    }

    private void CoinCounterAfterCannonBought(int number)
    {
        totalCoins -= number;
        coinText.text = totalCoins.ToString();
        PlayerPrefs.Save();
    }

    private void UpdateTimer(int timer)
    {
        timerText.text = timer.ToString();
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

    private IEnumerator GetPauseButton()
    {
        yield return new WaitForSeconds(1f);
        pauseButton.gameObject.SetActive(true);
    }
}
