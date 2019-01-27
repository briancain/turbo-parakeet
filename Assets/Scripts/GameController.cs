using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public GameObject miniGameController;

  public enum Plate
  {
    easy,
    medium,
    hard
  };

  private MiniGameController mgc;

  // Global Timer
  private float timeLeft;

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

  public PepperGameController mediumMinigame;
  public MeatSlicingGameController hardMinigame;

  // Start is called before the first frame update
  void Start()
  {
    audio = GetComponent<AudioSource>();

    mgc = miniGameController.GetComponent<MiniGameController>();
    // Default to 2 minutes
    timeLeft = 120f;

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
    if(minigameActive) {
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
        break;
      default:
        mgc.StartGame();
        break;
    }
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
  }

  public void EnableMiniGameSelection(){
    minigameActive = false;
  }
}
