using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Bullet[] bullets;
    [SerializeField] private int bulletsQueueLength = 10;
    public List<Bullet> bulletsQueue = new List<Bullet>();
    [SerializeField] GameObject nextBullet;
    [SerializeField] private List<TowerRing> rings;
    public List<Material> bulletsMaterials = new List<Material>();

    List<Color> bullColList = new List<Color>();

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
        index = (index + 1) % bulletsQueue.Count;
        var nexBull = bulletsQueue[index];
        ChangeMat(nexBull.GetComponent<Renderer>().sharedMaterial);
        DetectedColor();
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

    private void DetectedColor()
    {
        List<Color> brickColList = new List<Color>();

        foreach (var item in rings)
        {
            var colors = item.GetColorArr();
            foreach (var col in colors)
            {
                var color = col;
                brickColList.Add(color);
            }
        }
        bullColList.Clear();
        foreach (var bullets in bulletsQueue)
        {
            var bullMat = bullets.GetComponent<Renderer>().sharedMaterial;
            var bullColor = bullMat.color;

            bullColList.Add(bullColor);
        }

        //Debug.Log(brickColList.Count);
        //Debug.Log(bullColList.Count);

        bullColList = bullColList.Intersect(brickColList).ToList();
        RemoveRedundantBullet();
    }

    private void RemoveRedundantBullet()
    {
        List<Bullet> bulletToRemove = new List<Bullet>();

        foreach (var item in bulletsQueue)
        {
            if (!bullColList.Contains(item.GetComponent<Renderer>().sharedMaterial.color))
            {
                if (!bulletToRemove.Contains(item))
                {
                    bulletToRemove.Add(item);
                }
            }
        }

        foreach (var item in bulletToRemove)
        {
            bulletsQueue.RemoveAll((bullet) => { return bullet == item; });
        }

    }


    //public Bullet SetMaterial(int index)
    //{
    //    bullets[index].GetComponent<Renderer>().material = GameController.Instance.mats[index];

    //    return bullets[index];
    //}


}

