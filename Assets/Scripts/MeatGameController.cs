using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatGameController : MiniGameController
{
  //[SerializeField]
  //public GameObject foodPrefab;

  private int totalSlices;

  private float delay = 0.2f;
  private float delayTimer = 0.0f;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();

    totalSlices = 0;
    difficulty = GameController.Plate.hard;
    //foodPos = new Vector3(-0.04f, -1.75f, 0.0f);
  }
  //
  // Update is called once per frame
  protected override void Update()
  {
    base.Update();

    if (!gameStarted)
    {
      return;
    }

    if (delayTimer < delay)
    {
      delayTimer += Time.deltaTime;
      if (delayTimer >= delay)
      {
        // START GAME
      }
      return;
    }

    if (gameStarted)
    {
      // do it
    }
  }

  public override void StartGame()
  {
    base.StartGame();
  }

  protected override void EndGame()
  {
    DestroyLines();
    base.EndGame();
  }

  private void DestroyLines() {

  }

  public void SliceRegistered() {
    totalSlices++;
    Debug.Log(totalSlices);
  }
}
