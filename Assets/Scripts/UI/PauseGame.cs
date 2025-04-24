using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public KeyCode Pause;
    public GameObject PauseUI;
    public static bool isPaused = false;
    public int currentWeaponIndex = 0; // Make sure this syncs with your actual weapon system

    public GameObject gun;
    public Gun gunScript;
    public PlayerMovement playerMovement;
    public WeaponUIManager weaponUIManager;
    public WeaponManager weaponManager;

    private Animator playerAnimator;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player has the "Player" tag
        if (player != null)
        {
            playerAnimator = player.GetComponentInChildren<Animator>();
        }
    }

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

    public void PauseGameNow()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        gun.SetActive(false);
        gunScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (weaponUIManager != null)
            weaponUIManager.HideAll();

        if (playerAnimator != null)
        {
            playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            playerAnimator.enabled = false;
        }
    }


    public void ResumeGameNow()
    {
        PauseUI.SetActive(false);
        if (playerAnimator != null)
        {
            playerAnimator.updateMode = AnimatorUpdateMode.Normal;
            playerAnimator.enabled = true;
        }
        Time.timeScale = 1;
        isPaused = false;
        gun.SetActive(true);
        gunScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        LineRenderer lr = gun.GetComponent<LineRenderer>();
        if (lr != null) lr.enabled = true;

        if (weaponUIManager != null)
            weaponUIManager.ShowWeaponUI(currentWeaponIndex); // Show weapon UI again
    }

}
