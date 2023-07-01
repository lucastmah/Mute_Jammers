using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame()
    {
        Debug.Log("Application has been quit. This will be ignored in the editor.");
        Application.Quit();
    }
}