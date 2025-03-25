using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int DamageTaken = 2;
    public int Health;
    public int MaxHealth = 20;
    public GameObject Player;
    public HealthBarController healthBarController;

    private void Start()
    {
        Health = MaxHealth;
        healthBarController.SetMaxHealth(MaxHealth);
    }

    public void TakeDamagePlayer()
    {
        Health -= DamageTaken;
        healthBarController.SetHealth(Health);
        if (Health <= 0)
        {
            Destroy(Player);
            Debug.Log("You Died");
        }
    }

    public void HealPlayer(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth); // Ensure it doesn’t exceed max HP
        healthBarController.SetHealth(Health);
    }
}
