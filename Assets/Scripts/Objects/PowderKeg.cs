using UnityEngine;
using System.Collections;

public class PowderKeg : MonoBehaviour
{
    public float explosionRadius = 5f;      // Explosion range
    public float explosionForce = 1000f;    // Knockback force
    public bool exploded = false;          // Explosion flag
    public ParticleSystem explosionEffect; // Reference to explosion particles

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

        // Activate Particles 💨🔥
        if (explosionEffect != null)
        {
            explosionEffect.gameObject.SetActive(true);
            explosionEffect.Play();
        }

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

        // Start Coroutine to Destroy After 1 Second
        StartCoroutine(DestroyAfterDelay(1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
