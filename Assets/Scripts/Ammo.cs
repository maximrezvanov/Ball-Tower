using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Bullet[] bullets;
    private Tower tower;
    [SerializeField] private int bulletsQueueLength = 10;
    public List<Bullet> bulletsQueue = new List<Bullet>();
    [SerializeField] private GameObject nextBullet;
    private List<TowerRing> rings = new List<TowerRing>();
    List<Color> bullColList = new List<Color>();
    public List<Bullet> lastBullets = new List<Bullet>();
    [SerializeField] private GameObject cannonBall;

    private bool isLastBull = false;
    Bullet nextBull;


    int index = 0;

    public bool IsEmpty()
    {
        if (lastBullets.Count == 0)
        {
            return true;
        }
        return false;
    }


    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
        rings = tower.rings;
    }

    private void Start()
    {

        GenerateBulletsQueue();
        StartCoroutine(ChangeCurrentColor());
        InitColors();

    }

    private void InitColors()
    {
        index = (index + 1) % bulletsQueue.Count;
        nextBull = bulletsQueue[index];
        ChangeMat(nextBullet, nextBull.GetComponent<Renderer>().sharedMaterial);

    }

    public Bullet GetBullet()
    {
        if (!isLastBull)
            DetectedColor();

        if (bulletsQueue.Count == 0 && !isLastBull)
        {
            LastShoot();
        }
        if (bulletsQueue.Count == 2)
        {
            UIHandler.Instance.ShowLastBullPanel();
        }

        var bullet = bulletsQueue[index];
        InitColors();
        if (isLastBull)
        {
            bulletsQueue.RemoveAt(0);
        }
        return bullet;
    }


    private void LastShoot()
    {
        index = 0;
        bulletsQueue = lastBullets;
        isLastBull = true;
        cannonBall.SetActive(false);

    }

    public void ChangeMat(GameObject gameObject, Material mat)
    {
        gameObject.GetComponent<Renderer>().material = mat;
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
        }

    }

    public IEnumerator ChangeCurrentColor()
    {

        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (!isLastBull)
            {
                DetectedColor();
                if (bulletsQueue.Count > 0)
                    nextBullet.GetComponent<Renderer>().sharedMaterial = bulletsQueue[index].GetComponent<Renderer>().sharedMaterial;
                else nextBullet.GetComponent<Renderer>().sharedMaterial = lastBullets[0].GetComponent<Renderer>().sharedMaterial;
            }
        }
    }

    //public Bullet SetMaterial(int index)
    //{
    //    bullets[index].GetComponent<Renderer>().material = GameController.Instance.mats[index];

    //    return bullets[index];
    //}


}

