using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public GameObject GameOverScreen;
    Vector3 mapVector = new Vector3 (1,1,1);
    public static bool inQuest = false;
    public static float timer;


    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }  
    }
    
    public void GameOver()
    {
        GameOverScreen.SetActive(true);
      //  GameOverScreen.transform.LeanMoveLocalY(0,3.5f).setEaseOutExpo().delay = 4.2f;
        GameOverScreen.transform.LeanScale(mapVector, 0.8f);
        HealthMonitor.HealthValue = 0;
        
    }

    public void Update()
    {
        if (inQuest) timer += Time.deltaTime;
    }

}
