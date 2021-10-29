using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    public static int scoreCounter;
    public bool isSuperBall;
    public bool IsMatch = false;
    public int colorIndex = 0;
    private Renderer rend;
    private Color basicColor = new Color(0.5538003f, 0.79887f, 0.8962264f);
    
    public void ResetScore()
    {
        scoreCounter = 0;
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

    public void SetMaterial(int colorIndex)
    {
        this.colorIndex = colorIndex;
        rend.material = GameController.Instance.mats[colorIndex];
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetBoolForBsicMaterial();
    }

    private void SetBoolForBsicMaterial()
    {
        if (rend.material.name == "BasicColorMat (Instance)")
        {
            IsMatch = true;
        }
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
            scoreCounter++;
        }

        if (collision.gameObject.GetComponent<Renderer>().material.color == rend.material.color
            && collision.gameObject.CompareTag("superBall"))
        {
            isSuperBall = true;
            Destroy(collision.gameObject);
            Debug.Log("br SuperBall coll");
            scoreCounter += 10;
        }
    }
}
