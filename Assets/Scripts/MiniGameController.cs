using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour
{
  protected bool gameStarted = false;
  protected bool finishing = false;
  protected GameController.Plate difficulty; // Determined by subclass

  private float timer;
  private float gameTime = 10.0f;
  private float finishTime = 1.0f;

  private GameController gc;
  private SpriteRenderer bgSprite;

  public AudioSource audio;

  [SerializeField]
  public AudioClip winMiniGameClip;

  [SerializeField]
  AudioClip loseMiniGameClip;

  [SerializeField]
  AudioClip gameTransitionClip;

  public GameObject timerUI;
  public Text timerText;

  // Start is called before the first frame update
  protected virtual void Start()
  {
    timer = gameTime;

    gc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    bgSprite = GameObject.FindGameObjectWithTag("MinigameBackground").GetComponent<SpriteRenderer>();
    audio = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  protected virtual void Update()
  {
    if (finishing)
    {
      timer -= Time.deltaTime;
      if (timer <= 0.0f)
      {
        EndGame();
      }
    }
    else if (gameStarted)
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
      audio.PlayOneShot(gameTransitionClip, 1f);
      gameStarted = true;
      timer = gameTime;
      timerUI.SetActive(true);
      bgSprite.enabled = true;
    }
  }

  protected virtual void EndGame()
  {
    gameStarted = false;
    finishing = false;
    timerUI.SetActive(false);
    bgSprite.enabled = false;

    gc.EnableMiniGameSelection();
  }

  public void Win()
  {
    audio.PlayOneShot(winMiniGameClip, 1f);
    gc.AddPlate(difficulty);
    PrepForEnd();
  }

  public void Lose()
  {
    audio.PlayOneShot(loseMiniGameClip, 1f);
    EndGame();
  }

  protected virtual void PrepForEnd()
  {
    finishing = true;
    timer = finishTime;
  }
}
