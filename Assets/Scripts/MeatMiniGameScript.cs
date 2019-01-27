using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMiniGameScript : MonoBehaviour
{
  private float mouseX;
  private float mouseY;
  private Vector2 mousePosition;
  private float goalDistance;

  private int totalSlices;

  [SerializeField] private GameObject line;

  [SerializeField] public List<GameObject> slicesList;

  private List<GameObject> lineList;

  void Update () {
    mouseX = Input.mousePosition.x;
    mouseY = Input.mousePosition.y;

    if (Input.GetMouseButton(0)){ //Or use GetKey with key defined with mouse button

      mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      GameObject localLineObj = Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      Destroy(localLineObj.gameObject, 1);

      //Debug.Log(mouseX + "; " + mouseY);
      //Debug.Log("Slicing meat....");
      CheckSlices();
    }
  }

  void CheckSlices() {
    Vector3 mousePos = new Vector3(mouseX, mouseY, 0f);
    Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    pz.z = 0;
    foreach(GameObject s in slicesList) {
      float distance = Vector3.Distance(s.transform.position, pz);
      if(distance <= goalDistance) {
        totalSlices--;
        if(totalSlices <= 0) {
          GameOver();
        }
      }
    }

  }

  void OnCollisionEnter2D(Collision2D col) {
    Debug.Log("oops");
  }

  void Start() {
    totalSlices = 75;
    goalDistance = 1f;
  }

  // connect the dots....
  void OnMouseDrag() {

    // NEED TO CHECK COLLISIONSSSSS OF POINTS
  }

  private void GameOver() {
    Debug.Log("game Over!!");
  }

  void OnMouseUp() {
    // check if path is complete, if so you win, otherwise fail
    Debug.Log("Let go!");
  }

}
