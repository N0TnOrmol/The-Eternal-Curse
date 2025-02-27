using System;
using UnityEditor.Rendering;
using UnityEngine;

public class DmgHp : MonoBehaviour
{
    public int Damage = 2;
    public int Health;
    public int MaxHealth = 20;
    public GameObject Enemy;
    private void Start()
    {
        Health = MaxHealth;
    }
    public void TakeDamage (int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Destroy(Enemy);
        }
    }
}