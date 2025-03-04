using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public int DamageTaken = 2;
    public int Health;
    public int MaxHealth = 20;
    public GameObject Player;
    private void Start()
    {
        Health = MaxHealth;
    }
    public void TakeDamagePlayer()
    {
        Health -= DamageTaken;
        if (Health <= 0)
        {
            Destroy(Player);
            Debug.Log("You Died");
        }
    }
}
