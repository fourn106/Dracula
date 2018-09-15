using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public GameObject MainScreen;
	public GameObject DifficultyScreen;
	public GameObject OptionsScreen;

	// Use this for initialization
	void Start () 
	{
		PlayerPrefs.SetInt ("Night", 1);
		PlayerPrefs.SetInt ("FirstPlace", 5000);
		PlayerPrefs.SetInt ("SecondPlace", 2500);
		PlayerPrefs.SetInt ("ThirdPlace", 1000);
		Debug.Log(PlayerPrefs.GetInt("FirstPlace"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void BackButton()
	{
		DifficultyScreen.SetActive (false);
		OptionsScreen.SetActive (false);
		MainScreen.SetActive (true);
	}

	public void StartGame()
	{
		MainScreen.SetActive (false);
		DifficultyScreen.SetActive (true);
	}

	public void Options ()
	{
		MainScreen.SetActive (false);
		OptionsScreen.SetActive (true);
	}

	public void Quit()
	{
		Debug.Log("Quit");
		Application.Quit ();
	}

	public void Easy()
	{
		PlayerPrefs.SetInt ("Difficulty", 1);
		SceneManager.LoadScene (1);
	}

	public void Medium()
	{
		PlayerPrefs.SetInt ("Difficulty", 2);
		SceneManager.LoadScene (1);
	}

	public void Hard()
	{
		PlayerPrefs.SetInt ("Difficulty", 3);
		SceneManager.LoadScene (1);
	}
}
