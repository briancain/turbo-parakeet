using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMiniGameScript : MonoBehaviour
{
  private float mouseX;
  private float mouseY;
  private Vector2 mousePosition;

  [SerializeField] private GameObject line;

  private List<GameObject> lineList;

  // connect the dots....
  void OnMouseDrag() {
    Debug.Log(mouseX + "; " + mouseY);
    Debug.Log("Slicing meat....");

    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    GameObject localLineObj = Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    lineList.Add(localLineObj);
  }

  void gameOver() {
    lineList.Clear();
  }

  void OnMouseUp() {
    // check if path is complete, if so you win, otherwise fail
    Debug.Log("Let go!");
  }

  void Update () {
  }

  void Start() {
    lineList = new List<GameObject>();

  }
}
