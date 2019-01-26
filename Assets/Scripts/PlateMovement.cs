using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMovement : MonoBehaviour
{
  // Positions used to generate plate path
  private Vector3 startPosition;
  private Vector3 endPosition;
  private float plateSpeed;

  // Start is called before the first frame update
  void Start() {
    startPosition = new Vector3(-9.5f,3.5f,0f);
    endPosition = new Vector3(9.5f,3.5f,0f);
    plateSpeed = 1.0f;
  }

  public void updateSpeed(float updateSpeed) {
    plateSpeed = updateSpeed;
  }

  void Awake() {
    startMovement();
  }

  void startMovement() {

  }

  // Update is called once per frame
  void Update() {
  }
}
