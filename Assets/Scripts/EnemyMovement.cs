using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject enemyType;

    private Transform moveTarget;
    private int pathIndex = 0;

    private void Start()
    {
        moveTarget = GameManagerScript.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, moveTarget.position) < 0.1f)
        {
            pathIndex++;
            if (pathIndex >= GameManagerScript.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                moveTarget = GameManagerScript.main.path[pathIndex];
            }

        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (moveTarget.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
}
