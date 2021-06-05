using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int colorIndex;
    public bool isSuperBall;
    [SerializeField] private float lifeTime = 3.5f;
    [SerializeField] private ParticleSystem fireBall;
    private Renderer rend;
    private Rigidbody rb;

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetMaterial(int colorIndex)
    {
        this.colorIndex = colorIndex;
        rend.material = GameController.Instance.mats[colorIndex];
    }

    public void SetColor(Bullet[] bulls, List<Color> colors)
    {
        foreach (var bull in bulls)
        {
            foreach (var col in colors)
            {
                bull.GetComponent<Renderer>().material.color = col;
            }
        }
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DestroyBullet();
        lifeTime -= Time.deltaTime;
        if (isSuperBall)
        {
            EnabledFireBall();
        }
    }

    private void DestroyBullet()
    {
        if (!rend.isVisible || transform.position.z > 20f || lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void EnabledFireBall()
    {
        fireBall.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
    }

}
