using UnityEngine;

public class BottlePickUp : MonoBehaviour
{
    public DMController dmController;  // Reference to DMController (UI)
    public PlayerHealth playerHealth;  // Reference to player health

    public GameObject PickUpText;
    public GameObject BottleObject;

    private bool isPickedUp = false;

    void Start()
    {
        PickUpText.SetActive(false);
        BottleObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PickUpText.SetActive(true); // Show prompt
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpBottle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(false); // Hide prompt when leaving
        }
    }

    void PickUpBottle()
    {
        isPickedUp = true;
        dmController.IncreaseDrunkLevel(); // Activate UI Level
        playerHealth.HealPlayer(5); // Heal the player

        // Hide bottle & UI prompt
        BottleObject.SetActive(false);
        PickUpText.SetActive(false);
    }
}
