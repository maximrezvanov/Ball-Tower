using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int colorIndex;
    private Renderer rend;
    private Rigidbody rb;

    private void Start()
    {
        
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    public int ColorIndex => colorIndex;

    private void Update()
    {
        
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        if (!rend.isVisible)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
    
     



}
