using UnityEngine;

public class BottlePickUp : MonoBehaviour
{
    public DMController dmController;
    public PlayerHealth playerHealth;
    public GameObject pickUpText;
    public GameObject[] bottles; // array of all 3 bottles
    public GameObject[] pickupEffects; // array of all 3 pickup effects
    private bool isPickedUp = false;
    void Start()
    {
        pickUpText.SetActive(false);
        foreach (var bottle in bottles)
            bottle.SetActive(true);
        foreach (var effect in pickupEffects)
            effect.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            pickUpText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(false);
        }
    }
    private void Update()
    {
        if (pickUpText.activeSelf == true && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            PickUpBottles();
        }
    }
    void PickUpBottles()
    {
        isPickedUp = true;
        dmController.IncreaseDrunkLevel();
        playerHealth.HealPlayer(5);
        pickUpText.SetActive(false);
        foreach (var bottle in bottles)
            bottle.SetActive(false);
        foreach (var effect in pickupEffects)
            effect.SetActive(false);
        Destroy(gameObject, 0.2f);
    }
}
