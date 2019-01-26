using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMovement : MonoBehaviour
{
  // Positions used to generate plate path
  private Vector3 startPosition;
  private Vector3 centerPosition;
  private Vector3 endPosition;
  private float plateSpeed;

  private float startTime;
  // Time to move from sunrise to sunset position, in seconds.
  private float journeyTime;

  // Start is called before the first frame update
  void Start() {
  }

  public void updateSpeed(float updateSpeed) {
    plateSpeed = updateSpeed;
  }

  void Awake() {
    startPosition = new Vector3(-9.5f,4f,0f);
    endPosition = new Vector3(9.5f,4f,0f);
    plateSpeed = 1.0f;

    startTime = Time.time;
    journeyTime = 10.0f;

    centerPosition = (startPosition + endPosition) * 0.5F;
    centerPosition += new Vector3(0, 15, 0);
  }

  void startMovement() {
    // Interpolate over the arc relative to center
    Vector3 riseRelCenter = startPosition - centerPosition;
    Vector3 setRelCenter = endPosition - centerPosition;

    // The fraction of the animation that has happened so far is
    // equal to the elapsed time divided by the desired time for
    // the total journey.
    float fracComplete = (Time.time - startTime) / journeyTime;

    if (fracComplete >= 1) {
      destroyPlate();
    } else {
      transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
      transform.position += centerPosition;
    }
  }

  void destroyPlate() {
    Object.Destroy(this.gameObject);
  }

  // Update is called once per frame
  void Update() {
    startMovement();
  }

  // Check if mini game was clicked
  void OnMouseDown() {
    GameController gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();

    gameManager.startMiniGame(this.gameObject.tag);

  }
}
