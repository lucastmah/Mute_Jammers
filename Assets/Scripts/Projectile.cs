using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile {

    //basic properties
    private Sprite ProjectileSprite;
    public bool isPlayerProjectile;
    public float Speed = 1f;

    //other properties
    public bool Homing;

    public Projectile() 
    {
        
    }

    //public Projectile(Sprite projectileSprite, bool isPlayerProjectile, int hp, float speed, bool homing = false, bool destructable = false){
    //    this.ProjectileSprite = projectileSprite;
    //    this.isPlayerProjectile = isPlayerProjectile;
    //    this.HP = hp;
    //    this.Speed = speed;
    //    this.Homing = homing;
    //}

    public void setSprite(Sprite sprite)
    {
        this.ProjectileSprite = sprite;
    }

    //public Sprite getProjectileSprite()
    //{
    //    return this.ProjectileSprite;
    //}

    //public int getHP()
    //{
    //    return this.HP;
    //} 

    //public float getSpeed()
    //{
    //    return this.Speed;
    //}

    //public bool isHoming()
    //{
    //    return this.Homing;
    //}

    //public bool isDestructable()
    //{
    //    return this.Destructable;
    //}
}
