using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDeterminer : MonoBehaviour
{
    public GameObject gun;
    public Gun gunScript;
    public PlayerMovement playerMovement;
    private Animator playerAnimator;
    public void LoadScene(string sceneName)
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        
    }
    /*public void EnableGun()
    {
        Cursor.visible = true;
        if (playerAnimator != null)
        {
            playerAnimator.updateMode = AnimatorUpdateMode.Normal;
            playerAnimator.enabled = true;
        }

        gun.SetActive(true);
        gunScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        LineRenderer lr = gun.GetComponent<LineRenderer>();
        if (lr != null) lr.enabled = true;
        
    }*/
}