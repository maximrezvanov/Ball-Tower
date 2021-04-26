using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    [SerializeField] private float speedRotate;
    [SerializeField] private List<GameObject> hexsList = new List<GameObject>();

   

    private void Update()
    {
        transform.Rotate(Vector3.up * speedRotate);
    }



}
