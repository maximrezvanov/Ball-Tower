using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Bullet[] bullets;
    [SerializeField] GameObject nextBullet;

    private void Start()
    {
        
    }

    public Bullet GetBullet()
    {
        int index = Random.Range(0, bullets.Length);
        SetMaterial(index);

        return bullets[index];
    }

    public bool IsEmpty()
    {
        return false;
    }

    public Bullet SetMaterial(int index)
    {
        bullets[index].GetComponent<Renderer>().material = GameController.Instance.mats[index];
        nextBullet.GetComponent<Renderer>().material = GameController.Instance.mats[index];

        return bullets[index];
    }
}
