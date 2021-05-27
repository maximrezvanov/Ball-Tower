using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text coinCount;

    public static event UnityAction<int> CannonCost;
    public static event UnityAction<int> CannonIndex;
    public static Shop Instance;

    private void Awake()
    {
        Instance = this;
        coinCount.text = UIHandler.totalCoins.ToString();

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount.text = PlayerPrefs.GetInt("CoinCount").ToString();
        }
    }

    private void Update()
    {
        coinCount.text = PlayerPrefs.GetInt("CoinCount").ToString();
    }

    public void SelectCannonModel(int index)
    {
        CannonIndex?.Invoke(index);
        SaveCannon(index);
    }

    public void SaveCannon(int index)
    {
        PlayerPrefs.SetInt(("CannonInd"), index);
    }



}
