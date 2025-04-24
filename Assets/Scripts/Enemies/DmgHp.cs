using System.Collections;
using UnityEngine;

public class DmgHp : MonoBehaviour
{
    [Tooltip("Damage taken by the enemy")]
    public int DamageTaken = 2;
    [Tooltip("Damage dealt by the enemy")]
    public int DamageDealt = 2;
    public int Health;
    public int MaxHealth = 2;
    [Tooltip("Enemy entity")]
    public GameObject Enemies;
    [Tooltip("Allows connection to functionality located in WaveSystem")]
    public WaveSystem waveSpawner;
    [Tooltip("Identifier relating to spawn origins")]
    public string SpawnOrigin;
    [Tooltip("HP bar controller")]
    public EnemyHPBarController enemyHPBarController;
    [Tooltip("Booleon showing if enemy entity is allowed to attack")]
    private bool attack = true;

    // Animator reference to trigger animations
    public Animator animator;

    private void Start()
    {
        Health = MaxHealth;
        // Get the Animator component
        animator = GetComponent<Animator>();
        enemyHPBarController.SetMaxHealth(MaxHealth);
    }

    IEnumerator ContinuousDMG()
    {
        attack = false;
        yield return new WaitForSeconds(5);
        attack = true;
    }

    public void TakeDamageEnemy()
    {
        if (Random.Range(1, 10) > 9)
        {
            Health -= DamageTaken * 2;
            enemyHPBarController.SetHealth(Health);
            if (Health <= 0)
            {
                // Trigger the dead animation before destroying the object
                animator.SetBool("IsDead", true);
                Destroy(gameObject, 1f);  // Delay destruction to let the death animation play
            }
        }
        else
        {
            Health -= DamageTaken;
            enemyHPBarController.SetHealth(Health);
            if (Health <= 0)
            {
                // Trigger the dead animation before destroying the object
                animator.SetBool("IsDead", true);
                Destroy(gameObject, 1f);  // Delay destruction to let the death animation play
                waveSpawner = GameObject.FindGameObjectWithTag(SpawnOrigin).GetComponent<WaveSystem>();
                waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
                
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && attack == true)
        {
            // Trigger the attacking animation
            animator.SetBool("IsAttacking", true);
            other.GetComponent<PlayerHealth>().TakeDamagePlayer();
            StartCoroutine(ContinuousDMG());
            StartCoroutine(ResetAttackAnimation());
        }
    }

    // Reset the "IsAttacking" animation after a short delay
    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Adjust time as needed
        animator.SetBool("IsAttacking", false);
    }
}
