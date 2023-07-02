using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile {

    //basic properties
    private Sprite ProjectileSprite;
    public bool isPlayerProjectile;
    public float Speed = 0.1f;
    private Vector2 currentSpeed;
    public int lifetime = 60;
    public int damage = 10;

    //other properties
    public bool Homing;
    public float acceleration = 0.01f;

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

    public void setSpeed(Vector2 speed)
    {
        currentSpeed = speed;
        if(Mathf.Abs(currentSpeed.x) > this.Speed)
        {
            currentSpeed.x = this.Speed * ((currentSpeed.x > 0) ? 1 : -1);
        }
        if (Mathf.Abs(currentSpeed.y) > this.Speed)
        {
            currentSpeed.y = this.Speed * ((currentSpeed.y > 0) ? 1 : -1);
        }
    }

    public Vector2 getCurrentSpeed()
    {
        return currentSpeed;
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
