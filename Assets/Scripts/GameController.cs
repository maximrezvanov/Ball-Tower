using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private List<TowerRing> rings = new List<TowerRing>();
    [SerializeField] private List<GameObject> levelInvironmentPrefabs = new List<GameObject>();
    public List<Material> mats;
    public static GameController Instance;
    private Tower tower;
    public bool isWin = false;
    private Vector3 initPosition = new Vector3(-3.15f, -6f, 20f);
    private int index = 0;
    private void Awake()
    {
       Instance = this;
        ConstructLevel();

    }

    private void Start()
    {
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

   private void ConstructLevel()
   {
        index = SceneManager.GetActiveScene().buildIndex % levelInvironmentPrefabs.Count;
        if(index == SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        var prefab = Instantiate(levelInvironmentPrefabs[index]);

        //prefab.transform.Rotate(0, 90f, 0);

    }

}
