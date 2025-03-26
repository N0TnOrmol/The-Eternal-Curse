using UnityEngine;

public class DMController : MonoBehaviour
{
    public int DLIndex = 0;

    // Reference to the UI Drunk Levels
    public GameObject DL1;
    public GameObject DL2;
    public GameObject DL3;

    // Reference to PlayerMovement for dizzy effect
    public PlayerMovement playerMovement;

    void Start()
    {
        UpdateDrunkUI();
        ApplyDizzyEffect(); // Ensure effect applies at start
    }

    public void IncreaseDrunkLevel()
    {
        DLIndex = Mathf.Clamp(DLIndex + 1, 0, 3);
        UpdateDrunkUI();
        ApplyDizzyEffect();
    }

    void UpdateDrunkUI()
    {
        DL1.SetActive(DLIndex >= 1);
        DL2.SetActive(DLIndex >= 2);
        DL3.SetActive(DLIndex >= 3);
    }

    void ApplyDizzyEffect()
    {
        if (playerMovement == null) return; // Ensure playerMovement is assigned

        switch (DLIndex)
        {
            case 1:
                playerMovement.walkSpeed *= 1.2f; // Slight speed boost
                playerMovement.runSpeed *= 1.2f;
                break;
            case 2:
                playerMovement.walkSpeed *= 0.8f; // Slower movement
                playerMovement.runSpeed *= 0.8f;
                break;
            case 3:
                playerMovement.walkSpeed *= 0.6f;
                playerMovement.runSpeed *= 0.6f;
                playerMovement.transform.Rotate(0, Random.Range(-5f, 5f), 0); // Slight random rotation
                break;
        }
    }
}
