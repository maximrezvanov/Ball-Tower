using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BoxHandler : MonoBehaviour
{
    public static event UnityAction<int> OnCoinCount;
    public int coins;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnPoin;
    [SerializeField] private float timeSpawn;
    private Gun gun;
    private Bullet bullet;
    private TowerRing towerRing;
    private int counter;
    private float flyCoinSpeed = 50000f;

    public void Init()
    {
        coins = SceneController.Instance.ringCounter * 10;
    }

    private void Start()
    {
        gun = FindObjectOfType<Gun>();
        towerRing = FindObjectOfType<TowerRing>();
        StartCoroutine(CanShoot());
        counter = SceneController.Instance.ringCounter * 10;
        coins = counter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet") && !GameController.Instance.isWin && towerRing == null ||
            collision.gameObject.CompareTag("superBall") && !GameController.Instance.isWin && towerRing == null)
        {
            StartCoroutine(GetFinishCoin());
            SoundController.Instance.PlaySound(SoundController.Instance.openedBox);
            particle.Play();
            GameController.Instance.isWin = true;
            Destroy(collision.gameObject);
            OnCoinCount?.Invoke(coins);
            SoundController.Instance.PlaySound(SoundController.Instance.coinSound);
        }
    }

    private IEnumerator CanShoot()
    {
        bool restarted = false;
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            bullet = FindObjectOfType<Bullet>();
            if (!gun.CanShoot() && bullet == null && !GameController.Instance.isWin && !restarted)
            {
                restarted = true;
                SceneController.Instance.RestartLevel();
            }
        }
    }

    private IEnumerator GetFinishCoin()
    {
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.1f);
            counter--;
            var coin = Instantiate(coinPrefab);
            coin.transform.position = coinSpawnPoin.position;
            coin.GetComponent<Rigidbody>().AddForce(Vector3.up * flyCoinSpeed * Time.deltaTime);
        }
    }
}
