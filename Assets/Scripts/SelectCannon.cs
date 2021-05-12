using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCannon : MonoBehaviour
{
    [SerializeField] private List<GameObject> cannonPrefabs = new List<GameObject>();

    public static SelectCannon Instance;

    private void Awake()
    {
        Instance = this;

    }

  

    public void SelCannon(int index)
    {
        foreach (GameObject cannon in cannonPrefabs)
        {
            cannon.SetActive(false);
        }

        cannonPrefabs[index].SetActive(true);
    }
}
