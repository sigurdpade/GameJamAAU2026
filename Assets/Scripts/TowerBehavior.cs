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
    public float range = 1f;
    public GameObject rangeIndicator;

    private void Awake()
    {
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.radius = range;
        }

        // Scale the first child to 2 times the range
        if (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.localScale = new Vector3(range * 2f, range * 2f, range * 2f);
        }
    }

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
