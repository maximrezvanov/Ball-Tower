using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<TowerRing> rings = new List<TowerRing>();
    public List<Material> mats;
    public static GameController Instance;
    [SerializeField] private BoxHandler box;
    private Tower tower;
    public bool isWin = false;

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
    }

    private void OpenTheBoxToWin()
    {
        if(isWin)
            SceneController.Instance.LoadLevel();
    }

    public void DestroyRing(TowerRing ring)
    {
        rings.Remove(ring);
    }


}
