using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRing : MonoBehaviour
{
    private BrickBehavior brick;
    public List<BrickBehavior> bricks = new List<BrickBehavior>();

    private bool isHide = false;
    public bool IsBasicColor
    {
        get
        {
            return brick.ColorIndex == 0;
        }
    }

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
        List<int> indexes = Enumerable.Range(0, bricks.Count).ToList();
        for (int i = 0; i < bricks.Count; i++)
        {
            BrickBehavior item = (BrickBehavior)bricks[i];
            int index = Random.Range(0, indexes.Count);
            int rnd = indexes[index];
            item.SetMaterial(rnd);
            indexes.Remove(rnd);
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