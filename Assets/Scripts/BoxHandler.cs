using System;

using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    public bool isFinishHit = false;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            isFinishHit = true;
        }

    }



}
