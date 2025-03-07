using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
public class DmgHp : MonoBehaviour
{
    public int DamageTaken = 2;
    public int DamageDealt = 2;
    public int Health;
    public int MaxHealth = 2;
    private bool attack = true;
    public GameObject Enemies;
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
            Health -= DamageTaken * 2; // Multiply DamageTaken by 2 for a critical hit
            if (Health <= 0)
            {
                Destroy(Enemies);
            }
        }
        else
        {
            Health -= DamageTaken;
            if (Health <= 0)
            {
                Destroy(Enemies);
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