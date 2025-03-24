using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public KeyCode Pause;
    public GameObject PauseUI;
    public bool isPaused = false;
    public GameObject bullet;
    public GameObject gun;
    public Gun gunScript;
    private Bullet bulletScript;

    void Update()
    {
        if (Input.GetKeyDown(Pause) && !isPaused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            gun.SetActive(false);
            gunScript.enabled = false;
            bulletScript.enabled = false;
            bullet.SetActive(false);
        }
        else if (Input.GetKeyDown(Pause) && isPaused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            gun.SetActive(true);
            gunScript.enabled = true;
            bulletScript.enabled = true;
            bullet.SetActive(true);
        }
    }

    public void ButtonReturn()
    {
        isPaused = false;
        gunScript.enabled = true;
        bulletScript.enabled = true;
        gun.SetActive(true);
        bullet.SetActive(true);
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Off");
    }

}