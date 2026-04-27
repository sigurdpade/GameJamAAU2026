using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Timing")]
    public float fireRate = 1f; //seconds between shots

    [Header("Burst Settings")]
    public bool useBurst = false;
    public int burstCount = 3;
    public float burstDelay = 0.2f;

    private bool inRange = false;

    private void Start()
    {
        StartCoroutine(ShootingLoop());
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        inRange = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }

    IEnumerator ShootingLoop()
    {
        while (true)
        {   
            if (!inRange)
            {
                yield return null;
                continue;
            }
            if (useBurst)
            {
                yield return StartCoroutine(FireBurst());
            } else
            {
                Shoot();
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator FireBurst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            Shoot();
            yield return new WaitForSeconds(burstDelay);
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
