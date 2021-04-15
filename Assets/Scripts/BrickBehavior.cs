using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private Renderer rend;
    public int colorIndex;
    private Bullet bullet;
    public bool IsMatch = false;


    public int ColorIndex => colorIndex;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        bullet = FindObjectOfType<Bullet>();
    }

    public void SetMaterial(int colorIndex)
    {
        this.colorIndex = colorIndex;
        rend.material = GameController.Instance.mats[colorIndex];
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Renderer>().material.color == rend.material.color &&
            collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            rend.material.color = Color.black;
            IsMatch = true;
        }
    }
}
