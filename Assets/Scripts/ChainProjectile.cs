using UnityEngine;

public class ChainProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int maxBounces = 3;
    public float searchRadius = 5f;

    public int damage = 10; // Damage you can change in Inspector

    private int bounceCount = 0;
    private Transform currentTarget;

    void Start()
    {
        // Automatically find the first enemy
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            currentTarget = enemy.transform;
        }
    }

    void Update()
    {
        if (currentTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            HitTarget();
        }
    }
void HitTarget()
{
    // Deal damage
    Enemy enemy = currentTarget.GetComponent<Enemy>();
    if (enemy != null)
    {
        enemy.health -= damage;
    }

    bounceCount++;

    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, searchRadius);

    Transform nextTarget = null;

    foreach (Collider2D col in enemies)
    {
        if (col.CompareTag("Enemy") && col.transform != currentTarget)
        {
            nextTarget = col.transform;
            break;
        }
    }

    if (nextTarget != null && bounceCount < maxBounces)
    {
        currentTarget = nextTarget;
    }
    else
    {
        Destroy(gameObject);
    }
}
}