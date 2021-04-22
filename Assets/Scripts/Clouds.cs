using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        DestroyCloud();
    }

    private void DestroyCloud()
    {
        if(!rend.isVisible)
            Destroy(gameObject);
    }
}
