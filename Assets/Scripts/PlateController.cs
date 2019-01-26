using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{

  [SerializeField]
  GameObject platePrefab;

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
    float randomPlate = Random.Range(0, 10);

    if (plateGenerateTimer <= 0) {
      GameObject thisPlate = Instantiate(platePrefab, startPosition, Quaternion.identity);
      //activePlates.Add(thisPlate);
      plateGenerateTimer = 2f;
    }

    plateGenerateTimer -= Time.deltaTime;
  }

  void destroyPlates() {
    activePlates.Clear();
  }
}
