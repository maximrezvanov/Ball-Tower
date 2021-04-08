using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRing : MonoBehaviour
{
    private BrickBehavior brick;
    public List<BrickBehavior> bricks = new List<BrickBehavior>();
    public bool isHide = false;

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
            //gameObject.SetActive(false);
            Destroy(gameObject);
            isHide = true;
        }
    }

    
}