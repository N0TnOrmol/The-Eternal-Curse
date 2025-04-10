using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public KeyCode Pause;
    public GameObject PauseUI;
    public static bool isPaused = false;

    public GameObject gun;
    public Gun gunScript;

    void Update()
    {
        if (Input.GetKeyDown(Pause))
        {
            if (!isPaused)
                PauseGameNow();
            else
                ResumeGameNow();
        }
    }

    public void ButtonReturn()
    {
        ResumeGameNow();
        Debug.Log("ON");
    }

    void PauseGameNow()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        gun.SetActive(false);
        gunScript.enabled = false;
    }

    void ResumeGameNow()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        gun.SetActive(true);
        gunScript.enabled = true;
    }
}
