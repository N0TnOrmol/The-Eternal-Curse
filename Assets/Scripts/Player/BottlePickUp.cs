using UnityEngine;

public class BottlePickUp : MonoBehaviour
{
    public DMController dmController; // Reference to the Drunko Meter controller
    public PlayerHealth playerHealth; // Reference to the player health script
    public int healingAmount = 5; // Amount of health restored when drinking a bottle

    private bool isPickedUp = false; // Track if the bottle is picked up or not
    private int pickupCount = 0; // Track the number of times the bottle is picked up

    public GameObject PickUpText; // Text to show when the player is close enough to pick up
    public GameObject BottleObject; // The actual bottle object to be picked up
    public GameObject Player; // The player that is gonna pickup the bottles

    void Start()
    {
        BottleObject.SetActive(true); // Ensure the bottle is enabled at the start
        PickUpText.SetActive(false); // Hide the pick-up text at the start
    }

    void Update()
    {
        // Only show the pickup text if the player is close enough to the bottle
        if (isPickedUp) return;

        // Check for pickup when player presses "E"
        if (Vector3.Distance(transform.position, Player.transform.position) < 3f && !isPickedUp)
        {
            PickUpText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("it works");
                PickUpBottle();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(true); // Show pickup text when the player is in range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(false); // Hide pickup text when the player exits the trigger
        }
    }

    void PickUpBottle()
    {
        // Increment the pickup count each time the bottle is picked up
        pickupCount++;

        // Apply the appropriate Drunko Meter effect based on pickup count (Max 3 levels)
        switch (pickupCount)
        {
            case 1:
                dmController.DLIndex = 1; // Drunk level 1
                break;
            case 2:
                dmController.DLIndex = 2; // Drunk level 2
                break;
            case 3:
                dmController.DLIndex = 3; // Drunk level 3
                break;
            default:
                // We don't go past level 3
                dmController.DLIndex = 3;
                break;
        }

        // Heal the player when they drink the bottle
        playerHealth.HealPlayer(healingAmount);

        // Destroy the bottle after it has been picked up
        isPickedUp = true;
        BottleObject.SetActive(false);
        PickUpText.SetActive(false);
    }
}
