using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    public string enemyId;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject enemyType;
    public int killReward = 10;
    public int health = 100;
    public bool isImmune = false;
    private Transform moveTarget;
    private int pathIndex = 0;

    [Header("Effects")]
    public GameObject deathParticle;
    public GameObject moneyParticle;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    public AudioClip enemyDeathSound;
    public AudioClip damagePlayerSound;

    [Header("Learning Information...")]
    public LearningInformation learningInformation;

    private void Start()
    {
        moveTarget = GameManager.main.path[pathIndex];

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        //FindObjectOfType<LearningPopUp>().TryShowInfo(learningInformation, enemyId);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, moveTarget.position) < 0.1f)
        {
            pathIndex++;
            if (pathIndex >= GameManager.main.path.Length)
            {
                GameObject.Find("GameManager").GetComponent<Health>().TakeDamage(1);
                SoundManager.instance.PlaySFX(damagePlayerSound);
                Destroy(gameObject);
                return;
            }
            else
            {
                moveTarget = GameManager.main.path[pathIndex];
            }

        }

        if (health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<TowerBuilder>().money += killReward;
            GameObject.Find("GameManager").GetComponent<TowerBuilder>().UpdateUI();
            Instantiate(deathParticle, transform.position, transform.rotation);
            Transform playerParticlesPoint = GameObject.Find("PlayerParticlesPoint").transform;
            Instantiate(moneyParticle, playerParticlesPoint.transform.position, playerParticlesPoint.transform.rotation);
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (moveTarget.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    public void FlashRed()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
    }
}
