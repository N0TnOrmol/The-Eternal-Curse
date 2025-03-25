using UnityEngine;
using System.Collections;

public class DMController : MonoBehaviour
{
    public int DLIndex = 0;  // This controls the drunk level (0-3)
    public GameObject DL1; // Drunk Level 1 effect
    public GameObject DL2; // Drunk Level 2 effect
    public GameObject DL3; // Drunk Level 3 effect

    void Start()
    {
        // Start with all levels disabled
        DL1.SetActive(false);
        DL2.SetActive(false);
        DL3.SetActive(false);
    }

    void Update()
    {
        // Make sure the Drunko Meter stays between 0 and 3
        DLIndex = Mathf.Clamp(DLIndex, 0, 3);

        // Update which Drunk Level is active based on the DLIndex value
        UpdateDrunkLevel();
    }

    void UpdateDrunkLevel()
    {
        // Activate the correct DL based on the current DLIndex
        if (DLIndex == 1)
        {
            DL1.SetActive(true);
            DL2.SetActive(false);
            DL3.SetActive(false);
        }
        else if (DLIndex == 2)
        {
            DL1.SetActive(true);
            DL2.SetActive(true);
            DL3.SetActive(false);
        }
        else if (DLIndex == 3)
        {
            DL1.SetActive(true);
            DL2.SetActive(true);
            DL3.SetActive(true);
        }
        else
        {
            DL1.SetActive(false);
            DL2.SetActive(false);
            DL3.SetActive(false);
        }
    }

    // Optional: You could have a method for sobering up the player over time
    public IEnumerator SoberUp()
    {
        DLIndex = 0;
        UpdateDrunkLevel();
        yield return new WaitForSeconds(10f);
    }
}
