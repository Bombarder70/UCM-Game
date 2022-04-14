using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour
{

    public static int HealthValue;
    public int InternalHealth;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    void Start() {
      HealthValue = 3;
    }


    void Update(){
			InternalHealth = HealthValue;
      if (HealthValue == 0) Heart1.SetActive(false);
			if (HealthValue == 1) {
				Heart1.SetActive(true);
				Heart2.SetActive(false);
			}
			if (HealthValue == 2){
				Heart2.SetActive(true);
				Heart3.SetActive(false);
			}
			if (HealthValue == 3) Heart3.SetActive(true);   
    }


}
