using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int DamageTaken = 2;
    public int Health;
    public int MaxHealth = 20;
    public GameObject Player;
    public HealthBarController healthBarController;
    public GameObject deathScreen;
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
            Player.SetActive(false);
            Time.timeScale = 0;
            deathScreen.SetActive(true);
        }
    }
        public void HealPlayer(int healAmount)
    {
        Health = Mathf.Clamp(Health + healAmount, 0, MaxHealth);
        healthBarController.SetHealth(Health);
    }
}
