using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public float lifetime = 5f; // How long the bullet exists before being destroyed
    public string targetTag = "Enemy"; // Tag of the enemy to deal damage

    private Rigidbody rb; // To move the bullet
<<<<<<< Updated upstream
<<<<<<< Updated upstream

=======
    public PauseGame Pause;
>>>>>>> Stashed changes
=======
    public PauseGame Pause;
>>>>>>> Stashed changes
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
        // Debugging: Check if collision happens
        Debug.Log("Bullet hit something: " + collision.collider.name);

        if (collision.collider.CompareTag(targetTag))
        {
            // Try to get the enemy's health script and deal damage
            DmgHp DmgHp = collision.collider.GetComponent<DmgHp>();
            if (DmgHp != null)
            {
                DmgHp.TakeDamageEnemy();
            }
        }

        // Destroy the bullet on any collision (enemy or anything else)
        Destroy(gameObject);
    }
}
