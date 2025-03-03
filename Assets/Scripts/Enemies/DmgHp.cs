using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class DmgHp : MonoBehaviour
{
    public int DamageTaken = 2;
    public int Health;
    public int MaxHealth = 20;
    public GameObject Enemies;
    public PlayerHealth playerhealth;
    private void Start()
    {
        Health = MaxHealth;
        StartCoroutine(SustainedDamage());
    }
    public void TakeDamageEnemy()
    {
        Health -= DamageTaken;
        if (Health <= 0)
        {
            Destroy(Enemies);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        playerhealth = other.GetComponent<PlayerHealth>();
        playerhealth.TakeDamagePlayer();
        }
    }
    IEnumerator SustainedDamage()
    {  
        if(CompareTag("Player"))
        {
            
        }
    }
}