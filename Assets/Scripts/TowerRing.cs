using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRing : MonoBehaviour
{
    [SerializeField] private List<BrickBehavior> bricks = new List<BrickBehavior>();

    private bool isHide = false;
    [SerializeField] int colorCount = 4;
    [SerializeField] int brickColoredCount = 4;


    private void Start()
    {
        Init();
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

    private void LateUpdate()
    {
        HideRing();

    }
    private void HideRing()
    {
        bool match = true;
        foreach (var item in bricks)
        {
            if (!item.IsMatch)
            {
                match = false;
                break;
            }
        }
        if(match)
        {
            Destroy(gameObject);
            GameController.Instance.DestroyRing(this);
            isHide = true;
        }
    }

    public List<Color> GetColorArr()
    {
        List<Color> colors = new List<Color>();

        foreach (var item in bricks)
        {
            var col = item.GetComponent<Renderer>().sharedMaterial.color;
            if(col != Color.black)
            colors.Add(col);
        }
        return colors;
    }
}