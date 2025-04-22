using UnityEngine;
using System.Collections;

public class PowderKeg : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 1000f;
    public bool exploded = false;
    public ParticleSystem explosionEffect; 

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

        if (explosionEffect != null)
        {
            explosionEffect.gameObject.SetActive(true);
            explosionEffect.Play();
        }

        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider obj in hitObjects)
        {
            // ➤ Damage to player
            if (obj.CompareTag("Player"))
            {
                PlayerHealth player = obj.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.TakeDamagePlayer(); 
                }

                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (obj.transform.position - transform.position).normalized;
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

            // ➤ Damage to enemies
            if (obj.CompareTag("Enemy"))
            {
                DmgHp enemy = obj.GetComponent<DmgHp>();
                if (enemy != null)
                {
                    enemy.TakeDamageEnemy();
                }

                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (obj.transform.position - transform.position).normalized;
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

                if (obj.CompareTag("Enemy"))
            {
                DmgHpFast enemy = obj.GetComponent<DmgHpFast>();
                if (enemy != null)
                {
                    enemy.TakeDamageEnemy();
                }

                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (obj.transform.position - transform.position).normalized;
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        Debug.Log("💥 Boom!");

        StartCoroutine(DestroyAfterDelay(1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
