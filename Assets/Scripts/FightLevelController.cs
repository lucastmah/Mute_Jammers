using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLevelController : MonoBehaviour
{
    //public GameObject player;
    //list of enemy prefabs for the levels in order 1, 2, 3, ... 
    public GameObject enemy;
    private List<GameObject> enemies = new();
    public int numSpawnsAtOnce = 1;
    public int delayBtwnSpawns = 3;

    public int numEnemiesKilled = 0;
    public int targetKills = 10;

    // Start is called before the first frame update
    void Start()
    {
        /*if (!(enemy != null)) {
            int currLevelIndex = Mathf.Max(0, PowerupsList.GetInstance().currentLevel - 1);
            Instantiate(enemies[currLevelIndex], new Vector3(7, -3, 0), Quaternion.identity);
        }*/
        StartCoroutine(SpawnEnemies(numSpawnsAtOnce, delayBtwnSpawns));
    }

    IEnumerator SpawnEnemies(int numSpawnsAtOnce, float delayBtwnSpawns)
    {
        while(true)
        {
            for(int i = 0; i < numSpawnsAtOnce; i++)
            {
                int xPos = Random.Range(-4, 4);
                int yPos = Random.Range(-1, 2);
                GameObject temp = Instantiate(enemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
                enemies.Add(temp);
            }
            yield return new WaitForSeconds(delayBtwnSpawns);
        }
    }

    public void KillEnemy()
    {
        numEnemiesKilled++;
        if (numEnemiesKilled == targetKills)
        {
            StartWinSequence();
        }
    }

    public void StartWinSequence()
    {
        StopAllCoroutines();
        foreach (GameObject go in enemies)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        Invoke("WinLevel", 3);
    }

    public void WinLevel()
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

    public void LoseLevel()
    {
        SceneLoader.LoadScene("GameOverScene");
    }
}
