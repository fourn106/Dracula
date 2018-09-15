using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public int score;
	public int victimsLeft = 3;
	public float startTime;
	public float bloodLeft = 100.00f;
	public int min;
	public int difficulty = 1;
	public int night = 1;
	public int hour = 5;
	public int diificulty;

	public GameObject continueBtn;
	public Text scoreT;
	public Text victimT;
	public Text timeT;
	public Text bloodT;
	public Text nightT;
	public Text winLose;

	public Dracula drac;
	public GameObject Vlad;
	public GameObject wolf;
	public GameObject constable;
	public GameObject vulture;

	public bool introCutscene;
	public bool outroCutscene;
	public bool lostGame = false;
	public GameObject batMoveTo;
	public GameObject batReturnTo;

	// Use this for initialization
	void Start () 
	{
		continueBtn.SetActive (false);
		difficulty = PlayerPrefs.GetInt ("Difficulty");
		startTime = Time.time;
		Vlad = GameObject.FindGameObjectWithTag ("Dracula");
		drac = Vlad.GetComponent<Dracula> ();
		StartCoroutine (IntroCutscene ());
		winLose.enabled = false;
		night = PlayerPrefs.GetInt ("Night");

		if (difficulty == 1) 
		{
			victimsLeft = 2 + night;
			hour = 5;
			if (night == 3 || night >=5) 
			{
				constable.SetActive (true);
			}
			if (night >= 2) 
			{
				vulture.GetComponent<Vulture> ().alive = true;
			}
		} 
		else if (difficulty == 2) 
		{
			victimsLeft = 5 + night;
			hour = 5;
			if (night >= 2) 
			{
				constable.SetActive (true);
			}
			vulture.GetComponent<Vulture> ().alive = true;
		}
		else if (difficulty == 3) 
		{
			victimsLeft = 8 + night;
			hour = 6;
			constable.SetActive (true);
			vulture.GetComponent<Vulture> ().alive = true;
		}
		victimT.text = "Victims Left: " + victimsLeft;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (introCutscene) 
		{
			Vlad.transform.position = Vector2.MoveTowards(Vlad.gameObject.transform.position, batMoveTo.transform.position, 2f * Time.deltaTime);
		} 
		else if (outroCutscene) 
		{
			Vlad.transform.position = Vector2.MoveTowards(Vlad.gameObject.transform.position, batReturnTo.transform.position, 2f * Time.deltaTime);
		} 
		else
		{
			float time = Time.time - startTime;

			min = (int) time / 60;
			if (min < 1) 
			{
				min = 12;
			}
			int sec = (int)Mathf.Round ((time % 60));
			string minutes = min.ToString();
			string seconds;
			if (sec < 10) 
			{
				seconds = "0" + sec.ToString ();
			} 
			else 
			{
				seconds = sec.ToString ();
			}


			timeT.text = "Time: " + minutes + ":" + seconds;
			if (minutes == "5") 
			{
				Camera cam = this.GetComponent<Camera> ();
				cam.backgroundColor = Color.red;
			}
			if (drac.bat) 
			{
				vulture.SetActive (true);
				vulture.transform.parent = null;
			}

			if ((bloodLeft <= 0 || (min > 6 && min != 12)) && !lostGame) 
			{
				lostGame = true;
				LoseScreen ();
			}
		}
	}

	public bool CheckTime()
	{
		if (hour == min)
			return true;
		else
			return false;
	}

	public void UpdateScore(int points)
	{
		score += points;
		scoreT.text = "Score: " + score;
	}

	public void UpdateVictims()
	{
		if (victimsLeft > 0) 
		{
			Debug.Log (victimsLeft);
			victimsLeft-= 1;
			victimT.text = "Victims Left: " + victimsLeft;
			if (victimsLeft == 0) 
			{
				wolf.SetActive (true);
			}
		}
	}

	public void WinScreen()
	{
		winLose.enabled = true;
		winLose.text = "Night " + night + " survived";
	}

	public void LoseScreen()
	{
		// For First Place
		if (score > PlayerPrefs.GetInt("FirstPlace")) 
		{
			int temp1, temp2;
			temp1 = PlayerPrefs.GetInt ("FirstPlace");
			PlayerPrefs.SetInt ("FirstPlace", score);
			Debug.Log (temp1 +" "+ PlayerPrefs.GetInt ("FirstPlace"));
			temp2 = PlayerPrefs.GetInt("SecondPlace");
			PlayerPrefs.SetInt ("SecondPlace", temp1);
			PlayerPrefs.SetInt ("ThirdPlace", temp2);
			Debug.Log (temp2 +" "+ PlayerPrefs.GetInt ("SecondPlace"));
			winLose.text = "You Lose\nHigh Scores\n" + PlayerPrefs.GetInt ("FirstPlace") + "\n" + PlayerPrefs.GetInt ("SecondPlace") + "\n" + PlayerPrefs.GetInt ("ThirdPlace") ;
		}// For Second Place
		else if (score > PlayerPrefs.GetInt("SecondPlace"))
		{
			int temp1 = PlayerPrefs.GetInt("SecondPlace");
			PlayerPrefs.SetInt ("SecondPlace", score);
			PlayerPrefs.SetInt ("ThirdPlace", temp1);
			Debug.Log (temp1 +" "+ PlayerPrefs.GetInt ("SecondPlace"));
			winLose.text = "You Lose\nHigh Scores\n" + PlayerPrefs.GetInt ("FirstPlace") + "\n" + PlayerPrefs.GetInt ("SecondPlace") + "\n" + PlayerPrefs.GetInt ("ThirdPlace") ;
		}// For Third Place
		else if (score > PlayerPrefs.GetInt("ThirdPlace")) 
		{
			PlayerPrefs.SetInt ("ThirdPlace", score);
			winLose.text = "You Lose\nHigh Scores\n" + PlayerPrefs.GetInt ("FirstPlace") + "\n" + PlayerPrefs.GetInt ("SecondPlace") + "\n" + PlayerPrefs.GetInt ("ThirdPlace") ;
		}
		winLose.enabled = true;
		continueBtn.SetActive (true);
	}

	public void Continue()
	{
		SceneManager.LoadScene (0);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Vulture" && vulture.GetComponent<Vulture>().holding) 
		{
			Debug.Log ("Vulture pass");
			drac.enabled = false;
			LoseScreen ();
		}
	}

	public IEnumerator BloodLoss()
	{
		if (drac.bat) 
		{
			bloodLeft -= .15f;
		} 
		else 
		{
			bloodLeft -= .1f;
		}
		if (bloodLeft > 0) 
		{
			bloodT.text = "Blood: " + Math.Round(bloodLeft, 2) + "%";
			yield return new WaitForSeconds (.25f);
			StartCoroutine (BloodLoss ());
		} 
		else 
		{
			bloodLeft = 0;
			bloodT.text = "Blood: " + bloodLeft + "%";
		}

	}

	public IEnumerator IntroCutscene()
	{
		nightT.enabled = true;
		nightT.text = "Night: " + night;
		introCutscene = true;
		drac.enabled = false;
		Vlad.GetComponent<SpriteRenderer> ().sprite = drac.wings;
		yield return new WaitForSeconds (1.5f);
		nightT.enabled = false;
		introCutscene = false;
		StartCoroutine (BloodLoss ());
		drac.enabled = true;
		Vlad.GetComponent<SpriteRenderer> ().sprite = drac.dracula;
		Vlad.GetComponent<BoxCollider2D> ().enabled = true;
	}

	public IEnumerator OutroCutscene()
	{
		PlayerPrefs.SetInt ("Night", night + 1);
		WinScreen ();
		outroCutscene = true;
		drac.enabled = false;
		Vlad.GetComponent<SpriteRenderer> ().sprite = drac.wings;
		Vlad.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene (1);
	}
}
