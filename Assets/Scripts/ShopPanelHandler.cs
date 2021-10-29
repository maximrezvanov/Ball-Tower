using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelHandler : MonoBehaviour
{
    public Animator shopPanel;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("shopPanel");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ShowShopPanel()
    {
        if(shopPanel != null)
        shopPanel.SetBool("isShopPanelHidden", false);
    }

    public void HideShopPanel()
    {
        shopPanel.SetBool("isShopPanelHidden", true);
    }
}
