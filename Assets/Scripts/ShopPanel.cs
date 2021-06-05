using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public static event UnityAction<int> OnSubtractCannonCost;
    private static string cannonName = String.Empty;
    public string playerPrefString;
    public Text cannonNameText;
    [SerializeField] private Button disabledBuyButton;
    [SerializeField] private Button enabledBuyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Text cannonCostText;
    [SerializeField] private int numberOfCoins;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private int cannonIndex;
    [SerializeField] private bool isStartCannon;
    private bool canBeEquip;
    private int startCannonIndex;
    private List<string> cannonsList = new List<string>();


    public void BuyCannon()
    {
        equipButton.gameObject.SetActive(true);
        OnSubtractCannonCost?.Invoke(numberOfCoins);
        canBeEquip = true;
        DeactivateUIElements();
        SaveCannonName();
    }

    private void Start()
    {
        Shop.OnCannonIndex += OnCannonIndexHandler;
        Init();
    }

    private void Init()
    {
        cannonCostText.text = numberOfCoins.ToString();
        LoadCannonName();

        if (!canBeEquip)
            equipButton.gameObject.SetActive(false);

        if (isStartCannon)
        {
            equipButton.gameObject.SetActive(true);
            canBeEquip = true;
        }

        if (PlayerPrefs.HasKey("CannonInd"))
        {
            startCannonIndex = PlayerPrefs.GetInt("CannonInd");
        }

        if (startCannonIndex == cannonIndex)
        {
            equipButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        ActivateBuyButton();
    }

    private void ActivateBuyButton()
    {
        if (UIHandler.totalCoins >= numberOfCoins || Shop.Instance.myMoney >= numberOfCoins)
        {
            disabledBuyButton.gameObject.SetActive(false);
        }
    }

    private void OnCannonIndexHandler(int index)
    {
        if (index != cannonIndex && canBeEquip)
        {
            equipButton.gameObject.SetActive(true);
        }
    }

    private void DeactivateUIElements()
    {
        disabledBuyButton.gameObject.SetActive(false);
        enabledBuyButton.gameObject.SetActive(false);
        cannonCostText.gameObject.SetActive(false);
        coinImage.SetActive(false);
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
            canBeEquip = true;
        }
    }

    private void StringToList(string message, string seperator)
    {
        string name = String.Empty;
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
