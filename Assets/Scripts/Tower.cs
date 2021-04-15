using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int ringsCount;
    [SerializeField] TowerRing ringPrefab;
    [SerializeField] private GameObject spawnPoint;

    private void Start()
    {
        GetTowerRings();
    }
    public void GetTowerRings()
    {

        for (int i = 0; i < ringsCount; i++)
        {
            Vector3 startPosition = new Vector3(spawnPoint.transform.position.x,
                spawnPoint.transform.position.y + 1.5f + i, spawnPoint.transform.position.z);
            var towerRing = Instantiate(ringPrefab, startPosition, Quaternion.identity);
        }

    }


}
