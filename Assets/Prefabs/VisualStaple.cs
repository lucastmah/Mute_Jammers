using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualStaple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
