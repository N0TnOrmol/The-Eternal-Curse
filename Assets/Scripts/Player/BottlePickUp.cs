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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(false); // Hide prompt when leaving
        }
    }

    private void Update()
    {
        if (PickUpText.activeSelf && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            PickUpBottle();
        }
    }

    void PickUpBottle()
    {
        isPickedUp = true;
        dmController.IncreaseDrunkLevel(); // Activate UI Level + Dizzy Effect
        playerHealth.HealPlayer(5); // Heal the player

        // Hide bottle & UI prompt
        BottleObject.SetActive(false);
        PickUpText.SetActive(false);
    }
}
