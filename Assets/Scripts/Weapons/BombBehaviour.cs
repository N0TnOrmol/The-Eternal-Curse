using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionDuration = 2f;
    public float lifetime = 2f;
    public float explosionRadius = 3f;

    public AudioClip explosionSound; 
    private AudioSource audioSource;

    private bool hasExploded = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Invoke(nameof(Explode), lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        if (explosionEffect != null)
        {
            explosionEffect.transform.SetParent(null);
            explosionEffect.transform.position = transform.position;
            explosionEffect.SetActive(true);
            Destroy(explosionEffect, explosionDuration);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            if (nearby.CompareTag("Enemy"))
            {
                DmgHpFast fastEnemyHP = nearby.GetComponent<DmgHpFast>();
                if (fastEnemyHP != null)
                {
                    fastEnemyHP.TakeDamageEnemy();
                }

                DmgHp enemyHP = nearby.GetComponent<DmgHp>();
                if (enemyHP != null)
                {
                    enemyHP.TakeDamageEnemy();
                }
            }
            else if (nearby.CompareTag("Player"))
            {
                PlayerHealth playerHealth = nearby.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamagePlayer();
                }
            }
        }

        Destroy(gameObject, explosionSound != null ? explosionSound.length : 0.1f);
    }
}
