using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{   
    [HideInInspector]public int ringsCount;
    public List<TowerRing> rings = new List<TowerRing>();
    public int count = 0;
    [SerializeField] private TowerRing ringPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform parent;
    
    public void Init()
    {
        ringsCount = SceneController.Instance.ringCounter + 1;
        GetTowerRings();
        StartCoroutine(CountColoredBricks());
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

    private IEnumerator CountColoredBricks()
    {
        yield return new WaitForEndOfFrame();

        foreach (var item in rings)
        {
            count += item.coloredCounter;
        }
    }
}
