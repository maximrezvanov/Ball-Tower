
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletPrefab;
    public Transform shootPoint;
    public float speed = 10f;
    public TrajectoryRenderer Trajectory;



    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit))
        {
            Vector3 forceDirection = CalculateVelocty(hit.point, shootPoint.position, speed);
            //Trajectory.ShowTrajectory(transform.position, forceDirection);
            if (hit.point.z > 1 )
                transform.rotation = Quaternion.LookRotation(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                bullet.AddForce(forceDirection, ForceMode.VelocityChange);

            }
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float speed)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float distanceY = distance.y;
        float directionXz = distanceXz.magnitude;

        float forceDirectionXz = directionXz / speed;
        float gravityForce = (distanceY / speed) + (0.5f * Mathf.Abs(Physics.gravity.y) * speed);

        Vector3 result = distanceXz.normalized;
        result *= forceDirectionXz;
        result.y = gravityForce;

        return result;
    }

}