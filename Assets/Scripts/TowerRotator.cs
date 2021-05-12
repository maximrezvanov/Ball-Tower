using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotator : MonoBehaviour
{
    [SerializeField] private float speedRotation = 30;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        speedRotation += SceneController.Instance.ringCounter;
    }

    void Update()
    {
        if(!UIHandler.Instance.isPause)
        transform.Rotate(0, speedRotation * Time.deltaTime, 0);
    }
}
