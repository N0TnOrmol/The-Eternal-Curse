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
    private void Start()
    {
        Health = MaxHealth;
    }
    public void TakeDamage ()
    {
        Health -= DamageTaken;
        if (Health <= 0)
        {
            Destroy(Enemies);
        }
    }
}