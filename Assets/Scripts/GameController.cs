using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public GameObject miniGameController;

  private MiniGameController mgc;

  // Global Timer
  private float timeLeft;

  private float playerScore;

  // Start is called before the first frame update
  void Start()
  {

    mgc = miniGameController.GetComponent<MiniGameController>();
    // Default to 2 minutes
    timeLeft = 120f;

    playerScore = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    updateTimer();
  }

  void updateTimer()
  {
    if (timeLeft <= 0)
    {
      gameOver();
    }
    else
    {
      timeLeft -= Time.deltaTime;
      //Debug.Log("Time Left: " + timeLeft);
    }
  }

  public void startMiniGame(string type)
  {
    Debug.Log("Starting the game: " + type);
    mgc.StartGame();

  }

  void gameOver()
  {

  }
}
