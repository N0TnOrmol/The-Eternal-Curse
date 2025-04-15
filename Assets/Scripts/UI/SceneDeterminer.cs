using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDeterminer : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Cursor.visible = enabled;
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}