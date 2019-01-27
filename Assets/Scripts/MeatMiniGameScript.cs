using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMiniGameScript : MonoBehaviour
{
  private float mouseX;
  private float mouseY;
  private Vector2 mousePosition;
  private float goalDistance;

  private int totalSlices;

  [SerializeField] private GameObject line;

  [SerializeField] public List<GameObject> slicesList;

  private List<GameObject> lineList;

  private AudioSource audio;

  [SerializeField]
  AudioClip sliceAudioClip;
  private float sliceAudioDuration;
  private float sliceAudioCooldown;

  [SerializeField]
  AudioClip successFinishClip;

  private bool gameFinished;

  [SerializeField]
  Sprite slicedMeat;

  [SerializeField]
  GameObject meatFlank;

  Vector3 foodPos;

  private SpriteRenderer sr;

  void Update () {
    mouseX = Input.mousePosition.x;
    mouseY = Input.mousePosition.y;

    if (Input.GetMouseButton(0)){ //Or use GetKey with key defined with mouse button

      mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      GameObject localLineObj = Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      Destroy(localLineObj.gameObject, 0.1f);

      if (!gameFinished) {
        CheckSlices();
      }
    }
  }

  void PlaySliceAudio() {
    if (Time.time > sliceAudioCooldown) {
      sliceAudioCooldown = Time.time + sliceAudioDuration;
      audio.PlayOneShot(sliceAudioClip, 1f);
    }
  }

  void CheckSlices() {
    Vector3 mousePos = new Vector3(mouseX, mouseY, 0f);
    Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    pz.z = 0;
    foreach(GameObject s in slicesList) {
      float distance = Vector3.Distance(s.transform.position, pz);
      if(distance <= goalDistance) {
        PlaySliceAudio();
        totalSlices--;
        if(totalSlices <= 0) {
          gameFinished = true;
          Win();
        }
      }
    }

  }

  void Start() {
    totalSlices = 200;
    goalDistance = 1f;

    audio = GetComponent<AudioSource>();
    sliceAudioDuration = 0.5f;
    gameFinished = false;
    sr = GetComponent<SpriteRenderer>();
    foodPos = new Vector3(-0.02f, -1.5f, 0.0f);
  }

  private void Win() {
    audio.Stop();
    audio.PlayOneShot(successFinishClip, 1f);

    sr.sprite = slicedMeat;
    GameObject meatSliceObj = Object.Instantiate(meatFlank, foodPos, Quaternion.identity);
    // initialize meat piece here
    MeatSlicingGameController msgc = GameObject.FindGameObjectWithTag("MeatSlicing").GetComponent<MeatSlicingGameController>();

    msgc.WinGame();
  }
}
