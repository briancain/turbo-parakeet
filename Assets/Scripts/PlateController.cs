using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{

  [SerializeField]
  GameObject platePrefab;

  [SerializeField]
  GameObject easyPlate;

  [SerializeField]
  GameObject mediumPlate;

  [SerializeField]
  GameObject hardPlate;

  // Positions used to generate plate path
  private Vector3 startPosition;
  private Vector3 middlePosition;
  private Vector3 endPosition;

  private GameObject plate;

  private List<GameObject> activePlates;

  private float plateGenerateTimer;

  // Start is called before the first frame update
  void Start() {
    startPosition = new Vector3(-9.5f,3.5f,0f);
    middlePosition = new Vector3(0f,1.5f,0f);
    endPosition = new Vector3(9.5f,3.5f,0f);

    plateGenerateTimer = 1.0f;

    activePlates = new List<GameObject>();
    generatePlate();
  }

  // Update is called once per frame
  void Update() {
    generatePlate();
  }

  void generatePlate() {
    if (plateGenerateTimer <= 0) {
      float randomPlate = Random.Range(0, 10);
      GameObject plateChoice;

      if (randomPlate >= 0 && randomPlate <= 4) {
        //Debug.Log("Easy plate");
        plateChoice = easyPlate;
      } else if (randomPlate >= 5 && randomPlate <= 7) {
        //Debug.Log("Medium plate");
        plateChoice = mediumPlate;
      } else {
        //Debug.Log("Hard plate");
        plateChoice = hardPlate;
      }

      GameObject thisPlate = Instantiate(plateChoice, startPosition, Quaternion.identity);
      //activePlates.Add(thisPlate);
      plateGenerateTimer = 2f;
    }

    plateGenerateTimer -= Time.deltaTime;
  }

  void destroyPlates() {
    activePlates.Clear();
  }
}
