using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    private ShopPanel shopPanel;
    private bool isCannonBought;

    public event UnityAction<int> CannonCost;
    public event UnityAction<int> CannonIndex;

    public static Shop Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        shopPanel = FindObjectOfType<ShopPanel>();

    }

    private void Update()
    {
        
    }

    public void SelectCannonModel(int index)
    {
       SelectCannon.Instance.SelCannon(index);
        CannonIndex?.Invoke(index);
    }

}
