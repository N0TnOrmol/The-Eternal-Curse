using UnityEngine;

public class DmgHpFast : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    public Transform player;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
        }
        else
        {
            animator.SetBool("IsAttacking", false); // return to idle/move
        }
    }

    void Attack()
    {
        animator.SetBool("IsAttacking", true);
        nextAttackTime = Time.time + attackCooldown;

        // Optional: deal damage, play SFX, etc.
    }
}
