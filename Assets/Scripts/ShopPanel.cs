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
    [SerializeField] private Text cannonCost;
    [SerializeField] private GameObject coinImage;
    private bool canBuy, isEquip;
    [SerializeField] private bool isStartCannon;
    [SerializeField] private int cannonIndex;
    public static event UnityAction<int> SubtractCannonCost;

    private void Start()
    {
        equipButton.gameObject.SetActive(false);
        Shop.Instance.CannonIndex += OnClickEquipButton;
    }
    private void Update()
    {
        ActivateBuyButton();
    }

    private void ActivateBuyButton()
    {

        if (UIHandler.totalCoins >= int.Parse(cannonCost.text))
        {
            canBuy = true;
            disabledBuyButton.gameObject.SetActive(false);
        }

        if (isStartCannon) isEquip = true;
    }

    public void OnClickEquipButton(int index)
    {
        if(index != cannonIndex && isEquip)
        {
            equipButton.gameObject.SetActive(true);

        }
    }

    public void DeactivateUIElements()
    {
        disabledBuyButton.gameObject.SetActive(false);
        enabledBuyButton.gameObject.SetActive(false);
        cannonCost.gameObject.SetActive(false);
        coinImage.SetActive(false);
    }

    public void BuyCannon()
    {
        int value = int.Parse(cannonCost.text);
        equipButton.gameObject.SetActive(true);
        SubtractCannonCost?.Invoke(value);
        isEquip = true;
    }
    
}
