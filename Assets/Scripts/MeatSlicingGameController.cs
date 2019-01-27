using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSlicingGameController : MiniGameController
{

  [SerializeField]
  public GameObject hintUI;

  [SerializeField]
  public GameObject meatPrefab;

  [SerializeField]
  public GameObject platePrefab;

  private List<GameObject> sceneList;

  private Vector3 foodPos;
  private Vector3 platePos;

  private bool gameOver;

  // Start is called before the first frame update
  void Start()
  {
    base.Start();

    difficulty = GameController.Plate.hard;
    foodPos = new Vector3(3f, 1f, 0.0f);
    platePos = new Vector3(0f, -2f, 0.0f);

    sceneList = new List<GameObject>();
  }

  // Update is called once per frame
  void Update()
  {
    base.Update();

    if (!gameStarted)
    {
      return;
    }
  }

  protected override void PrepForEnd() {
    base.PrepForEnd();

  }

  public void WinGame() {
    Win();
  }

  public override void StartGame() {
    base.StartGame();
    hintUI.SetActive(true);

    GameObject spawnedFood = Instantiate(meatPrefab, foodPos, Quaternion.identity);
    GameObject spawnedPlate = Instantiate(platePrefab, platePos, Quaternion.identity);

    sceneList.Add(spawnedFood);
    sceneList.Add(spawnedPlate);
  }

  protected override void EndGame()
  {
    hintUI.SetActive(false);
    foreach(GameObject m in sceneList) {
      Object.Destroy(m);
    }

    GameObject oldMeat = GameObject.FindWithTag("MeatSlice");
    if (oldMeat != null) {
      Object.Destroy(oldMeat);
    } else {
      Debug.Log("Null");
    }
    base.EndGame();
  }
}
