using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaHitbox : MonoBehaviour {

	public GameManager gm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (col.tag == "Victim") 
			{
				gm.UpdateScore (50);
				Destroy (col.gameObject);
			}
			else if (col.tag == "Door") 
			{
				col.gameObject.GetComponent<Door> ().SpawnVictim ();
			}
		}
		else if (Input.GetKeyDown(KeyCode.E) && col.tag == "Victim") 
		{
			col.gameObject.GetComponent<Zombies> ().TurnZombie ();
		}
	}
}
