using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Material> mats;
    public static GameController Instance;
    private BoxHandler box;
    public bool isWin = false;
    public bool isShowUI = true;
    [HideInInspector] public bool isSuperBallCollision = false;

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
            SceneController.Instance.LoadLevel();
    }
}
