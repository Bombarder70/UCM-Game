using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour {

  public GameObject playerPosObject;
  public GameObject objectPosObject;

  Vector3 _playerPos;
  Vector3 _objectPos;

  void Start() {
    _objectPos = objectPosObject.transform.position;
  }

  void Update() {
    _playerPos = playerPosObject.transform.position;

    Debug.Log(Vector3.Distance(_playerPos, _objectPos));
  }

}
