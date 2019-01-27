using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public enum Plate
  {
    easy,
    medium,
    hard
  };

  // Global Timer
  private float timeLeft;
  private Text timerText;

  public GameObject gameOverUI;
  public Text gameOverText;

  private List<Plate> completedPlates;

  private AudioSource audio;

  private bool minigameActive;

  // Cursor handling
  [SerializeField]
  Texture2D cursorTexture;
  [SerializeField]
  Texture2D cursorTextureKnife;
  [SerializeField]
  Texture2D cursorTexturePepper;
  [SerializeField]
  Texture2D cursorTextureHandOpen;
  [SerializeField]
  Texture2D cursorTextureHandSelect;
  private CursorMode cursorMode = CursorMode.Auto;
  private Vector2 hotSpot = Vector2.zero;

  public SushiGameController easyMinigame;
  public PepperGameController mediumMinigame;
  public MeatSlicingGameController hardMinigame;


  [SerializeField]
  AudioClip gameOverAudioClip;

  // Start is called before the first frame update
  void Start()
  {
    audio = GetComponent<AudioSource>();

    // Default to 2 minutes
    timeLeft = 120f;
    timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
    timerText.enabled = true;

    completedPlates = new List<Plate>();
    minigameActive = false;
  }

  // Update is called once per frame
  void Update()
  {
    updateTimer();
  }

  void updateTimer()
  {
    if (timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      timerText.text = ":" + Mathf.Ceil(timeLeft);
      //Debug.Log("Time Left: " + timeLeft);
    }

    if (timeLeft <= 0 && !minigameActive)
    {
      gameOver();
    }
  }

  public void startMiniGame(string type)
  {
    if (minigameActive)
    {
      return;
    }

    Debug.Log("Starting the game: " + type);

    switch (type)
    {
      case "MediumPlate":
        mediumMinigame.StartGame();
        break;
      case "HardPlate":
        hardMinigame.StartGame();
        Cursor.SetCursor(cursorTextureKnife, hotSpot, cursorMode);
        break;
      case "EasyPlate":
        Cursor.SetCursor(cursorTextureHandOpen, hotSpot, cursorMode);
        easyMinigame.StartGame();
        break;
      default:
        break;
    }
    timerText.enabled = false;
    minigameActive = true;
  }

  public void AddPlate(Plate difficulty)
  {
    completedPlates.Add(difficulty);
    Debug.Log("Won plate " + difficulty);
    string contents = "[";
    completedPlates.ForEach(delegate (Plate p)
    {
      contents = contents + p + ",";
    });
    contents += "]";
    Debug.Log(contents);
  }

  void gameOver()
  {
    audio.Stop();
    audio.PlayOneShot(gameOverAudioClip, 1f);
    minigameActive = true;
    gameOverUI.SetActive(true);

    int score = CalculateScore();
    gameOverText.text = "$" + score + ".00";
  }

  public void EnableMiniGameSelection()
  {
    minigameActive = false;
    timerText.enabled = true;
    Debug.Log("returning cursor...");
    Cursor.SetCursor(null, Vector2.zero, cursorMode);
  }

  private int CalculateScore()
  {
    int score = 0;

    foreach (Plate p in completedPlates)
    {
      switch (p)
      {
        case Plate.easy:
          score += 5;
          break;
        case Plate.medium:
          score += 20;
          break;
        case Plate.hard:
          score += 50;
          break;
        default:
          break;
      }
    }

    return score;
  }
}
