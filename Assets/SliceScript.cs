using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
  private bool beenSliced;
  // Start is called before the first frame update
  void Start()
  {
    beenSliced = false;
  }

  // Update is called once per frame
  void Update()
  {
  }


  public void Sliced(){
    beenSliced = true;
  }
}
