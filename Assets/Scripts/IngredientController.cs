using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
  public SushiGameController.Ingredient type;

  bool clicked;

  // Start is called before the first frame update
  void Start()
  {
    clicked = false;
  }

  private void OnMouseDown()
  {
    Debug.Log("Clicked on " + type);
    if (!clicked)
    {
      clicked = true;
      SushiGameController sgc = GameObject.FindGameObjectWithTag("SushiController").GetComponent<SushiGameController>();
      if (sgc.Required(type))
      {
        transform.GetChild(0).gameObject.SetActive(true);
        sgc.AddIngredient(type);
      }
      else
      {
        sgc.WrongSelect();
      }
    }
  }
}
