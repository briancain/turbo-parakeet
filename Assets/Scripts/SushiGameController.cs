﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SushiGameController : MiniGameController
{
  [SerializeField]
  AudioClip selectAudioClip;

  [SerializeField]
  AudioClip selectFailAudioClip;

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
  public GameObject foodPrefab;
  public List<GameObject> ingredientPrefabs;

  Vector3 platePos;
  Vector3 foodPos;
  List<Vector3> ingredientPositions;

  List<GameObject> objs;

  Recipe currRecipe;
  int recipeSum;

  [SerializeField]
  GameObject sushiUIPrompt;
  [SerializeField]
  Image sushiPromptImage;

  [SerializeField]
  Sprite salmonNigiriTexture;
  [SerializeField]
  Sprite tunaNigiriTexture;
  [SerializeField]
  Sprite salmonRollTexture;
  [SerializeField]
  Sprite tunaRollTexture;

  [SerializeField]
  Texture2D cursorTextureHandOpen;
  [SerializeField]
  Texture2D cursorTextureHandSelect;

  private CursorMode cursorMode = CursorMode.Auto;
  private Vector2 hotSpot;

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();

    platePos = new Vector3(-0.06f, -1.69f, 0.0f);
    foodPos = new Vector3(-0.02f, -0.72f, 0.0f);

    ingredientPositions = new List<Vector3>();
    ingredientPositions.Add(new Vector3(-4.91f, -1.36f, 0.0f));
    ingredientPositions.Add(new Vector3(-2.02f, 1.05f, 0.0f));
    ingredientPositions.Add(new Vector3(2.41f, 0.68f, 0.0f));
    ingredientPositions.Add(new Vector3(4.88f, -1.41f, 0.0f));

    objs = new List<GameObject>();

    difficulty = GameController.Plate.easy;

    hotSpot = new Vector2(35.0f, 30.0f);
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

    // Randomly generate our recipe. Each recipe is the numerical sum of its
    // ingredients.
    currRecipe = (Recipe)Random.Range(4, 7);
    recipeSum = 0;

    //Enable our UI prompt
    sushiUIPrompt.SetActive(true);
    sushiPromptImage.sprite = GetRecipeTexture();

    // Set our cursor to an open hand on no click, and a closed hand on
    // click.
    Cursor.SetCursor(cursorTextureHandOpen, hotSpot, cursorMode);
  }

  protected override void Update()
  {
    base.Update();

    if (gameStarted)
    {
      if (Input.GetMouseButtonDown(0))
      {
        Cursor.SetCursor(cursorTextureHandSelect, hotSpot, cursorMode);
      }
      else if (Input.GetMouseButtonUp(0))
      {

        Cursor.SetCursor(cursorTextureHandOpen, hotSpot, cursorMode);
      }
    }
  }

  protected override void EndGame()
  {
    base.EndGame();
    foreach (GameObject obj in objs)
    {
      Object.Destroy(obj);
    }
    objs.Clear();

    sushiUIPrompt.SetActive(false);


    Cursor.SetCursor(null, Vector2.zero, cursorMode);
  }

  protected override void PrepForEnd()
  {
    base.PrepForEnd();

    GameObject food = Object.Instantiate(foodPrefab, foodPos, Quaternion.identity);
    SpriteRenderer foodSprite = food.GetComponent<SpriteRenderer>();
    foodSprite.sprite = GetRecipeTexture();
    objs.Add(food);
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
    audio.PlayOneShot(selectAudioClip, 1f);
    if (recipeSum == (int)currRecipe)
    {
      Win();
    }
  }

  public void WrongSelect()
  {
    audio.PlayOneShot(selectFailAudioClip, 1f);
  }

  public Sprite GetRecipeTexture()
  {
    switch (currRecipe)
    {
      case Recipe.salmonHandroll:
        return salmonRollTexture;
      case Recipe.tunaHandroll:
        return tunaRollTexture;
      case Recipe.salmonNigiri:
        return salmonNigiriTexture;
      case Recipe.tunaNigiri:
        return tunaNigiriTexture;
      default:
        break;
    }

    return null;
  }

}
