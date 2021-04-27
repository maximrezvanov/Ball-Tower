using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private Renderer rend;
    public int colorIndex = 0;
    private Bullet bullet;
    public bool IsMatch = false;
    private Color basicColor = new Color(0.6517889f, 0.7342701f, 0.9150f);

    public int ColorIndex => colorIndex;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        
    }

    private void Start()
    {
        bullet = FindObjectOfType<Bullet>();
        if (rend.material.name == "BasicColorMat (Instance)")
        {
            IsMatch = true;

        }
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
            SoundController.Instance.PlaySound(SoundController.Instance.collisionSound);
            Destroy(collision.gameObject);
            Debug.Log(rend.material.name);
            rend.material.color = basicColor;
            IsMatch = true;

        }
    }

    public List<BrickBehavior> InitBrick(int brickCount)
    {
        List<BrickBehavior> bricksList = new List<BrickBehavior>();
        BrickBehavior brick = new BrickBehavior();
        
        for (int i = 0; i < brickCount; i++)
        {
            bricksList.Add(brick);
        }
        return bricksList;
    }
}
