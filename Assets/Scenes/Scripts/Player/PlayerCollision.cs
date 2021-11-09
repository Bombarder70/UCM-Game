using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.name == "tree1") {
      Debug.Log(collision.gameObject.name);
    } else {
      Debug.Log(collision.gameObject.name);
    }
  }

  // Znicenie objektu
  // Destroy(gameObject);
  //

}
