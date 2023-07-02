using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLevelController : MonoBehaviour
{
    public GameObject player;
    //list of enemy prefabs for the levels in order 1, 2, 3, ... 
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        if (!(enemies != null)) {
            int currLevelIndex = Mathf.Max(0, PowerupsList.GetInstance().currentLevel - 1);
            Instantiate(enemies[currLevelIndex], new Vector3(7, -3, 0), Quaternion.identity);
        }
    }

    public void OnEnemyDeath()
    {
        bool[] powerupArray = PowerupsList.GetInstance().GetPowerupArray();
        int numPowerupsLeft = 0;
        foreach (bool powerup in powerupArray) {
            if (powerup)
            {
                numPowerupsLeft++;
            }
        }
        if (numPowerupsLeft == 0)
        {
            SceneLoader.LoadScene("VictoryScene");
        }
        else
        {
            PowerupsList.GetInstance().currentLevel += 1;
            SceneLoader.LoadScene("NextStageScene");
        }
    }

    public void OnPlayerDeath()
    {
        SceneLoader.LoadScene("GameOverScene");
    }
}
