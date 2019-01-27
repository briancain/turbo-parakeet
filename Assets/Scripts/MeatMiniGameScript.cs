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
      Destroy(localLineObj.gameObject, 0.1f);

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
          Win();
        }
      }
    }

  }

  void Start() {
    totalSlices = 200;
    goalDistance = 1f;
  }

  private void Win() {
    MeatSlicingGameController msgc = GameObject.FindGameObjectWithTag("MeatSlicing").GetComponent<MeatSlicingGameController>();
    msgc.Win();
  }

  void OnMouseUp() {
    // check if path is complete, if so you win, otherwise fail
  }

}
