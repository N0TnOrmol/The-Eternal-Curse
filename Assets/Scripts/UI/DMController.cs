using UnityEngine;

public class DMController : MonoBehaviour
{
    public int DLIndex = 0;

    // Reference to the UI Drunk Levels
    public GameObject DL1;
    public GameObject DL2;
    public GameObject DL3;

    void Start()
    {
        UpdateDrunkUI(); // Ensure UI is updated at start
    }

    public void IncreaseDrunkLevel()
    {
        DLIndex = Mathf.Clamp(DLIndex + 1, 0, 3); // Limit between 0 and 3
        UpdateDrunkUI();
    }

    void UpdateDrunkUI()
    {
        // Activate/deactivate drunk UI elements
        DL1.SetActive(DLIndex >= 1);
        DL2.SetActive(DLIndex >= 2);
        DL3.SetActive(DLIndex >= 3);
    }
}
