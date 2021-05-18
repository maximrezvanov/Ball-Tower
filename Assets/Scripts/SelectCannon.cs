using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCannon : MonoBehaviour
{
    [SerializeField] private List<GameObject> cannonPrefabs = new List<GameObject>();
    private static int cannonInd;

    public static SelectCannon Instance;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        cannonPrefabs[cannonInd].SetActive(true);
    }

    public void SelCannon(int index)
    {
        foreach (GameObject cannon in cannonPrefabs)
        {
            cannon.SetActive(false);
        }

        cannonPrefabs[index].SetActive(true);
        cannonInd = index;
    }
}
