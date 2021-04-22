using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> clouds = new List<GameObject>();
    [SerializeField] private Transform spawnerPoint;
    [SerializeField] private float spawnTime = 3f;
    private float yRange; // 9-12
    private float zRange; // 12-16
    private Vector3 spawnPos;

    void Start()
    {
        StartCoroutine(SpawnClouds());
    }

   
    public IEnumerator SpawnClouds()
    {
        while (true)
        {
            yRange = Random.Range(9f, 15f);
            zRange = Random.Range(10f, 16f);
            spawnPos = new Vector3(spawnerPoint.position.x, yRange, zRange);
            int index = Random.Range(0, clouds.Count);
            var cloud = Instantiate(clouds[index], spawnPos, spawnerPoint.rotation);
            yield return new WaitForSeconds(spawnTime);

        }

    }
   
}
