using UnityEngine;

public class PowderKeg : MonoBehaviour
{
    public float explosionRadius = 5f;       // Explosion range
    public float explosionForce = 1000f;     // Knockback force
    public bool exploded = false;           // Explosion flag
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") && !exploded)
        {
            Explode();
        }
    }
    public void Explode()
    {
        if (exploded) return;
        exploded = true;
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider obj in hitObjects)
        {
            if (obj.CompareTag("Player"))
            {
                PlayerHealth player = obj.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.TakeDamagePlayer(); // Deal damage
                }
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (obj.transform.position - transform.position).normalized;
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
        Debug.Log("Boom 💥!");
        Destroy(gameObject); // Destroy the keg
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}