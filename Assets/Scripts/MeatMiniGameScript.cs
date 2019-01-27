using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMiniGameScript : MonoBehaviour
{
  private float mouseX;
  private float mouseY;
  private Vector2 mousePosition;

  [SerializeField] private GameObject line;

  // Check if mini game was clicked
  // Only called if initiated on collider
  void OnMouseDrag() {
    Debug.Log(mouseX + "; " + mouseY);
    Debug.Log("Slicing meat....");
    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
  }

  //void OnMouseUp() {
  //  // check if path is complete, if so you win, otherwise fail
  //  Debug.Log("Let go!");
  //}

  void Update () {
  }
}
