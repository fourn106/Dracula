using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public int score;
	public int victimsLeft;
	public float startTime;
	public float bloodLeft = 100.00f;
//	public float minutes;
//	public float seconds;
	public int difficulty = 1;

	public Text scoreT;
	public Text victimT;
	public Text timeT;
	public Text bloodT;

	public Dracula drac;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		drac = GameObject.FindGameObjectWithTag ("Dracula").GetComponent<Dracula> ();
		StartCoroutine (BloodLoss ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		float time = Time.time - startTime;
		string minutes = ((int) time / 60).ToString();
		string seconds = Mathf.Round((time % 60)).ToString ("f2");

		timeT.text = "Time: " + minutes + ":" + seconds;
		if (minutes == "5") 
		{
			Camera cam = this.GetComponent<Camera> ();
			cam.backgroundColor = Color.red;
		}
	}

	public void UpdateScore(int points)
	{
		score += points;
		scoreT.text = "Score: " + score;
	}

	public IEnumerator BloodLoss()
	{
		if (drac.bat) 
		{
			bloodLeft -= .3f;
		} 
		else 
		{
			bloodLeft -= .15f;
		}
		bloodT.text = "Blood: " + bloodLeft + "%";
		yield return new WaitForSeconds (1f);
		StartCoroutine (BloodLoss ());
	}
}
