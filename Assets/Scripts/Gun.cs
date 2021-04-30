
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public Transform shootPoint;
    public float speed = 10f;
    public TrajectoryRenderer Trajectory;
    [SerializeField] private Ammo ammo;
    [SerializeField] float minRotationAngle;
    [SerializeField] float maxRotationAngle;
    [SerializeField] float minAngleX;
    [SerializeField] float maxAngleX;
    [SerializeField] private ParticleSystem shootPs;
    [SerializeField] float intersectionPoint = 20f;
    private Camera mainCamera;
    private bool isReadyToShoot = true;
    public int shootBonus = 100;

    private int count = 0;
    private int countV = 0;
    public int bullCounter;

    public void Init()
    {
        ammo.Init();
    }

    void Awake()
    {
        mainCamera = Camera.main;
        while (minRotationAngle < 0)
        {
            minRotationAngle += 360;
            count++;
        }
        maxRotationAngle += count * 360;

        while (minAngleX < 0)
        {
            minAngleX += 360;
            countV++;
        }
        maxAngleX += countV * 360;
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
        var look = Quaternion.LookRotation(direction);
        float gunAngle = look.eulerAngles.y;
        float gunAngleX = look.eulerAngles.x;

        if (IsBetween(minRotationAngle, maxRotationAngle, gunAngle))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, gunAngle, transform.rotation.eulerAngles.z);
        }

        if (IsBetween(minAngleX, maxAngleX, gunAngleX))
        {
            transform.rotation = Quaternion.Euler(gunAngleX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        if (!CanShoot())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && isReadyToShoot)
        {
            Bullet prefab = ammo.GetBullet();
            var bullet = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            bullet.SetVelocity(transform.forward * speed);
            shootPs.Play();
            SoundController.Instance.PlaySound(SoundController.Instance.shootSound);
            if(bullet.CompareTag("superBall"))
                SoundController.Instance.PlaySound(SoundController.Instance.shootSuperBall);

            StartCoroutine(ReadyToShoot());
            bullCounter++;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
    //    Vector3 point = mainCamera.transform.position + (mousePosition - mainCamera.transform.position).normalized * intersectionPoint;
    //    Vector3 direction = (point - shootPoint.position).normalized;
    //    Gizmos.DrawSphere(mousePosition, 0.3f);
    //    Gizmos.DrawLine(mainCamera.transform.position, mousePosition);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(mainCamera.transform.position, point);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(shootPoint.position, shootPoint.position + direction * speed);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(shootPoint.position, shootPoint.position + transform.forward * speed);

    //}

    bool IsBetween(float start, float end, float mid)
    {
        end = (end - start) < 0.0f ? end - start + 360.0f : end - start;
        mid = (mid - start) < 0.0f ? mid - start + 360.0f : mid - start;
        return (mid < end);
    }

    public bool CanShoot()
    {
        if (ammo.IsEmpty())
        {
            return false;
        }
        return true;
    }

    public IEnumerator ReadyToShoot()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(0.05f);
        isReadyToShoot = true;
    }

}

