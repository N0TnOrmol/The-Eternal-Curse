using System;
using UnityEditor.Rendering;
using UnityEngine;
public class DmgHp : MonoBehaviour
{
    public int DamageTaken = 2;
    public int DamageDealt = 2;
    public int Health;
    public int MaxHealth = 2;
    public GameObject Enemies;
    private PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        Health = MaxHealth;
    }
    public void TakeDamageEnemy()
    {
        Health -= DamageTaken;
        if (Health <= 0)
        {
            Destroy(Enemies);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamagePlayer();
        }
    }
}