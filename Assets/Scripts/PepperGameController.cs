using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PepperGameController : MiniGameController
{

  public Slider stopper;
  public Slider marker;
  public GameObject pepperBar;

  [SerializeField]
  public GameObject foodPrefab;

  [SerializeField]
  public GameObject pepperPrefab;

  private float delay = 0.2f;
  private float delayTimer = 0.0f;
  private int dishesPeppered;

  private Vector3 foodPos;
  private Vector3 pepperPos;

  private GameObject spawnedFood;
  private GameObject spawnedPepper;

  [SerializeField]
  AudioClip grindAudioClip;
  private float grindAudioDuration;
  private float grindAudioCooldown;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();

    grindAudioDuration = 1f;
    difficulty = GameController.Plate.medium;
    foodPos = new Vector3(-0.04f, -1.75f, 0.0f);
    pepperPos = new Vector3(3.96f, 1.39f, 0.0f);
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

      PlayGrindClip();

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
    PrepNewSlider();
    base.EndGame();
  }

  private void StartPepperSlider()
  {
    pepperBar.SetActive(true);
    marker.value = Random.Range(0.1f, 0.9f);

    spawnedFood = Instantiate(foodPrefab, foodPos, Quaternion.identity);
    spawnedPepper = Instantiate(pepperPrefab, pepperPos, Quaternion.identity);
  }

  private void PrepNewSlider()
  {
    pepperBar.SetActive(false);
    delayTimer = 0.0f;

    Object.Destroy(spawnedFood);
    Object.Destroy(spawnedPepper);
  }

  private void PlayGrindClip() {
    if (Time.time > grindAudioCooldown) {
      grindAudioCooldown = Time.time + grindAudioDuration;
      audio.PlayOneShot(grindAudioClip, 3f);
    }
  }

  private void UpdateGameState()
  {
    if (Mathf.Abs(stopper.value - marker.value) > 0.05f)
    {
      Lose();
      return;
    }

    audio.PlayOneShot(winMiniGameClip, 1f);
    dishesPeppered++;
    if (dishesPeppered >= 3)
    {
      Win();
      return;
    }

    PrepNewSlider();
  }
}
