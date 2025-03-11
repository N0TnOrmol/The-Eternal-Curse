using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public string targetTag = "Enemy";
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!rb)
        {
            Debug.LogError("Bullet Rigidbody missing!");
            return;
        }

        rb.isKinematic = false;
        rb.useGravity = false;

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<NavMeshAgent>() != null)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }

        if (collision.collider.CompareTag(targetTag))
        {
            DmgHp enemy = collision.collider.GetComponent<DmgHp>();
            if (enemy) enemy.TakeDamageEnemy();
        }

        if (collision.collider.CompareTag("Explosive"))
        {
            PowderKeg keg = collision.collider.GetComponent<PowderKeg>();
            if (keg && !keg.exploded) keg.Explode();
        }

        Destroy(gameObject);
    }
}
