using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int colorIndex;
    private Renderer rend;
    private Rigidbody rb;
    [SerializeField] private float lifeTime = 5f;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    public int ColorIndex => colorIndex;

    private void Update()
    {
        DestroyBullet();
        lifeTime -= Time.deltaTime;
    }

    private void DestroyBullet()
    {
        if (!rend.isVisible || transform.position.z > 20f || lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
       
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

}
