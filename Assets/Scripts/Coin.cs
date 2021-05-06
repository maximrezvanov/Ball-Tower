using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        DestroyCoin();
    }

    private void DestroyCoin()
    {
        if (!rend.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
