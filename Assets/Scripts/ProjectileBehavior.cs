using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject self;
    private SpriteRenderer SpriteRenderer;

    public Projectile ProjectileClass;
    public GameObject FakeStapleClass;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        SpriteRenderer = self.GetComponent<SpriteRenderer>();
        ProjectileClass.setSprite(SpriteRenderer.sprite);
        if (ProjectileClass.Homing)
        {
            homingBehavior(true);
        }
        
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
            Debug.Log(gameObject.transform.rotation);
            transform.localPosition = new Vector2(transform.position.x + ProjectileClass.Speed * transform.localScale.x, transform.position.y);
        }
        
    }

    private void homingBehavior(bool onStart = false)
    {
        GameObject player = GameObject.Find("Player");
        Vector3 tPos = player.transform.position - gameObject.transform.position;
        Vector3 tRot = gameObject.transform.position - player.transform.position;
        float targetRot = calculateRotation(tRot.x, tRot.y);
        transform.Rotate(0, 0, targetRot - ((transform.rotation.z < 0)? 360 + transform.rotation.z: transform.rotation.z));
        if(onStart)
        {
            transform.localScale = new Vector3(1, 1, 1);
            ProjectileClass.setSpeed(normalizedVectors(tPos) * ProjectileClass.Speed);
        }
        else
        {
            Vector2 spd = ProjectileClass.getCurrentSpeed();
            //Vector2 vec = normalizedVectors(targetPosRot);
            float norm = Mathf.Abs(tPos.x) + Mathf.Abs(tPos.y);
            Vector2 vec = new Vector2(tPos.x / norm, tPos.y / norm);
            ProjectileClass.setSpeed(
                new Vector2(
                    spd.x + ProjectileClass.acceleration * vec.x,
                    spd.y + ProjectileClass.acceleration * vec.y
                    ));
        }

        transform.position = 
            new Vector2(
                transform.position.x + ProjectileClass.getCurrentSpeed().x, 
                transform.position.y + ProjectileClass.getCurrentSpeed().y);
    }

    //returns the angle in degrees [0,360)
    private float calculateRotation(float x, float y)
    {
        float calc = Mathf.Atan2(y, x);
        if(calc < 0)
        {
            calc = 2 * Mathf.PI + calc;
        }
        //calc = calc * 180 / Mathf.PI;
        return calc;
    }

    //returns the normalized magnitude from the provided rotation (ranging between [0,1])
    private Vector2 normalizedVectors(Vector2 vec)
    {
        float norm = vec.x + vec.y;
        return new Vector2(vec.x / norm, vec.y / norm);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (ProjectileClass.isPlayerProjectile)
        {
            GameObject t = Instantiate(FakeStapleClass, new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
            Destroy(t, 4);
        }
    }
}
