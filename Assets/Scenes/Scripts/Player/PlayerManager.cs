using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public static PlayerManager instance;

    public static string nickname; // player nickname
    public static int idGenerator; // current id generator
    public static int idPlayerGenerator; // current player id generator
    public static int idPlayer; // current player id

    public static float lastPositionX;
    public static float lastPositionY;
    public static float lastPositionZ;

    void Awake () {
			instance = this;
    }

    public GameObject player;
}
