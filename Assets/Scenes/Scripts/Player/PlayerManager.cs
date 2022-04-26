using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public static PlayerManager instance;

    public static string nickname;
    public static int idGenerator;

    void Awake () {
        instance = this;
    }

    public GameObject player;
}
