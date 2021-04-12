
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shootPoint;
    public float speed = 10f;
    public TrajectoryRenderer Trajectory;
    [SerializeField] private Ammo ammo;
    [SerializeField] float minRotationAngle;
    [SerializeField] float maxRotationAngle;
    private int count = 0;
    [SerializeField] float intersectionPoint = 20f;

    [SerializeField]private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        while (minRotationAngle < 0)
        {
            minRotationAngle += 360;
            count++;
        }
        maxRotationAngle += count * 360;
    }

    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
        Vector3 point = mainCamera.transform.position + (mousePosition - mainCamera.transform.position).normalized * intersectionPoint;
        Vector3 direction = (point - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        float gunAngle = transform.rotation.eulerAngles.y;
        if (IsBetween(minRotationAngle, maxRotationAngle, gunAngle))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, gunAngle, transform.rotation.eulerAngles.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Bullet prefab = ammo.GetBullet();
            var bullet = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            bullet.SetVelocity(direction * speed);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
        Vector3 point = mainCamera.transform.position + (mousePosition - mainCamera.transform.position).normalized * intersectionPoint;
        Vector3 direction = (point - shootPoint.position).normalized;
        Gizmos.DrawSphere(mousePosition, 0.3f);
        Gizmos.DrawLine(mainCamera.transform.position, mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mainCamera.transform.position, point);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(shootPoint.position, shootPoint.position + direction * speed);
    }

    bool IsBetween(float start, float end, float mid)
    {
        end = (end - start) < 0.0f ? end - start + 360.0f : end - start;
        mid = (mid - start) < 0.0f ? mid - start + 360.0f : mid - start;
        return (mid < end);
    }

}