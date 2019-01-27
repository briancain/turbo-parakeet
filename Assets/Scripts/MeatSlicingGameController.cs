using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSlicingGameController : MiniGameController
{

  [SerializeField]
  public GameObject meatPrefab;

  private Vector3 foodPos;

  private GameObject spawnedFood;

  private bool gameOver;

  // Start is called before the first frame update
  void Start()
  {
    base.Start();
    difficulty = GameController.Plate.hard;
    foodPos = new Vector3(3f, 1f, 0.0f);
  }

  // Update is called once per frame
  void Update()
  {
    base.Update();

    if (!gameStarted)
    {
      return;
    }

    Debug.Log("starting hard game");
  }

  public void WinGame() {
    Win();
  }

  public override void StartGame() {
    base.StartGame();
    spawnedFood = Instantiate(meatPrefab, foodPos, Quaternion.identity);
  }

  protected override void EndGame()
  {
    Object.Destroy(spawnedFood);
    base.EndGame();
  }
}
