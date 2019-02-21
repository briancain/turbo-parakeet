using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{

  public float flashTime = 1.0f;

  private Material m;
  private float flashTimer = 0.0f;
  private bool flashing = false;
  private int flashDir = 1;

  // Start is called before the first frame update
  void Start()
  {
    flashTimer = 0.0f;
    flashing = false;
    m = GetComponent<SpriteRenderer>().material;
  }

  // Update is called once per frame
  void Update()
  {
    if (flashing)
    {
      flashTimer += Time.deltaTime * flashDir;
      if (flashTimer >= flashTime)
      {
        flashDir *= -1;
      }
      else if (flashTimer <= 0.0f)
      {
        flashing = false;
      }

      m.SetFloat("_FlashAmount", 0.8f * (flashTimer / flashTime));
    }
  }

  public void Flash()
  {
    flashing = true;
    flashDir = 1;
    flashTimer = 0.0f;
    Debug.Log(flashing);
  }
}
