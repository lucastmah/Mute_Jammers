using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBob : MonoBehaviour
{
    private Transform t;
    private int step;
    public int stepOff;
    public float amtX, amtY;
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        step += stepOff;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        step++;
        transform.position = t.position + new Vector3(Mathf.Cos(step / 24f) * amtX, Mathf.Sin(step/24f)*amtY, 0);
    }
}
