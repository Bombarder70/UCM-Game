using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public static PlayerManager instance;

    public static string nickname; // player nickname
    public static int idGenerator; // current id generator
    public static int idPlayerGenerator; // current player id generator

    void Awake () {
        instance = this;
    }

    public GameObject player;
}
