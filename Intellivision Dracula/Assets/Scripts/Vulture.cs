using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : MonoBehaviour 
{
	public bool holding = false;
	public int direction;
	public float oldPosition;
	public float speed;
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Caw ());
		player = GameObject.FindGameObjectWithTag ("Dracula");
	}
	
	// Update is called once per frame
	void Update () 
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

		if (player.GetComponent<Dracula>().bat && !holding) 
		{
			this.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
		}
		else 
		{
			this.transform.Translate(Vector2.right * Time.deltaTime * speed * direction);
		}

		if (!player.GetComponent<Dracula>().bat) 
		{
			holding = false;
		}
	}

	public void Holding(GameObject child)
	{
		holding = true;
		child.transform.parent = this.gameObject.transform;
	}

	public IEnumerator Caw()
	{
		int rand = Random.Range (1, 3);
		for (int i = 0; i < rand; i++) 
		{
			this.GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (.67f);
		}
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (Caw ());
	}
}
