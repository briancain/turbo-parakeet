using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  // Global Timer
  private float timeLeft;

  // Start is called before the first frame update
  void Start() {
    timeLeft = 120f;
  }

  // Update is called once per frame
  void Update() {
    updateTimer();
  }

  void updateTimer() {
    if (timeLeft <= 0) {
      gameOver();
    } else {
      timeLeft -= Time.deltaTime;
      Debug.Log("Time Left: " + timeLeft);
    }
  }

  void gameOver() {

  }
}
