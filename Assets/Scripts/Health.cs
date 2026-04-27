using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    public Healthbar healthbar;

    private void Start()
    {
        healthbar.DrawHearts();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthbar.DrawHearts();
    }
}