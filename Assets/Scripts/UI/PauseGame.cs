using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public KeyCode Pause;
    public GameObject PauseUI;
    public static bool isPaused = false;
    public GameObject gun;
    public Gun gunScript;
    public PlayerMovement playerMovement;
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
    void PauseGameNow()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        gun.SetActive(false);
        gunScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (playerAnimator != null)
        {
            playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            playerAnimator.enabled = false;
        }
    }
    void ResumeGameNow()
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
    }
}
