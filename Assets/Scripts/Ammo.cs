using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Bullet[] bullets;
    [SerializeField] private int bulletsQueueLength = 10;
    public List<Bullet> bulletsQueue = new List<Bullet>();
    [SerializeField] GameObject nextBullet;
    int index = 0;

    public bool IsEmpty()
    {
        return false;
    }

    private void Start()
    {
        GenerateBulletsQueue();
    }

    public Bullet GetBullet()
    {
        var bullet = bulletsQueue[index];
        index++;
        var nexBull = bulletsQueue[index];
        ChangeMat(nexBull.GetComponent<Renderer>().sharedMaterial);
        return bullet;
       
    }

    private void ChangeMat(Material mat)
    {
        nextBullet.GetComponent<Renderer>().material = mat;
    }

    public void GenerateBulletsQueue()
    {
        index = 0;
        for (int i = 0; i < bulletsQueueLength; i++)
        {
            for (int j = 0; j < bullets.Length; j++)
            {
                index = Random.Range(0, bullets.Length);
                bulletsQueue.Add(bullets[index]);
            }
        }
    }



    //public Bullet SetMaterial(int index)
    //{
    //    bullets[index].GetComponent<Renderer>().material = GameController.Instance.mats[index];

    //    return bullets[index];
    //}


}

