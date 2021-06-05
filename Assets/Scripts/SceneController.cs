
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    public event UnityAction<int> OnBullCount;
    public event UnityAction<int> OnRoundTimer;
    public static SceneController Instance;
    public static int bestScore;
    [HideInInspector] public int ringCounter = 1;
    [HideInInspector] public List<TowerRing> rings = new List<TowerRing>();
    [HideInInspector] public int totalBullets = 1;
    [HideInInspector] public float timer;
    [HideInInspector] public float startTime;
    [SerializeField] private List<GameObject> levelInvironmentPrefabs = new List<GameObject>();
    private Tower tower;
    private TowerRotator towerRotator;
    private Gun gun;
    private Ammo ammo;
    private BoxHandler box;
    private CannonBallCountText cannonBallText;
    private BrickBehavior brick;
    private GameObject prevLevelModel;
    private int index;
    private int lastIndex;
    private bool isCountNull;
    private int timeForOneRing = 30;
    private int score;

    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }

    public void LoadLevel()
    {
        StartCoroutine(NextLevel());
    }

    public void DestroyRing(TowerRing ring)
    {
        rings.Remove(ring);
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
        cannonBallText = FindObjectOfType<CannonBallCountText>();
        brick = FindObjectOfType<BrickBehavior>();

        tower.Init();
        towerRotator.Init();
        gun.Init();
        ringCounter++;
        box.Init();
        cannonBallText.Init();

        rings = tower.rings;
        prevLevelModel = levelModel;
        StartCoroutine(CountBull());
        timer = (timeForOneRing - (timeForOneRing * ringCounter * 2 / timeForOneRing)) * ringCounter;
        startTime = timer;
        UIHandler.Instance.Init();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConstructLevel(index);
    }

    private void Update()
    {
        TimerHandler();
        ScoreHandler();
    }

    private void ScoreHandler()
    {
        score = BrickBehavior.scoreCounter;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    private void TimerHandler()
    {
        if (timer > 0 && !UIHandler.Instance.isPause && rings.Count != 0)
        {
            timer -= Time.deltaTime;
            OnRoundTimer?.Invoke((int)timer);
        }
        else if (timer <= 0 && !UIHandler.Instance.isPause)
        {
            timer = 0;
            RestartLevel();
        }
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

    private IEnumerator CountBull()
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
                isCountNull = false;
            }

            if (totalBullets < 0 && !ammo.IsEmptyMainAmmo() && isCountNull)
            {
                UIHandler.Instance.ShowlosingPanel();

                RestartLevel();
            }
            OnBullCount?.Invoke(totalBullets);
        }
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
        brick.ResetScore();
    }

    private IEnumerator NextLevel()
    {
        GameController.Instance.isWin = false;
        StopCoroutine(CountBull());
        yield return new WaitForSeconds(3f);
        if (prevLevelModel != null)
        {
            Destroy(prevLevelModel);
        }

        ConstructRandomLevel();
    }
}
