using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitbox : MonoBehaviour 
{
	public GameManager gm;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Constable" && this.tag == "Zombie")
		{
			gm.UpdateScore (75);
			Destroy (col.gameObject);
		}
	}
}
