using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 25;

    public Transform target;
    public AudioClip hitSound;

    public GameObject hitParticle;

    private void Start()
    {
        target = FindNearestEnemy().transform;
    }

    public GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            Destroy(gameObject);

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    void Update()
    {
        if (target == null)
        { 
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        /*if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }*/

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Only allow damage if enemy is not immune OR this projectile is ProjectileHR
                if (!enemy.isImmune || gameObject.name.Contains("ProjectileHR"))
                {
                    enemy.health -= damage;

                    SoundManager.instance.PlaySFX(hitSound, true);

                    enemy.FlashRed();
                    Instantiate(hitParticle, collision.transform.position, collision.transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }
    /*void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        SoundManager.instance.PlaySFX(hitSound, true);

        Destroy(gameObject);

    }*/

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

}