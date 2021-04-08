﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private Renderer rend;
    public int colorIndex;


    public int ColorIndex => colorIndex;


    void Awake()
    {
        rend = GetComponent<Renderer>();
        //rend.material.color = new Color(Random.value, Random.value, Random.value, 1);
    }

    public void SetMaterial(int colorIndex)
    {
        this.colorIndex = colorIndex;
        rend.material = GameController.Instance.mats[colorIndex];
    }

    

}
