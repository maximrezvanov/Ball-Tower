using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<TowerRing> rings = new List<TowerRing>();
    public List<Material> mats;
    public static GameController Instance;
    [SerializeField] int minRingsToWin = 4;

    private void Awake()
    {
       Instance = this;
    }


    private void Update()
    {
        FinishLevel();
    }

    private void FinishLevel()
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
