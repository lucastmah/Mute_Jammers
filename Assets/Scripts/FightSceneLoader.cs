using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneLoader : MonoBehaviour
{
    public void LoadFightScene()
    {
        int currLevel = PowerupsList.GetInstance().currentLevel;
        switch (currLevel)
        {
            case 1:
                SceneLoader.LoadScene("");
                break;
            case 2:
                SceneLoader.LoadScene("");
                break;
            case 3:
                SceneLoader.LoadScene("");
                break;
            case 4:
                SceneLoader.LoadScene("");
                break;
            case 5:
                SceneLoader.LoadScene("");
                break;
            case 6:
                SceneLoader.LoadScene("");
                break;
            case 7:
                SceneLoader.LoadScene("");
                break;
            case 8:
                SceneLoader.LoadScene("");
                break;
            case 9:
                SceneLoader.LoadScene("");
                break;
            case 10:
                SceneLoader.LoadScene("");
                break;
            case 11:
                SceneLoader.LoadScene("");
                break;
            default:
                Debug.LogWarning("FightSceneLoader.Start: Invalid scene index passed. Valid range [1,11], passed value = " + currLevel);
                SceneLoader.LoadScene("");
                break;
        }
    }
}
