using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PepperGameController : MiniGameController
{

  public Slider stopper;
  public Slider marker;
  public GameObject pepperBar;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
  }

  // Update is called once per frame
  protected override void Update()
  {
    base.Update();

    if (gameStarted)
    {
      stopper.value = 0.5f + 0.5f * Mathf.Sin(1.0f * Time.time);
    }
  }

  public override void StartGame()
  {
    if (!gameStarted)
    {
      pepperBar.SetActive(true);
      marker.value = Random.Range(0.1f, 0.9f);
    }

    base.StartGame();
  }

  protected override void EndGame()
  {
    base.EndGame();
    pepperBar.SetActive(false);
  }
}
