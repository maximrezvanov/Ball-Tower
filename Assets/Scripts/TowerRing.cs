using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRing : MonoBehaviour
{
    public int coloredCounter = 0;
    [SerializeField] private List<BrickBehavior> bricks = new List<BrickBehavior>();
    [SerializeField] private int colorCount = 4;
    [SerializeField] private int brickColoredCount = 4;
    [SerializeField] private ParticleSystem particle;
    private bool isHide;

    private void Start()
    {
        Init();
        GetColoredCounter();
        StartCoroutine(SuperBallDestroyRing());
    }

    private void Init()
    {
        InitRings();
    }

    private void InitRings()
    {
        for (int i = 0; i < brickColoredCount; i++)
        {
            List<int> indexes = Enumerable.Range(0, bricks.Count).ToList();
            int colorIndex = Random.Range(0, colorCount);
            int brickIndex = Random.Range(0, indexes.Count);
            bricks[brickIndex].SetMaterial(colorIndex);
            indexes.Remove(brickIndex);
        }
    }

    private void GetColoredCounter()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].GetComponent<Renderer>().material.name != "BasicColorMat (Instance)")
            {
                coloredCounter++;
            }
        }
    }

    private void LateUpdate()
    {
        HideRing();
    }

    private void HideRing()
    {
        bool isMatch = true;
        foreach (var item in bricks)
        {
            if (!item.IsMatch)
            {
                isMatch = false;
                break;
            }
        }

        if (isMatch && !isHide)
        {
            StartDestroying();
            SoundController.Instance.PlaySound(SoundController.Instance.destroyRing);
            isHide = true;
        }
    }

    private void StartDestroying()
    {
        particle.Play();
        StartCoroutine(DestroyRing());
    }

    public List<Color> GetColorArr()
    {
        List<Color> colors = new List<Color>();

        foreach (var item in bricks)
        {
            var col = item.GetComponent<Renderer>().sharedMaterial.color;
            if (col != Color.black)
            {
                colors.Add(col);
            }
        }
        return colors;
    }

    public IEnumerator DestroyRing()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        SceneController.Instance.DestroyRing(this);
    }

    private IEnumerator SuperBallDestroyRing()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            foreach (var item in bricks)
            {
                if (item.isSuperBall)
                {
                    StartDestroying();
                }
            }
        }
    }
}


