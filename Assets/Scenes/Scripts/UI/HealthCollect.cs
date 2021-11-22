using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollect : MonoBehaviour
{
    
    public GameObject Heart;

    
    void Update()
    {
 //       RotateSpeed = 2;
        transform.Rotate (0, 0, 2);
    }


    void OnTriggerEnter ()
    {
        HealthMonitor.HealthValue += 1;
        Heart.SetActive (false);
    }


}
