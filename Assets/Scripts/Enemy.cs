using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject enemyType;
    [SerializeField] private int health = 100;

    private Transform moveTarget;
    private int pathIndex = 0;

    private void Start()
    {
        moveTarget = GameManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, moveTarget.position) < 0.1f)
        {
            pathIndex++;
            if (pathIndex >= GameManager.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                moveTarget = GameManager.main.path[pathIndex];
                health -= 25;
            }

        }

        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (moveTarget.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
}
