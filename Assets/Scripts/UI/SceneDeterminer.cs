using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDeterminer : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}