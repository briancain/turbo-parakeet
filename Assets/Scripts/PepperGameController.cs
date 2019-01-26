using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PepperGameController : MiniGameController
{

  public Slider stopper;
  public Slider marker;
  public GameObject pepperBar;

  private float delay = 0.2f;
  private float delayTimer = 0.0f;
  private int dishesPeppered;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
  }

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
        StartPepperSlider();
      }
      return;
    }

    if (gameStarted)
    {
      stopper.value = 0.5f + 0.5f * Mathf.Sin(1.0f * Time.time);

      if (Input.GetButtonDown("Fire1"))
      {
        UpdateGameState();
      }
    }
  }

  public override void StartGame()
  {
    base.StartGame();
    dishesPeppered = 0;
    PrepNewSlider();
  }

  protected override void EndGame()
  {
    base.EndGame();
    pepperBar.SetActive(false);
  }

  private void StartPepperSlider()
  {
    pepperBar.SetActive(true);
    marker.value = Random.Range(0.1f, 0.9f);
  }

  private void PrepNewSlider()
  {
    pepperBar.SetActive(false);
    delayTimer = 0.0f;
  }

  private void UpdateGameState()
  {
    if (Mathf.Abs(stopper.value - marker.value) > 0.05f)
    {
      Lose();
      return;
    }

    dishesPeppered++;
    if (dishesPeppered >= 3)
    {
      Win();
      return;
    }

    PrepNewSlider();
  }
}
