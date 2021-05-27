using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCannon : MonoBehaviour
{
    [SerializeField] private List<GameObject> cannonPrefabs = new List<GameObject>();
    public int cannonInd;

    public static SelectCannon Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Init();
        Shop.CannonIndex += SelCannon;
    }

    public void Init()
    {
        cannonPrefabs[cannonInd].SetActive(true);
        SelCannon(PlayerPrefs.GetInt("CannonInd"));
    }

    public void SelCannon(int index)
    {
        for (int i = 0; i < cannonPrefabs.Count; i++)
        {
            if (cannonPrefabs[i] != null)
                cannonPrefabs[i].SetActive(false);
        }

        cannonInd = index;

        if (cannonPrefabs[cannonInd] != null)
            cannonPrefabs[cannonInd].SetActive(true);



    }
}
