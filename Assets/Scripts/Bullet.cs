using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int colorIndex;
    private Renderer rend;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        SetMaterial();
    }

    public int ColorIndex => colorIndex;



    public void SetMaterial()
    {

        colorIndex = Random.Range(0, 3);
        rend.material = GameController.Instance.mats[colorIndex];


    }



}
