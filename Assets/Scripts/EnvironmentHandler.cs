using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    private Renderer rend;
    private Color color;

    public int colorIndex = 0;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        color = rend.material.color;
    }

    void Start()
    {
        colorIndex = Random.Range(0, GameController.Instance.mats.Count);
        SetMaterial(colorIndex);
        color.a = 10f;

    }



    public void SetMaterial(int colorIndex)
    {
        this.colorIndex = colorIndex;
        rend.material = GameController.Instance.mats[colorIndex];
    }
}
