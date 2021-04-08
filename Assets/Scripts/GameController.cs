using System;
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
    private Vector3 startPosition;
    public static GameController Instance;

    private void Awake()
    {
       Instance = this;
    }
    
}
