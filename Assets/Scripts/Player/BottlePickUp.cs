using UnityEngine;

public class BottlePickUp : MonoBehaviour
{
    public DMController dmController;  // Reference to DMController (UI)
    public PlayerHealth playerHealth;  // Reference to player health

    public GameObject PickUpText; // The text that appears when player is near
    public GameObject BottleObject; // The bottle object in the world
    public GameObject pickupEffect; // The effect (e.g., particle system) to disable after pickup

    private bool isPickedUp = false;

    void Start()
    {
        PickUpText.SetActive(false);
        BottleObject.SetActive(true);

        // Optionally, disable the pickup effect initially
        if (pickupEffect != null)
            pickupEffect.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PickUpText.SetActive(true); // Show the pickup prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(false); // Hide the prompt when player leaves the trigger area
        }
    }

    private void Update()
    {
        // Check for pickup input and make sure the bottle isn't already picked up
        if (PickUpText.activeSelf && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            PickUpBottle();
        }
    }

    void PickUpBottle()
    {
        isPickedUp = true;
        dmController.IncreaseDrunkLevel(); // Increase drunk level and apply dizzy effects
        playerHealth.HealPlayer(5); // Heal the player

        // Hide the bottle and the UI pickup prompt
        BottleObject.SetActive(false);
        PickUpText.SetActive(false);

        // Disable the pickup effect after the bottle is picked up
        if (pickupEffect != null)
        {
            pickupEffect.SetActive(false); // Hide the pickup effect
        }
    }
}
