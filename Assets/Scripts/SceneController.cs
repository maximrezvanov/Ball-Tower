
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    public int currentIndex;
    public int ringCounter = 1;
    [SerializeField] private List<GameObject> levelInvironmentPrefabs = new List<GameObject>();
    private List<TowerRing> rings = new List<TowerRing>();
    private Tower tower;
    private TowerRotator towerRotator;
    private Gun gun;
    private Ammo ammo;
    private BoxHandler box;
    private int totalBullets = 1;
    private GameObject prevLevelModel;
    private int index;
    private int lastIndex;
    private bool isCountNull;
    bool restarted = false;

    public event UnityAction<int> BullCount;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConstructLevel(index);
    }

    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }

    public void LoadLevel()
    {
        StartCoroutine(NextLevel());
    }

    private IEnumerator Restart()
    {
        GameController.Instance.isWin = false;
        yield return new WaitForSeconds(2f);
        if (prevLevelModel != null)
        {
            Destroy(prevLevelModel);
            ringCounter -= 1;
        }
        ConstructLevel(index);
    }

    private IEnumerator NextLevel()
    {
        GameController.Instance.isWin = false;
        StopCoroutine(CountBull());
        yield return new WaitForSeconds(3f);
        if (prevLevelModel != null)
            Destroy(prevLevelModel);
        ConstructRandomLevel();

    }

    public void DestroyRing(TowerRing ring)
    {
        rings.Remove(ring);
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
                //Debug.Log("IsEmptyMainAmmo");
                //Debug.Log(isCountNull);

            }

            if (totalBullets < 0 && !ammo.IsEmptyMainAmmo() && isCountNull)
            {
                restarted = true;
                UIHandler.Instance.HideBulletsPanel();
                UIHandler.Instance.ShowlosingPanel();

                RestartLevel();
            }
            BullCount?.Invoke(totalBullets);
        }
    }

    private void ConstructLevel(int index)
    {
        StopAllCoroutines();

        StopCoroutine(Restart());
        var levelModel = Instantiate(levelInvironmentPrefabs[index]);

        tower = FindObjectOfType<Tower>();
        towerRotator = FindObjectOfType<TowerRotator>();
        gun = FindObjectOfType<Gun>();
        ammo = FindObjectOfType<Ammo>();
        box = FindObjectOfType<BoxHandler>();
        tower.Init();
        towerRotator.Init();
        gun.Init();
        ringCounter++;
        box.Init();
        UIHandler.Instance.Init();
        rings = tower.rings;
        prevLevelModel = levelModel;
        StartCoroutine(CountBull());

    }

    private void ConstructRandomLevel()
    {
        lastIndex = index;
        do
        {
            index = Random.Range(0, levelInvironmentPrefabs.Count);
        }
        while (index == lastIndex);
        ConstructLevel(index);
    }

    

}
