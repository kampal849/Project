using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
	public float speed;
	void Start () 
	{
		GetComponent<Animator>().speed = speed;
	}
}
