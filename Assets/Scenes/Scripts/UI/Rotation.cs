using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    
    public GameObject Object;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,120f, 0f) * Time.deltaTime);
        

    //    transform.Rotate(0,2,0);
    //    transform.Rotate(Vector3.forward *Time.deltaTime *100);
    }
}
