using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour
{
  public Text timerText;

  protected bool gameStarted = false;
  private float timer;
  private float gameTime = 10.0f;

  // Start is called before the first frame update
  void Start()
  {
    timer = gameTime;
  }

  // Update is called once per frame
  void Update()
  {

    if (gameStarted)
    {
      timer -= Time.deltaTime;
      timerText.text = ":" + Mathf.Ceil(timer);
      Debug.Log("Minigame time: " + timer);


      if (timer <= 0.0f)
      {
        Debug.Log("Minigame over!");
        EndGame();
      }
    }
  }

  public void StartGame()
  {
    if (!gameStarted)
    {
      gameStarted = true;
      timer = gameTime;
      timerText.enabled = true;
    }
  }

  public void EndGame()
  {
    gameStarted = false;
    timerText.enabled = false;
  }
}
