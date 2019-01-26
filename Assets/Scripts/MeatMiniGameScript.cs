using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMiniGameScript : MonoBehaviour
{
  private float mouseX;
  private float mouseY;

  private BoxCollider2D bc;

  // Use this for initialization
  void Start () {
    bc = gameObject.GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update () {
    mouseX = Input.mousePosition.x;
    mouseY = Input.mousePosition.y;
    //Debug.Log(mouseX + "; " + mouseY);
  }

  // Check if mini game was clicked
  // Only called if initiated on collider
  void OnMouseDrag() {
    Debug.Log(mouseX + "; " + mouseY);
    Debug.Log("Slicing meat....");
  }

  void OnMouseUp() {
    // check if path is complete, if so you win, otherwise fail
    Debug.Log("Let go!");
  }
}
