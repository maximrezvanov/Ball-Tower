using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Bullet[] bullets;

    public Bullet GetBullet()
    {
        int index = Random.Range(0, bullets.Length);
        return bullets[index];
    }

    public bool IsEmpty()
    {
        return false;
    }
}
