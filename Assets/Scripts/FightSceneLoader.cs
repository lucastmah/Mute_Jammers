using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneLoader : MonoBehaviour
{
    public LevelLoader ll;
    public void LoadFightScene()
    {
        int currLevel = PowerupsList.GetInstance().currentLevel - 1;
        string levelString = "Level" + currLevel;
        ll.LoadScene(levelString);
        /*switch (currLevel)
        {
            case 1:
                ll.LoadScene("");
                break;
            case 2:
                ll.LoadScene("");
                break;
            case 3:
                ll.LoadScene("");
                break;
            case 4:
                ll.LoadScene("");
                break;
            case 5:
                ll.LoadScene("");
                break;
            case 6:
                ll.LoadScene("");
                break;
            case 7:
                ll.LoadScene("");
                break;
            case 8:
                ll.LoadScene("");
                break;
            case 9:
                ll.LoadScene("");
                break;
            case 10:
                ll.LoadScene("");
                break;
            case 11:
                ll.LoadScene("");
                break;
            default:
                Debug.LogWarning("FightSceneLoader.Start: Invalid scene index passed. Valid range [1,11], passed value = " + currLevel);
                ll.LoadScene("");
                break;
        }*/
    }
}
