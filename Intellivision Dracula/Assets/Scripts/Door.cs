using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	public AudioClip doorKnock;
	public AudioClip doorOpen;
	public AudioSource audio;
	public bool inHouse = true;
	public GameObject victim;

	// Use this for initialization
	void Start () 
	{
		audio = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void DoorOpen()
	{
		audio.clip = doorOpen;
		audio.Play ();
	}

	public void SpawnVictim()
	{
		//if victim did not see them / had eyes
		if (inHouse) 
		{
			GameObject newVictim = Instantiate(victim, this.transform.position, this.transform.rotation);
			newVictim.gameObject.GetComponent<Zombies>().gm = GameObject.Find ("Main Camera").GetComponent<GameManager> ();
			inHouse = false;
			DoorOpen ();
		} 
		else 
		{
			StartCoroutine (KnockKnock ());
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Dracula")
		{
			SpawnVictim ();
		} 
	}
		
	public IEnumerator KnockKnock() //Who's there? //A broken pencil //A broken pencil who? //Never mind, its pointless
	{
		audio.clip = doorKnock;
		audio.Play ();
		yield return new WaitForSeconds (.35f);
		audio.Play ();
	}
}
