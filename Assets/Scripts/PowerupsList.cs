using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsList : MonoBehaviour
{
    //singleton class to store player powerups left 
    static PowerupsList instance;
    public bool hasInvincibility = true;
    public bool hasDoubleJump = true;
    public bool hasBiggerProjectiles = true;
    public bool hasDoubleProjectiles = true;
    public bool hasHomingProjectiles = true;
    public bool hasBonusAtk = true;
    public bool hasBonusMvspd = true;
    public bool hasBonusMaxHp = true;
    public bool hasBonusJumpHeight = true;
    public bool hasRegen = true;
    public bool hasAcceleration = true;
    public bool hasNoFallDmg = true;
    public bool hasUnlimitedAmmo = true;

    public int currentLevel = 1;

    private bool[] powerupArray;


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
        powerupArray = new bool[] { hasInvincibility, hasDoubleJump, 
            hasBiggerProjectiles, hasDoubleProjectiles, hasHomingProjectiles, hasBonusAtk, 
            hasBonusMvspd, hasBonusMaxHp, hasBonusJumpHeight, hasRegen, hasAcceleration, 
            hasNoFallDmg, hasUnlimitedAmmo};
    }

    public static PowerupsList GetInstance()
    {
        return instance;
    }

    public bool[] GetPowerupArray()
    {
        powerupArray = new bool[] { hasInvincibility, hasDoubleJump,
            hasBiggerProjectiles, hasDoubleProjectiles, hasHomingProjectiles, hasBonusAtk,
            hasBonusMvspd, hasBonusMaxHp, hasBonusJumpHeight, hasRegen, hasAcceleration,
            hasNoFallDmg, hasUnlimitedAmmo};
        return powerupArray;
    }
}
