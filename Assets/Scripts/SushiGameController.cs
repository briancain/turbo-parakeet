using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiGameController : MiniGameController
{
  public enum Ingredient
  {
    rice = 1,
    nori,
    salmon,
    tuna
  }

  private enum Recipe
  {
    salmonNigiri = 4,
    tunaNigiri = 5,
    salmonHandroll = 6,
    tunaHandroll = 7
  }

  public GameObject platePrefab;

  public List<GameObject> ingredientPrefabs;

  Vector3 platePos;
  List<Vector3> ingredientPositions;

  List<GameObject> objs;

  Recipe currRecipe;

  int recipeSum;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();

    platePos = new Vector3(-0.06f, -1.69f, 0.0f);

    ingredientPositions = new List<Vector3>();
    ingredientPositions.Add(new Vector3(-5.18f, -1.24f, 0.0f));
    ingredientPositions.Add(new Vector3(-2.29f, 0.8f, 0.0f));
    ingredientPositions.Add(new Vector3(2.43f, 0.83f, 0.0f));
    ingredientPositions.Add(new Vector3(4.98f, -0.97f, 0.0f));

    objs = new List<GameObject>();

    difficulty = GameController.Plate.easy;
  }

  public override void StartGame()
  {
    base.StartGame();

    // Shuffle our list of positions so each ingredient ends up in a random
    // spot
    for (int i = 0; i < ingredientPositions.Count; i++)
    {
      int randIndex = Random.Range(i, ingredientPositions.Count);
      Vector3 temp = ingredientPositions[i];
      ingredientPositions[i] = ingredientPositions[randIndex];
      ingredientPositions[randIndex] = temp;
    }

    // Instantiate each ingredient
    for (int i = 0; i < 4; i++)
    {
      objs.Add(Instantiate(ingredientPrefabs[i], ingredientPositions[i], Quaternion.identity));
    }

    objs.Add(Instantiate(platePrefab, platePos, Quaternion.identity));

    currRecipe = (Recipe)Random.Range(4, 7);
    recipeSum = 0;

    Debug.Log("Current recipe is " + currRecipe);
  }

  protected override void EndGame()
  {
    base.EndGame();
    foreach (GameObject obj in objs)
    {
      Object.Destroy(obj);
    }
    objs.Clear();
  }

  public bool Required(Ingredient toCheck)
  {
    switch (toCheck)
    {
      case Ingredient.rice:
        return true;
      case Ingredient.nori:
        return currRecipe == Recipe.salmonHandroll || currRecipe == Recipe.tunaHandroll;
      case Ingredient.salmon:
        return currRecipe == Recipe.salmonHandroll || currRecipe == Recipe.salmonNigiri;
      case Ingredient.tuna:
        return currRecipe == Recipe.tunaHandroll || currRecipe == Recipe.tunaNigiri;
      default:
        break;
    }

    return false;
  }

  public void AddIngredient(Ingredient toAdd)
  {
    recipeSum += (int)toAdd;
    if (recipeSum == (int)currRecipe)
    {
      Win();
    }
  }

}
