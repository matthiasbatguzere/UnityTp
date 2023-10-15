using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // ... (autres fonctions du script)

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
