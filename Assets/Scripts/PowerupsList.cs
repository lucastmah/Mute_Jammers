using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsList : MonoBehaviour
{
    //singleton class to store player powerups left 
    static PowerupsList instance;

    public bool hasInvincibility = true;
    public bool hasDoubleJump = true;
    public bool hasDoubleProjectiles = true;
    public bool hasHomingProjectiles = true;
    public bool hasBonusAtk = true;
    public bool hasBonusMvspd = true;
    public bool hasBonusJumpHeight = true;
    public bool hasAcceleration = true;
    public bool hasNoFallDmg = true;

    public int currentLevel = 1;

    private bool[] powerupArray;

    [SerializeField] private AudioSource enemyDeathSource;
    [SerializeField] private AudioSource enemyHurtSource;
    [SerializeField] private AudioSource debuffSource;

    // Start is called before the first frame update
    void Start()
    {
        //ensure singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        powerupArray = new bool[] { hasInvincibility, hasDoubleJump, hasDoubleProjectiles, hasHomingProjectiles, 
            hasBonusAtk, hasBonusMvspd, hasBonusJumpHeight, hasAcceleration, hasNoFallDmg};
    }

    public void Debuff() {
        instance.debuffSource.Play();
    }



    public void EnemyHurt() {
        instance.enemyHurtSource.Play();
    }

    public void EnemyDie() {
        instance.enemyDeathSource.Play();
    }

    public static PowerupsList GetInstance()
    {
        return instance;
    }

    public bool[] GetPowerupArray()
    {
        powerupArray = new bool[] { hasInvincibility, hasDoubleJump, hasDoubleProjectiles, hasHomingProjectiles,
            hasBonusAtk, hasBonusMvspd, hasBonusJumpHeight, hasAcceleration, hasNoFallDmg};
        return powerupArray;
    }
}
