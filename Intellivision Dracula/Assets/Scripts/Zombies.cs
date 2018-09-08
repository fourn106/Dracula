﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour 
{

	public float speed = .1f;
	public GameManager gm;
	public GameObject hitbox;
	public GameObject player;
	public Sprite zombie;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Invincible ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.tag == "Zombie") 
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow)) 
			{
				this.gameObject.transform.Translate (0, speed * Time.deltaTime, 0);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow)) 
			{
				this.gameObject.transform.Translate (-1 * speed * Time.deltaTime, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow)) 
			{
				this.gameObject.transform.Translate (speed * Time.deltaTime, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow)) 
			{
				this.gameObject.transform.Translate (0, -1 * speed * Time.deltaTime, 0);
			}
		} 
		else 
		{
			player = GameObject.FindGameObjectWithTag ("Dracula");
			this.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, player.transform.position, -speed * Time.deltaTime);
		}
	}
		
	public void TurnZombie()
	{
		StartCoroutine (DeathDelay ());
		StartCoroutine (Moan ());
		hitbox.SetActive (true);
		this.gameObject.tag = "Zombie";
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = zombie;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Dracula")
		{
			col.GetComponentInParent<Dracula> ().BiteNoise ();
			gm.UpdateScore (50);
			Destroy (this.gameObject);
		} 
		else if(col.tag == "Bite") 
		{
			col.gameObject.GetComponentInParent<Dracula> ().BiteNoise ();
			TurnZombie ();
		}
	}

	public IEnumerator DeathDelay()
	{
		yield return new WaitForSeconds (10f);
		gm.UpdateScore (50);
		Destroy (this.gameObject);
	}

	public IEnumerator Moan()
	{
		this.GetComponent<AudioSource> ().Play ();
		
		yield return new WaitForSeconds (3f);
		StartCoroutine (Moan ());
	}

	public IEnumerator Invincible()
	{
		this.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds (.2f);
		this.GetComponent<BoxCollider2D>().enabled = true;
	}
}
