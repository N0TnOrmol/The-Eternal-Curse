using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour
{
    public float damage = 10f;  // Amount of damage the weapon deals
    public float swingTime = 0.5f;  // Time it takes to complete the swing
    public float attackRange = 2f;  // Range of the attack (how far the weapon hits)
    private bool isSwinging = false;  // Whether the weapon is currently swinging

    void Start() {}

    void Update()
    {
        // Detect if the player presses the attack button (e.g., left mouse button or a custom input)
        if (Input.GetButtonDown("Attack") && !isSwinging)
        {
            StartCoroutine(SwingWeapon());
        }
    }

    // Swing the weapon
    private IEnumerator SwingWeapon()
    {
        isSwinging = true;
        // Wait for the swing time (if there's an animation, this can sync with it)
        yield return new WaitForSeconds(swingTime);
        // Detect for collisions in the attack range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider enemy in hitEnemies)
        {
            // Only deal damage to enemies (you can add a tag or layer check here)
            if (enemy.CompareTag("Enemy"))
            {
                // Apply damage to the enemy (you'll need to implement the enemy's health system)
                enemy.GetComponent<DmgHp>().TakeDamageEnemy();
            }
        }
        isSwinging = false;
    }

    // Debug the range of the weapon (for visualization)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
