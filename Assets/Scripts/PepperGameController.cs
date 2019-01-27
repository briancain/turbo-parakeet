using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PepperGameController : MiniGameController
{

  public Slider stopper;
  public Slider marker;
  public GameObject hintUI;
  public GameObject pepperBar;
  public GameObject foodPrefab;
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
  [SerializeField]
  AudioClip grindSuccessAudioClip;
  private float grindAudioDuration;
  private float grindAudioCooldown;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();

    grindAudioDuration = 1f;
    difficulty = GameController.Plate.medium;
    foodPos = new Vector3(-0.04f, -1.75f, 0.0f);
    pepperPos = new Vector3(2.34f, 0.41f, 0.0f);
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
    hintUI.SetActive(true);
    dishesPeppered = 0;
    spawnedFood = Instantiate(foodPrefab, foodPos, Quaternion.identity);
    spawnedPepper = Instantiate(pepperPrefab, pepperPos, Quaternion.identity);
    PrepNewSlider();
  }

  protected override void EndGame()
  {
    hintUI.SetActive(false);
    PrepNewSlider();
    Object.Destroy(spawnedFood);
    Object.Destroy(spawnedPepper);
    base.EndGame();
  }

  protected override void PrepForEnd()
  {
    base.PrepForEnd();
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

  private void PlayGrindClip()
  {
    if (Time.time > grindAudioCooldown)
    {
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

    audio.PlayOneShot(grindSuccessAudioClip, 1f);
    dishesPeppered++;

    spawnedFood.transform.GetChild(dishesPeppered).gameObject.SetActive(true);
    if (dishesPeppered >= 3)
    {
      Win();
      return;
    }

    PrepNewSlider();
  }
}
