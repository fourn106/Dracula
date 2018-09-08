using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baton : MonoBehaviour {

	public int direction;
	public float speed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate(Vector2.right * Time.deltaTime * speed * direction);
	}
}
