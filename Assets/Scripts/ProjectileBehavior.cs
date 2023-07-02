using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject self;
    private SpriteRenderer SpriteRenderer;

    public Projectile ProjectileClass;
    public GameObject FakeStapleClass;

    public FightLevelController flc;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        SpriteRenderer = self.GetComponent<SpriteRenderer>();
        ProjectileClass.setSprite(SpriteRenderer.sprite);
        /*if (ProjectileClass.Homing)
        {
            homingBehavior(true);
        }*/
        if (PowerupsList.GetInstance().hasHomingProjectiles && ProjectileClass.isPlayerProjectile)
        {
            //Debug.Log("This is a homing projectile");
            ProjectileClass.Homing = true;
        }
        else
        {
            //Debug.Log("This is not a homing projectile");
            ProjectileClass.Homing = false;
        }
        flc = GameObject.Find("LevelController").GetComponent<FightLevelController>();
        player = GameObject.Find("Player");
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
            float rot = gameObject.transform.rotation.z * Mathf.PI;
            //Debug.Log(rot);
            transform.localPosition = new Vector2(transform.position.x + ProjectileClass.Speed * Mathf.Cos(rot), transform.position.y + ProjectileClass.Speed * Mathf.Sin(rot));
        }
        
    }

    private void homingBehavior(bool onStart = false)
    {
        //Debug.Log("Projectile homing in on enemy");
        //Debug.Log("Player is at " + player.transform.position);
        List<Vector3> distances = new();
        foreach (GameObject go in flc.enemies)
        {
            Vector3 enemyPos = go.transform.position;
            Vector3 diffPos = enemyPos - this.transform.position;
            distances.Add(diffPos);
            //print("diffpos = " + diffPos);
        }
        float minNorm = 99999;
        int minIndex = -1;
        for (int i = 0; i < distances.Count; i++)
        {
            float nextNorm = Mathf.Abs(distances[i].x) + Mathf.Abs(distances[i].y);
            if (nextNorm < minNorm)
            {
                minNorm = nextNorm;
                minIndex = i;
            }
        }
        if (minIndex < 0)
        {
            float rot = gameObject.transform.rotation.z * Mathf.PI;
            //Debug.Log(rot);
            transform.localPosition = new Vector2(transform.position.x + ProjectileClass.Speed * Mathf.Cos(rot), transform.position.y + ProjectileClass.Speed * Mathf.Sin(rot));
            return;
        }
        else
        {
            Vector3 closestEnemyVec = distances[minIndex];
            //Debug.Log("Min dist = " + closestEnemyVec);
            Vector2 spd = ProjectileClass.getCurrentSpeed();
            //Vector2 vec = normalizedVectors(targetPosRot);
            float norm = Mathf.Abs(closestEnemyVec.x) + Mathf.Abs(closestEnemyVec.y);
            Vector2 vec = new Vector2(closestEnemyVec.x / norm, closestEnemyVec.y / norm);
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
        return;
        /*
        Vector3 tPos = player.transform.position - gameObject.transform.position;
        Vector3 tRot = gameObject.transform.position - player.transform.position;
        float targetRot = calculateRotation(tRot.x, tRot.y);
        transform.Rotate(0, 0, targetRot - ((transform.rotation.z < 0)? 360 + transform.rotation.z * 180: transform.rotation.z * 180));
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
                transform.position.y + ProjectileClass.getCurrentSpeed().y);*/
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
        if(!ProjectileClass.isPlayerProjectile && collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        else if (ProjectileClass.isPlayerProjectile && collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        Destroy(gameObject);
        if (ProjectileClass.isPlayerProjectile)
        {
            GameObject t = Instantiate(FakeStapleClass, new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
            Destroy(t, 0.5f);
        }
    }
}
