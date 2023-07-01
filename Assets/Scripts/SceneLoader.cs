using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public static void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Debug.Log("Application has been quit. This will be ignored in the editor.");
        Application.Quit();
    }
}
