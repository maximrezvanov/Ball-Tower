using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public string playerPrefString;
    public Text cannonNameText;
    [SerializeField] private Button disabledBuyButton;
    [SerializeField] private Button enabledBuyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Text cannonCostText;
    [SerializeField] private int numberOfCoins;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private bool isStartCannon;
    [SerializeField] private int cannonIndex;
    private bool canBeEquip;
    private List<string> cannonsList = new List<string>();
    private static string cannonName = "";

    public static event UnityAction<int> SubtractCannonCost;

    private void Start()
    {
        equipButton.gameObject.SetActive(false);
        Shop.CannonIndex += UpdateStatusEquipButton;
        numberOfCoins = PlayerPrefs.GetInt("CoinCount");
        cannonCostText.text = numberOfCoins.ToString();
        LoadCannonName();
    }

    private void Update()
    {
        ActivateBuyButton();

    }

    private void ActivateBuyButton()
    {
        if (UIHandler.totalCoins >= numberOfCoins)
        {
            disabledBuyButton.gameObject.SetActive(false);
        }

        if (isStartCannon) canBeEquip = true;
    }

    public void UpdateStatusEquipButton(int index)
    {
        index = PlayerPrefs.GetInt(("CannonInd"));
        if (canBeEquip)
        {
            equipButton.gameObject.SetActive(true);
        }

    }

    public void DeactivateUIElements()
    {
        disabledBuyButton.gameObject.SetActive(false);
        enabledBuyButton.gameObject.SetActive(false);
        cannonCostText.gameObject.SetActive(false);
        coinImage.SetActive(false);
    }

    public void BuyCannon()
    {
        numberOfCoins = PlayerPrefs.GetInt("CoinCount");
        equipButton.gameObject.SetActive(true);
        SubtractCannonCost?.Invoke(numberOfCoins);
        canBeEquip = true;
        DeactivateUIElements();
        SaveCannonName();
    }

    private void SaveCannonName()
    {
        cannonName = PlayerPrefs.GetString("cannonName");
        cannonName += cannonNameText.text + ",";
        PlayerPrefs.SetString("cannonName", cannonName);
    }

    private void LoadCannonName()
    {
        playerPrefString = PlayerPrefs.GetString("cannonName");
        StringToList(playerPrefString, ",");
        foreach (var item in cannonsList)
        {
            Debug.Log(item);
        }
        if (cannonsList.Contains(cannonNameText.text))
        {
            DeactivateUIElements();
            equipButton.gameObject.SetActive(true);

            canBeEquip = true;
        }
    }

    void StringToList(string message, string seperator)
    {
        string name = "";
        foreach (char character in message)
        {
            name += character;
            if (name.Contains(seperator))
            {
                name = name.Replace(seperator, "");
                cannonsList.Add(name);
                name = "";
            }
        }
    }

}
