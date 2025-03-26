using UnityEngine;

public class BottlePickUp : MonoBehaviour
{
    // Reference to the DMController UI script
    public DMController dmController; // Make sure to assign this from the UI (Inspector)
    
    // Reference to PlayerHealth to heal the player
    public PlayerHealth playerHealth; // Assign this from the Player's GameObject
    
    // Amount of health restored when the bottle is picked up
    public int healingAmount = 5;

    // Bottle Pickup UI Elements
    public GameObject PickUpText;
    public GameObject BottleObject; // Reference to the actual bottle object

    private bool isPickedUp = false; // If the bottle has been picked up
    private int pickupCount = 0; // Track how many times the bottle was picked up

    void Start()
    {
        // Set the bottle visible and pick-up text hidden initially
        BottleObject.SetActive(true); 
        PickUpText.SetActive(false); 
    }

    void Update()
    {
        // Check if the bottle has been picked up, return if so
        if (isPickedUp) return;

        // Check distance from player to bottle, assuming player and bottle are in the same scene
        if (Vector3.Distance(transform.position, playerHealth.transform.position) < 3f && !isPickedUp)
        {
            PickUpText.SetActive(true); // Show pickup prompt

            // Check if player presses "E" to pick up the bottle
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picking up the bottle!"); // Debugging log
                PickUpBottle(); // Handle picking up the bottle
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // When the player enters the trigger zone of the bottle
        {
            PickUpText.SetActive(true); // Show the pickup text
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // When the player exits the trigger zone of the bottle
        {
            PickUpText.SetActive(false); // Hide the pickup text
        }
    }

    void PickUpBottle()
    {
        // Increment the pickup count and update the Drunk Level accordingly
        pickupCount++;

        switch (pickupCount)
        {
            case 1:
                dmController.DLIndex = 1; // Set Drunk Level 1
                break;
            case 2:
                dmController.DLIndex = 2; // Set Drunk Level 2
                break;
            case 3:
                dmController.DLIndex = 3; // Set Drunk Level 3
                break;
            default:
                dmController.DLIndex = 3; // Keep at level 3 if more than 3 bottles are picked up
                break;
        }

        // Heal the player when they drink the bottle
        playerHealth.HealPlayer(healingAmount);

        // After pickup, deactivate the bottle and hide the text
        isPickedUp = true;
        BottleObject.SetActive(false);
        PickUpText.SetActive(false); // Hide the pickup text
    }
}
