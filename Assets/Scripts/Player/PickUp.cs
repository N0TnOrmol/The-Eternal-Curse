using UnityEngine;

public class PickUpSaber : MonoBehaviour
{
    public GameObject PickUpText; // Text that prompts the player to pick up the object
    public GameObject ObjectToPickUp; // The actual object that the player picks up
    public GameObject ObjectInHand; // The object that appears in hand after pickup
    public Gun gunScript; // Reference to the gun script, in case you disable it
    public GameObject pickupEffect; // The effect (e.g., particle effect or glow) to disable when picked up

    void Start()
    {
        ObjectToPickUp.SetActive(false);
        PickUpText.SetActive(false);
        
        // Disable the pickup effect initially if you want it off at the start
        if (pickupEffect != null)
            pickupEffect.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(true); // Show pickup text prompt
            if(Input.GetKey(KeyCode.E)) // If the player presses "E"
            {
                // Disable the object and effect, and enable the object in hand
                this.gameObject.SetActive(false); // Disable the item in the world
                ObjectToPickUp.SetActive(true); // Show the item in hand
                PickUpText.SetActive(false); // Hide the pickup prompt
                ObjectInHand.SetActive(false); // Ensure the object in hand is not active at first
                gunScript.enabled = false; // Optionally disable the gun script
                
                // Disable the pickup effect after the object is picked up
                if (pickupEffect != null)
                {
                    pickupEffect.SetActive(false); // Hide the effect
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(false); // Hide the pickup text when player leaves trigger
        }
    }
}
