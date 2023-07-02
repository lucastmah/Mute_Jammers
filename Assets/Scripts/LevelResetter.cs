using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PowerupsList.GetInstance().currentLevel = 1;
        PowerupsList.GetInstance().hasAcceleration = true;
        PowerupsList.GetInstance().hasBonusAtk = true;
        PowerupsList.GetInstance().hasBonusJumpHeight = true;
        PowerupsList.GetInstance().hasBonusMvspd = true;
        PowerupsList.GetInstance().hasDoubleJump = true;
        PowerupsList.GetInstance().hasDoubleProjectiles = true;
        PowerupsList.GetInstance().hasHomingProjectiles = true;
        PowerupsList.GetInstance().hasInvincibility = true;
        PowerupsList.GetInstance().hasNoFallDmg = true;
    }
}
