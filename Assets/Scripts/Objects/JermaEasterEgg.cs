using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JermaEasterEgg : MonoBehaviour
{
    public GameObject pressEUI; // Assign the "Press E" UI GameObject in the Inspector
    private bool playerInRange = false;

    void Start()
    {
        if (pressEUI != null)
        {
            pressEUI.SetActive(false); // Hide the UI initially
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("JermaScene"); // Replace with your actual scene name
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEUI != null)
            {
                pressEUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEUI != null)
            {
                pressEUI.SetActive(false);
            }
        }
    }
}
