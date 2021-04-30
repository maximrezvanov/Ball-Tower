using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    public GameObject lastBullPanel;
    public GameObject losingPanel;
    public GameObject bulletsPanel;

    private float showTime = 3.5f;
    [SerializeField] private Text counterText;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(false);
        bulletsPanel.SetActive(true);
    }

    private void Start()
    {
        SceneController.Instance.BullCount += ShowBullCount;
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
       bulletsPanel.SetActive(false);
    }

    public IEnumerator HidePanel(GameObject panel)
    {
        yield return new WaitForSeconds(showTime);
       panel.SetActive(false);

    }

    public void ShowBullCount(int number)
    {
        counterText.text = "bullets: " + number.ToString();
    }
}
