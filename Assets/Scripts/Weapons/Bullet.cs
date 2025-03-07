using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public float lifetime = 5f; // How long the bullet exists before being destroyed
    public string targetTag = "Enemy"; // Tag of the enemy to deal damage
    private Rigidbody rb; // To move the bullet
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing on the bullet!");
            return;
        }

        // Move the bullet forward using its Rigidbody
        rb.linearVelocity = transform.forward * speed;  // Replace .velocity with .linearVelocity

        // Debugging: Check if bullet is moving
        Debug.Log("Bullet moving with linear velocity: " + rb.linearVelocity);

        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit: " + collision.collider.name);
        if (collision.collider.CompareTag(targetTag))
        {
            DmgHp enemy = collision.collider.GetComponent<DmgHp>();
            if (enemy != null)
            {
                enemy.TakeDamageEnemy();
            }
        }
        if (collision.collider.CompareTag("Explosive"))
        {
            PowderKeg keg = collision.collider.GetComponent<PowderKeg>();
            if (keg != null && !keg.exploded)
            {
                keg.Explode(); // Boom 💥
            }
        }
        Destroy(gameObject);
    }
}
