using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    public GameObject lastBullPanel;
    public GameObject losingPanel;
    private float showTime = 3.5f;

    private void Awake()
    {
        Instance = this;
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(false);

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

}
