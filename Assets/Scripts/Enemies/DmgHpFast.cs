using System.Collections;
using UnityEngine;

public class DmgHpFast : MonoBehaviour
{
    [Tooltip("Damage taken by the enemy")]
    public int DamageTaken = 2;

    [Tooltip("Damage dealt by the enemy")]
    public int DamageDealt = 2;

    public int Health;
    public int MaxHealth = 2;

    [Tooltip("Enemy Animator")]
    public Animator animator;

    [Tooltip("Enemy entity")]
    public GameObject Enemies;

    [Tooltip("Allows connection to functionality located in WaveSystem")]
    public WaveSystem waveSpawner;

    [Tooltip("Identifier relating to spawn origins")]
    public string SpawnOrigin;
    [Tooltip("Contrtols enemy hp bar")]
    public EnemyHPBarController enemyHPBarController;

    private bool attack = true;

    private void Start()
    {
        Health = MaxHealth;
        enemyHPBarController.SetMaxHealth(MaxHealth);
    }

    IEnumerator ContinuousDMG()
    {
        attack = false;

        // Trigger attack animation
        if (animator != null)
        {
            animator.SetBool("IsAttacking", true);
            yield return new WaitForSeconds(0.2f); // time to show animation
            animator.SetBool("IsAttacking", false);
        }

        yield return new WaitForSeconds(5); // delay between attacks
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
                Destroy(gameObject);
            }
        }
        else
        {
            Health -= DamageTaken;
            enemyHPBarController.SetHealth(Health);
            if (Health <= 0)
            {
                Destroy(gameObject);
                waveSpawner = GameObject.FindGameObjectWithTag(SpawnOrigin).GetComponent<WaveSystem>();
                waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && attack)
        {
            other.GetComponent<PlayerHealth>().TakeDamagePlayer();
            StartCoroutine(ContinuousDMG());
        }
    }
}
