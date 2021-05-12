using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Bullet[] bullets;
    private Tower tower;
    private BrickBehavior brick;
    [SerializeField] private int bulletsQueueLength = 1;
    [SerializeField] private int superBallsCount = 10;
    public List<Bullet> bulletsQueue = new List<Bullet>();
    public List<Bullet> superBalls = new List<Bullet>();

    [SerializeField] private GameObject nextBullet;
    private List<TowerRing> rings = new List<TowerRing>();
    List<Color> bullColList = new List<Color>();
    public List<Bullet> lastBullets = new List<Bullet>();
    [SerializeField] private GameObject cannonBall;
    private bool isLastBull = false;
    Bullet nextBull;
    private int superBallIndex;
    int index = 0;
    private List<int> superIndexes = new List<int>();

    public bool IsEmpty()
    {
        if (lastBullets.Count == 0)
        {
            return true;
        }
        return false;
    }

    public bool IsEmptyMainAmmo()
    {
        if (bulletsQueue.Count == 0)
        {
            return true;
        }
        return false;
    }


    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
        brick = FindObjectOfType<BrickBehavior>();
        rings = tower.rings;
    }

    public void Init()
    {
        GenerateBulletsQueue();
        StartCoroutine(ChangeCurrentColor());
        GenerateSuperBalls();
    }

    public Bullet GetBullet()
    {
        if(!isLastBull)
        index++;

        //if (!isLastBull)
        //    DetectedColor();

        if (bulletsQueue.Count == 0 && !isLastBull)
        {
            LastShoot();
        }
        if (bulletsQueue.Count == 3)
        {
            UIHandler.Instance.ShowLastBullPanel();
        }
        var bullet = bulletsQueue[index];
        if (!isLastBull)
        {
            nextBull = bulletsQueue[index + 1];
            ChangeMat(nextBullet, nextBull.GetComponent<Renderer>().sharedMaterial);
        }
        
        if (isLastBull)
        {
            bulletsQueue.RemoveAt(0);
        }

        bullet.tag = "bullet";
        if(bullet.tag == "bullet")
            bullet.transform.localScale = new Vector3(1f, 1f, 1f);


        DetectedSuperBall();
        Debug.Log("index " + index);
        return bullet;
    }


    private void LastShoot()
    {
        bulletsQueue = lastBullets;
        index = 0;
        isLastBull = true;
        HideCannonBall();
    }

    public void HideCannonBall()
    {
        cannonBall.SetActive(false);

    }

    public void ChangeMat(GameObject gameObject, Material mat)
    {
        gameObject.GetComponent<Renderer>().material = mat;
    }


    public void GenerateBulletsQueue()
    {
        bulletsQueue.Clear();
        int colIndex = 0;
        for (int i = 0; i < bulletsQueueLength; i++)
        {
            for (int j = 0; j < bullets.Length; j++)
            {
                colIndex = UnityEngine.Random.Range(0, bullets.Length);

                bulletsQueue.Add(bullets[colIndex]);
            }
        }

    }

    public void GenerateSuperBalls()
    {
        superIndexes.Clear();
        for (int i = 0; i < superBallsCount; i++)
        {
            superBallIndex = UnityEngine.Random.Range(0, bulletsQueue.Count);
            superIndexes.Add(superBallIndex);
            Debug.Log(superBallIndex);
        }
    }

    private void DetectedSuperBall()
    {
        for (int i = 0; i < superIndexes.Count; i++)
        {
            if (superIndexes[i] == index)
            {
                bulletsQueue[superIndexes[i]].tag = "superBall";
                bulletsQueue[superIndexes[i]].transform.localScale += new Vector3(1.05f, 1.05f, 1.05f);
            }
        }
    }

    public void DetectedColor()
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
            index = 0;
        }
    }

    public IEnumerator ChangeCurrentColor()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!isLastBull)
            {
                DetectedColor();
                if (bulletsQueue.Count > 0)
                {
                    nextBullet.GetComponent<Renderer>().sharedMaterial = bulletsQueue[index + 1].GetComponent<Renderer>().sharedMaterial;
                }
                else
                {
                    nextBullet.GetComponent<Renderer>().sharedMaterial = lastBullets[0].GetComponent<Renderer>().sharedMaterial;
                }
            }
        }
    }

    //public Bullet SetMaterial(int index)
    //{
    //    bullets[index].GetComponent<Renderer>().material = GameController.Instance.mats[index];

    //    return bullets[index];
    //}


}

