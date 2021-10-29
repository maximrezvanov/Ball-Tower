using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static event UnityAction<int> OnCannonIndex;
    public Text coinCount;
    public static Shop Instance;
    public int myMoney;
    public void SelectCannonModel(int index)
    {
        OnCannonIndex?.Invoke(index);
        SaveCannonModel(index);
    }

    public void SaveCannonModel(int index)
    {
        PlayerPrefs.SetInt(("CannonInd"), index);
    }

    private void Awake()
    {
        Instance = this;
        coinCount.text = UIHandler.totalCoins.ToString();

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount.text = PlayerPrefs.GetInt("CoinCount").ToString();
        }

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            myMoney = PlayerPrefs.GetInt("CoinCount");
        }
    }

    private void Update()
    {
        coinCount.text = PlayerPrefs.GetInt("CoinCount").ToString();
    }

}
