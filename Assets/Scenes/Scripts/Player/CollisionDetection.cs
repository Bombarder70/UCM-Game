using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.name != "Ostrov") {
      Debug.Log(collision.gameObject.name);
    }

    if (collision.gameObject.name == "skeleton2_redeyes") {
      EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

      if (enemy != null) {
        enemy.die();
      }
    }
  }

  // Znicenie objektu
  // Destroy(gameObject);
  //

}
