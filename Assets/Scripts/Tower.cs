using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{   
    //[Range(0, 10), SerializeField] private int ringsCount;
     private int ringsCount;

    [SerializeField] TowerRing ringPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform parent;
    public List<TowerRing> rings = new List<TowerRing>();

    private void Start()
    {
        ringsCount = SceneManager.GetActiveScene().buildIndex + 1;

        GetTowerRings();
    }
    public void GetTowerRings()
    {
        for (int i = 0; i < ringsCount; i++)
        {
            Vector3 startPosition = new Vector3(spawnPoint.transform.position.x,
                                    spawnPoint.transform.position.y + 2f + i * 1.5f,
                                    spawnPoint.transform.position.z);
            var towerRing = Instantiate(ringPrefab, startPosition, Quaternion.identity);
            towerRing.transform.SetParent(parent);
            rings.Add(towerRing);
        }

    }


}
