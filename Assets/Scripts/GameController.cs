using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private TowerRing ring;
    private BrickBehavior brick;
    private Bullet bullet;

    public List<TowerRing> rings = new List<TowerRing>();
    public List<Material> mats;


    public static GameController Instance;

    private void Awake()
    {
       Instance = this;
    }

    private void Start()
    {
        ring = FindObjectOfType<TowerRing>();
        bullet = FindObjectOfType<Bullet>();
        Init();
    }

    private void Init()
    {
    }



}
