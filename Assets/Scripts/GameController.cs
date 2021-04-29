using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private List<TowerRing> rings = new List<TowerRing>();
    [SerializeField] private List<GameObject> levelInvironmentPrefabs = new List<GameObject>();
    public List<Material> mats;
    public static GameController Instance;
    private Tower tower;
    private Ammo ammo;
    private Gun gun;
    public bool isWin = false;
    public bool isShowUI = true;
    private Vector3 initPosition = new Vector3(-3.15f, -6f, 20f);
    private static int index = 0;
    private static int lastIndex = 0;
    [HideInInspector] public bool isSuperBallCollision = false;
    private List<int> indexes = new List<int>();
    private int totalBullets = 1;
    private bool isCountNull;

    public event UnityAction<int> BullCount;

    private void Awake()
    {
        Instance = this;
        ConstructLevel();

    }

    private void Start()
    {
        ammo = FindObjectOfType<Ammo>();
        tower = FindObjectOfType<Tower>();
        gun = FindObjectOfType<Gun>();
        rings = tower.rings;
        StartCoroutine(CountBull());

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
        if (isWin)
            SceneController.Instance.LoadLevel();
    }

    public void DestroyRing(TowerRing ring)
    {
        rings.Remove(ring);
    }

    private void ConstructLevel()
    {
        //index = SceneManager.GetActiveScene().buildIndex % levelInvironmentPrefabs.Count
        //if (index == SceneManager.sceneCountInBuildSettings)
        //{
        //    index = 0;
        //}
        lastIndex = index;

        index = (index + Random.Range(0, levelInvironmentPrefabs.Count)) % levelInvironmentPrefabs.Count;

        while (index == lastIndex)
        {
            index = (index + Random.Range(0, levelInvironmentPrefabs.Count)) % levelInvironmentPrefabs.Count;
        }

        var prefab = Instantiate(levelInvironmentPrefabs[index]);


    }

    public IEnumerator CountBull()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);

            totalBullets = tower.count + gun.shootBonus - gun.bullCounter;

            if (!ammo.IsEmptyMainAmmo() && !isCountNull && totalBullets < 0 && rings.Count != 0)
            {
                isCountNull = true;
            }

            if (ammo.IsEmptyMainAmmo() || rings.Count == 0)
            {
                UIHandler.Instance.HideBulletsPanel();
                isCountNull = false;
                Debug.Log("IsEmptyMainAmmo");
                Debug.Log(isCountNull);

            }

            if (isCountNull && !ammo.IsEmptyMainAmmo())
            {
                UIHandler.Instance.HideBulletsPanel();
                UIHandler.Instance.ShowlosingPanel();
                SceneController.Instance.RestartLevel();

            }
            BullCount?.Invoke(totalBullets);
            //Debug.Log("TB " + totalBullets);
        }


    }

}
