using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Button disabledBuyButton;
    [SerializeField] private Button enabledBuyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Text cannonCostText;
    [SerializeField] private int numberOfCoins;
    [SerializeField] private GameObject coinImage;
    private bool canBuy, canBeEquip;
    [SerializeField] private bool isStartCannon;
    [SerializeField] private int cannonIndex;

    public static event UnityAction<int> SubtractCannonCost;

    private void Start()
    {
        equipButton.gameObject.SetActive(false);
        Shop.CannonIndex += UpdateStatusEquipButton;
        numberOfCoins = PlayerPrefs.GetInt("CoinCount");
        cannonCostText.text = numberOfCoins.ToString();
    }

    private void Update()
    {
        ActivateBuyButton();
    }

    private void ActivateBuyButton()
    {
        if (UIHandler.totalCoins >= numberOfCoins)
        {
            canBuy = true;
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
    }
    
}
