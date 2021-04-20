using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;
    [SerializeField] float speed;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 offset = startPosition - movePosition;
        transform.position = startPosition - offset;
       Mathf.PingPong(Time.time * speed, 1);
    }
}
