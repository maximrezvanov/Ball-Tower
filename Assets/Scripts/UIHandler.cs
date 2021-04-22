using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    public GameObject lastBullPanel;
    public GameObject losingPanel;


    private void Awake()
    {
        Instance = this;
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(false);

    }

    public void ShowLastBullPanel()
    {
        lastBullPanel.SetActive(true);

    }

    public void ShowlosingPanel()
    {
        lastBullPanel.SetActive(false);
        losingPanel.SetActive(true);

    }


}
