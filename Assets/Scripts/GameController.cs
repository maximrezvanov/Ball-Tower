using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<TowerRing> rings = new List<TowerRing>();
    public List<Material> mats;
    public static GameController Instance;
    private BoxHandler box;
    [SerializeField] int minRingsToWin = 4;

    private void Awake()
    {
       Instance = this;
    }

    private void Start()
    {
        box = FindObjectOfType<BoxHandler>(); 
    }

    private void Update()
    {
        FinishLevel();
    }

    private void FinishLevel()
    {
        OpenTheBoxToWin();
    }

    private void OpenTheBoxToWin()
    {
        if(box.isFinishHit)
            SceneController.I.LoadLevel();
    }

    private void DestroyRingsToWin()
    {
        if (rings.Count <= minRingsToWin)
        {
            SceneController.I.LoadLevel();
        }
    }

    public void DestroyRing(TowerRing ring)
    {
        rings.Remove(ring);
    }

    
}
