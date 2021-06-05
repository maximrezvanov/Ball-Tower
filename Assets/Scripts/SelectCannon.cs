using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCannon : MonoBehaviour
{
    public static SelectCannon Instance;
    public int cannonIndex;
    [SerializeField] private List<GameObject> cannonPrefabs = new List<GameObject>();

    public void Init()
    {
        cannonPrefabs[cannonIndex].SetActive(true);
        CannonIndexHandler(PlayerPrefs.GetInt("CannonInd"));
    }

    public void CannonIndexHandler(int index)
    {
        for (int i = 0; i < cannonPrefabs.Count; i++)
        {
            if (cannonPrefabs[i] != null)
            {
                cannonPrefabs[i].SetActive(false);
            }
        }

        cannonIndex = index;

        if (cannonPrefabs[cannonIndex] != null)
        {
            cannonPrefabs[cannonIndex].SetActive(true);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
        Shop.OnCannonIndex += CannonIndexHandler;
    }
}
