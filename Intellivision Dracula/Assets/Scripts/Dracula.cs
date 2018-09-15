using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dracula : MonoBehaviour 
{
	public AudioClip bite;
	public AudioClip stab;
	public AudioSource audio;
	public float speed = .3f;
	public bool stunned = false;
	public bool bat = false;
	public GameObject hitbox;
	public Sprite dracula;
	public Sprite wings;

	public GameManager gm;

	private Vector2 leftStick;

	// Use this for initialization
	void Start () 
	{
		audio = this.GetComponent<AudioSource> ();
		gm = GameObject.Find ("Main Camera").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		GamePadState state = GamePad.GetState ();
		if (!stunned) 
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W)) 
			{
				this.gameObject.transform.Translate (0, 1 * speed * Time.deltaTime, 0);
			}
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A)) 
			{
				this.gameObject.transform.Translate (-1 * speed * Time.deltaTime, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D)) 
			{
				this.gameObject.transform.Translate (1 * speed * Time.deltaTime, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
			{
				this.gameObject.transform.Translate (0, -1 * speed * Time.deltaTime, 0);
			}
			leftStick = GamePad.GetStick(CStick.Left);
			this.gameObject.transform.Translate (leftStick.x * speed * Time.deltaTime, -1 * leftStick.y * speed * Time.deltaTime, 0);


			if (Input.GetKeyDown (KeyCode.Q) || state.Pressed(CButton.B)) 
			{
				StartCoroutine (ActivateHitbox ());
			}
			if (Input.GetKeyDown(KeyCode.E) || state.Pressed(CButton.A)) 
			{
				StartCoroutine (BiteHitbox ());
			}
		}

		if (Input.GetKeyDown(KeyCode.Space) || state.Pressed(CButton.X)) 
		{
			if (!bat) 
			{
				speed = 5f;
				bat = true;
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = wings;
			} 
			else 
			{
				this.gameObject.transform.parent = null;
				stunned = false;
				speed = 4f;
				bat = false;
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dracula;
			}
		}
	}

	public void BiteNoise()
	{
		audio.clip = bite;
		audio.Play ();
	}

	public void StabNoise()
	{
		audio.clip = stab;
		audio.Play ();
	}

	public IEnumerator ActivateHitbox()
	{
		hitbox.SetActive (true);
		yield return new WaitForSeconds (.01f);
		hitbox.SetActive (false);
	}

	public IEnumerator BiteHitbox()
	{
		hitbox.tag = "Bite";
		hitbox.SetActive (true);
		yield return new WaitForSeconds (.01f);
		hitbox.SetActive (false);
		hitbox.tag = "Dracula";
	}

	public IEnumerator Stunned()
	{
		stunned = true;
		yield return new WaitForSeconds (1f);
		stunned = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!bat) 
		{
			if (col.tag == "Stick") 
			{
				StabNoise ();
				StartCoroutine (Stunned ());
				Destroy (col.gameObject);
			}
			else if (col.tag == "Dog") 
			{
				StartCoroutine (Stunned ());
				col.transform.GetComponentInParent<Wolf> ().RunAway ();
			}
		} 
		else 
		{
			if (col.tag == "Vulture") 
			{
				stunned = true;
				col.gameObject.GetComponent<Vulture> ().Holding (this.gameObject);
			}
		}

		if (col.tag == "Coffin") 
		{
			if (gm.victimsLeft == 0 || gm.CheckTime()) 
			{
				gm.StartCoroutine (gm.OutroCutscene ());
			}
		}
		else if (col.tag == "BackGround") 
		{
			col.GetComponent<Background> ().MoveBG ();
		}
	}
}
