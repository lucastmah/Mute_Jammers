using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLevelController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        if (!(enemy != null)) {
            Instantiate(enemy);
        }
    }

    public void OnEnemyDeath()
    {
        SceneLoader.LoadScene("NextStageScene");
    }

    public void OnPlayerDeath()
    {
        SceneLoader.LoadScene("GameOverScene");
    }
}
