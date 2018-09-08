using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour 
{
	public bool runningAway = false;
	public int direction;
	public float oldPosition;
	public float speed;
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Bark ());
		player = GameObject.FindGameObjectWithTag ("Dracula");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!runningAway) 
		{
			if (this.transform.position.x > oldPosition) 
			{
				direction = 1;	
			} 
			else 
			{
				direction = -1;	
			}
			oldPosition = this.transform.position.x;
			if (direction == 1) 
			{
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			} 
			else 
			{
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			}
			this.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
		} 
		else 
		{
			this.transform.Translate(Vector2.right * Time.deltaTime * speed * direction);
		}
	}

	public void RunAway()
	{
		runningAway = true;
	}

	public IEnumerator Bark()
	{
		int rand = Random.Range (1, 3);
		for (int i = 0; i < rand; i++) 
		{
			this.GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (.3f);
		}
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (Bark ());
	}
}
