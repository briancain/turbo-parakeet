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
  private SpriteRenderer bgSprite;

  private AudioSource audio;

  [SerializeField]
  AudioClip winMiniGameClip;

  [SerializeField]
  AudioClip loseMiniGameClip;

  // Start is called before the first frame update
  protected virtual void Start()
  {
    timer = gameTime;

    gc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    timerText = GameObject.FindGameObjectWithTag("MinigameTimer").GetComponent<Text>();
    bgSprite = GameObject.FindGameObjectWithTag("MinigameBackground").GetComponent<SpriteRenderer>();
    audio = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  protected virtual void Update()
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

  public virtual void StartGame()
  {
    if (!gameStarted)
    {
      gameStarted = true;
      timer = gameTime;
      timerText.enabled = true;
      bgSprite.enabled = true;
    }
  }

  protected virtual void EndGame()
  {
    gameStarted = false;
    timerText.enabled = false;
    bgSprite.enabled = false;

    gc.EnableMiniGameSelection();
  }

  public void Win()
  {
    audio.PlayOneShot(winMiniGameClip, 1f);
    gc.AddPlate(difficulty);
    EndGame();
  }

  public void Lose()
  {
    audio.PlayOneShot(loseMiniGameClip, 1f);
    Debug.Log("lost minigame");
    EndGame();
  }
}
