using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public 

    public Transform target;
    public AudioClip hitSound;

    void Update()
    {
        if (target == null)
            Destroy(gameObject);
        return;
    }


}