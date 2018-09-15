using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public int direction;
	public SceneRotation sr;
	public Transform backp;
	public Transform frontp;

	// Use this for initialization
	void Start () 
	{
		sr = GameObject.Find ("Main Camera").GetComponent<SceneRotation> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void MoveBG()
	{
		sr.ShiftScene (direction);
	}
}
