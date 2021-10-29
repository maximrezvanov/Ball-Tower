
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform shootPoint;
    public float speed = 10f;
    public int shootBonus = 100;
    public int bullCounter;
    [SerializeField] private float minRotationAngle;
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private float minAngleX;
    [SerializeField] private float maxAngleX;
    [SerializeField] private ParticleSystem shootPs;
    [SerializeField] private float intersectionPoint = 20f;
    private Camera mainCamera;
    private bool isReadyToShoot = true;
    private int count = 0;
    private int countV = 0;
    private Ammo ammo;

    public void Init()
    {
        ammo.Init();
    }

    public bool CanShoot()
    {
        if (ammo.IsEmpty())
        {
            return false;
        }
        return true;
    }

    private void Awake()
    {
        ammo = FindObjectOfType<Ammo>();
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

    private void Update()
    {
        LaunchProjectile();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void LaunchProjectile()
    {
        if (!UIHandler.Instance.isPause)
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
            if (Input.GetMouseButtonDown(0) && isReadyToShoot && !IsPointerOverUIObject())
            {
                Bullet prefab = ammo.GetBullet();
                var bullet = Instantiate(prefab, shootPoint.position, Quaternion.identity);
                bullet.SetVelocity(transform.forward * speed);
                shootPs.Play();
                SoundController.Instance.PlaySound(SoundController.Instance.shootSound);
                if (bullet.CompareTag("superBall"))
                    SoundController.Instance.PlaySound(SoundController.Instance.shootSuperBall);

                StartCoroutine(ReadyToShoot());
                bullCounter++;
            }
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

    private IEnumerator ReadyToShoot()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(0.5f);
        isReadyToShoot = true;
    }

    private bool IsBetween(float start, float end, float mid)
    {
        end = (end - start) < 0.0f ? end - start + 360.0f : end - start;
        mid = (mid - start) < 0.0f ? mid - start + 360.0f : mid - start;
        return (mid < end);
    }
}

