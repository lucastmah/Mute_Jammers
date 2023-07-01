using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile {

    //basic properties
    public bool isPlayerProjectile;
    private int HP;
    private float Speed;

    //other properties
    private bool Homing;
    private bool Destructable;


    public Projectile(){
        HP = 100;
        Speed = 100;
        Homing = false;
        Destructable = true;
    }

    public Projectile(bool isPlayerProjectile, int hp, float speed, bool homing = false, bool destructable = false){
        this.HP = hp;
        this.Speed = speed;
        this.Homing = homing;
        this.Destructable = destructable;
    }
}
