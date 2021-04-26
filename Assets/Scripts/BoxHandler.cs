using System;
using System.Collections;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private Gun gun;
    private Bullet bullet;
    private TowerRing towerRing;

    
    private void Start()
    {
        gun = FindObjectOfType<Gun>();
        towerRing = FindObjectOfType<TowerRing>();
        StartCoroutine(CanShoot());
    }


   
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("bullet") && !GameController.Instance.isWin && towerRing == null)
        {
            SoundController.Instance.PlaySound(SoundController.Instance.openedBox);
            particle.Play();
            GameController.Instance.isWin = true;
        
        }

    }

    public IEnumerator CanShoot()
    {
        bool restarted = false;
        while (true)
        {
            yield return new WaitForSeconds(1);
            bullet = FindObjectOfType<Bullet>();
            if (!gun.CanShoot() && bullet == null && !GameController.Instance.isWin && !restarted)
            {
                UIHandler.Instance.ShowlosingPanel();
                restarted = true;
                SceneController.Instance.RestartLevel();
            }


        }
       
    }

}
