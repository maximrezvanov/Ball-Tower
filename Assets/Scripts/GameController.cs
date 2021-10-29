using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [HideInInspector] public bool isSuperBallCollision = false;
    public List<Material> mats;
    public bool isWin = false;
    public bool isShowUI = true;

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
        OpenTheBoxToWin();
    }

    private void OpenTheBoxToWin()
    {
        if (isWin)
        {
            SceneController.Instance.LoadLevel();
        }
    }
}
