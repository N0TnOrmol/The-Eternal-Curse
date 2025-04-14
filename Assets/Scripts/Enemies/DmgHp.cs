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
    [Tooltip("Booleon showing if enemy entity is allowed to attack")]
    private bool attack = true;

    private void Start()
    {
        Health = MaxHealth;
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
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Health -= DamageTaken;
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
        if (other.CompareTag("Player") && attack == true)
        {
            other.GetComponent<PlayerHealth>().TakeDamagePlayer();
            StartCoroutine(ContinuousDMG());
        }
    }
}