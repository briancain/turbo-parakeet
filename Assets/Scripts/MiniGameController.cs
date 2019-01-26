using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour
{
  protected bool gameStarted = false;
  protected GameController.Plate difficulty; // Determined by subclass

  private float timer;
  private float gameTime = 10.0f;
  private GameController gc;
  private Text timerText;

  // Start is called before the first frame update
  void Start()
  {
    timer = gameTime;
    gc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    timerText = GameObject.FindGameObjectWithTag("MinigameTimer").GetComponent<Text>();
  }

  // Update is called once per frame
  void Update()
  {

    if (gameStarted)
    {
      timer -= Time.deltaTime;
      timerText.text = ":" + Mathf.Ceil(timer);

      if (timer <= 0.0f)
      {
        Lose();
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

  private void EndGame()
  {
    gameStarted = false;
    timerText.enabled = false;
  }

  public void Win()
  {
    gc.AddPlate(difficulty);
    EndGame();
  }

  public void Lose()
  {
    Debug.Log("lost minigame");
    EndGame();
  }
}
