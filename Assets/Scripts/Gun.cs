
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletPrefab;
    public GameObject cursor;
    public Transform shootPoint;
    public LineRenderer lineVisual;
    public float flightTime = 1f;
    public TrajectoryRenderer Trajectory;
    private Bullet bullet;



    private Camera mainCamera;
 
    void Start()
    {
        bullet = FindObjectOfType<Bullet>();
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
 
        if (Physics.Raycast(camRay, out hit, 100f))
        {
            //cursor.SetActive(true);
            //cursor.transform.position = hit.point + Vector3.up * 0.1f;
 
            Vector3 forceDirection = CalculateVelocty(hit.point, shootPoint.position, flightTime);
            Trajectory.ShowTrajectory(transform.position, forceDirection);
            transform.rotation = Quaternion.LookRotation(forceDirection);
 
            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                bullet.AddForce(forceDirection, ForceMode.VelocityChange);
            }
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;
 
        float distanceY = distance.y;
        float directionXz = distanceXz.magnitude;
 
        float forceDirectionXz = directionXz / time;
        float gravityForce = (distanceY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);
 
        Vector3 result = distanceXz.normalized;
        result *= forceDirectionXz;
        result.y = gravityForce;
 
        return result;
    }


}
