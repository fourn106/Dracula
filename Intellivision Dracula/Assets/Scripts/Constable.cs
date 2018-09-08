using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constable : MonoBehaviour 
{
	public bool canThrow = true;
	public float speed;
	public GameObject baton;
	public GameObject player;
	public GameManager gm;
	public int direction = 1;
	public float oldPosition;

	// Use this for initialization
	void Start () 
	{
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

		this.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, player.transform.position, speed * Time.deltaTime);

		if (direction == 1) 
		{
			if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < 6 && canThrow)
			{
				StartCoroutine (ThrowWait ());
				GameObject newBaton = Instantiate(baton, this.transform.position, this.transform.rotation);
				newBaton.gameObject.GetComponent<Baton> ().direction = direction;
			}
		}
		else if (direction == -1) 
		{
			if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < 6 && canThrow)
			{
				StartCoroutine (ThrowWait ());
				GameObject newBaton = Instantiate(baton, this.transform.position, this.transform.rotation);
				newBaton.gameObject.GetComponent<Baton> ().direction = direction;
				newBaton.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Zombie")
		{
			gm.UpdateScore (75);
			Destroy (this.gameObject);
		}
	}

	public IEnumerator ThrowWait()
	{
		canThrow = false;
		yield return new WaitForSeconds (3f);
		canThrow = true;
	}
}
