using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   
    public void StartGame()
    {
        SceneManager.LoadScene("Jeu");
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}
