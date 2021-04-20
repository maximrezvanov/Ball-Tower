using System;

using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    public bool isFinishHit = false;
    [SerializeField] private ParticleSystem particle;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            SoundController.Instance.PlaySound(SoundController.Instance.openedBox);
            particle.Play();
            isFinishHit = true;
        
        }

    }

  

}
