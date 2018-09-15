using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public Transform player;
	public Vulture vulture;
	public Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Dracula").transform;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!vulture.holding) 
		{
			Vector3 position = transform.position;
			position.x = player.transform.position.x;
			transform.position = Vector3.SmoothDamp (this.transform.position, position, ref velocity, 0.3f);
		}
	}		
}
