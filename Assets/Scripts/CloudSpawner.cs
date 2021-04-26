using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> clouds = new List<GameObject>();
    [SerializeField] private Transform spawnerPoint;
    [SerializeField] private float spawnTime = 3f;
    private float yRange; 
    private float zRange; 
    private Vector3 spawnPos;

    void Start()
    {
        StartCoroutine(SpawnClouds());
    }

   
    public IEnumerator SpawnClouds()
    {
        while (true)
        {
            yRange = Random.Range(spawnerPoint.position.y, spawnerPoint.position.y + 3);
            zRange = Random.Range(spawnerPoint.position.z, spawnerPoint.position.z + 4);
            spawnPos = new Vector3(spawnerPoint.position.x, yRange, zRange);
            int index = Random.Range(0, clouds.Count);
            var cloud = Instantiate(clouds[index], spawnPos, spawnerPoint.rotation);
            yield return new WaitForSeconds(spawnTime);

        }

    }
   
}
