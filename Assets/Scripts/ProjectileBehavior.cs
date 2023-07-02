using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject self;
    private SpriteRenderer SpriteRenderer;

    public Projectile ProjectileClass;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        SpriteRenderer = self.GetComponent<SpriteRenderer>();
        ProjectileClass.setSprite(SpriteRenderer.sprite);
        homingBehavior(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ProjectileClass.lifetime--;
        if(ProjectileClass.lifetime < 0 )
        {
            Destroy( self );
        }
        if (ProjectileClass.Homing)
        {
            homingBehavior();
        }
        else
        {
            transform.localPosition = new Vector2(transform.position.x + ProjectileClass.Speed * transform.localScale.x, transform.position.y);
        }
        
    }

    private void homingBehavior(bool onStart = false)
    {
        GameObject player = GameObject.Find("Player");
        Vector3 tPos = player.transform.position - gameObject.transform.position;
        float targetRot = calculateRotation(tPos.x, tPos.y);
        transform.Rotate(0, 0, 1);
        transform.localPosition = new Vector2(transform.position.x + ProjectileClass.Speed * transform.localScale.x, transform.position.y);
    }

    //returns the angle in degrees [0,360)
    private float calculateRotation(float x, float y)
    {
        if(x == 0)
        {
            return (y > 0) ? 90 : 270;
        }
        float calc = Mathf.Atan(y / x);
        return (x < 0) ? 180 - calc : ((y < 0) ? 360 + calc : calc);
    }

    private Vector2 normalizedMagnitudes(float rotation)
    {
        return Vector2.zero;
    }
}
