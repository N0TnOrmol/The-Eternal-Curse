using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamagePlayer(); 
        }
    }
}