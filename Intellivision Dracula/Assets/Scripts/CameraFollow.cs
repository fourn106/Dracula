using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	public Transform player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Dracula").transform;
	}

	// Update is called once per frame
	void Update () 
	{
		
			Vector3 position = transform.position;
			position.x = player.transform.position.x;
			transform.position = position;
	}
}
