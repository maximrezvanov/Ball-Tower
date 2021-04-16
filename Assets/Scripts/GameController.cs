using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<TowerRing> rings = new List<TowerRing>();
    public List<Material> mats;
    public static GameController Instance;
    [SerializeField] private BoxHandler box;
    private Tower tower;

    //[SerializeField] int minRingsToWin = 4;

    private void Awake()
    {
       Instance = this;
    }

    private void Start()
    {
        box = FindObjectOfType<BoxHandler>();
        tower = FindObjectOfType<Tower>();
        rings = tower.rings;
    }

    private void Update()
    {
        FinishLevel();
    }

    private void FinishLevel()
    {
        OpenTheBoxToWin();
        //DestroyRingsToWin();
    }

    private void OpenTheBoxToWin()
    {
        if(box.isFinishHit)
            SceneController.I.LoadLevel();
    }

    //private void DestroyRingsToWin()
    //{
    //    if (rings.Count <= minRingsToWin)
    //    {
    //        SceneController.I.LoadLevel();
    //    }
    //}

    public void DestroyRing(TowerRing ring)
    {
        rings.Remove(ring);
    }


}
