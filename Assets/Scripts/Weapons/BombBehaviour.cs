using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionDelay = 3f;

    void Start()
    {
        // Make sure it's not active when the bomb spawns
        if (explosionEffect)
        {
            explosionEffect.SetActive(false);
        }

        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        if (explosionEffect)
        {
            explosionEffect.transform.SetParent(null); // Detach from bomb so it can persist
            explosionEffect.SetActive(true);
            Destroy(explosionEffect, 2f); // Optional: clean it up after 2 seconds
        }

        Destroy(gameObject); // Destroy the bomb itself
    }
}
