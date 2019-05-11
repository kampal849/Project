using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
  [Range(-2, 2)]
  public float speed;

  void Update () 
  {
		transform.Rotate(Vector3.forward * 180 * Time.deltaTime * speed);
  }
}
