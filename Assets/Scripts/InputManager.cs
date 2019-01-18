﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

  private float mouseX;
  private float mouseY;

  private BoxCollider2D bc;

  // Use this for initialization
  void Start () {
    bc = gameObject.GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update () {
    mouseX = Input.mousePosition.x;
    mouseY = Input.mousePosition.y;
  }
}