using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    public Healthbar healthbar;

    public Transform playerParticlePoint;
    public GameObject playerHitParticles;

    private void Start()
    {
        healthbar.DrawHearts();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        Instantiate(playerHitParticles, playerParticlePoint.transform.position, playerParticlePoint.transform.rotation);

        healthbar.DrawHearts();

        if (health <= 0)
        {
            SceneManager.LoadScene("Test1Scene");
            //actually have a deathmenu here
        }
    }
}