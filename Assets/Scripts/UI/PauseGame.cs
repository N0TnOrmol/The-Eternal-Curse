using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public KeyCode Pause;
    public GameObject PauseUI;
    public bool isPaused = false;

    public GameObject gun;
    public Gun gunScript;


    void Update()
    {
        if (Input.GetKeyDown(Pause) && !isPaused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            gun.SetActive(false);
            gunScript.enabled = false;

        }
        else if (Input.GetKeyDown(Pause) && isPaused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            gun.SetActive(true);
            gunScript.enabled = true;

        }
    }

    public void ButtonReturn()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        gun.SetActive(true);
        gunScript.enabled = true;
        Debug.Log("ON");
    }

}