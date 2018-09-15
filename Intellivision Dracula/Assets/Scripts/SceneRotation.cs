using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRotation : MonoBehaviour {

	public GameObject[] backgrounds;
	public GameObject right;
	public GameObject left;

	// Use this for initialization
	void Start () 
	{
		right = backgrounds [4];
		left = backgrounds [2];
		right.GetComponent<Background> ().direction = 1;
		left.GetComponent<Background> ().direction = -1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ShiftScene(int direction)
	{
		if (direction == 1) {
			GameObject first = backgrounds [0];
			for (int i = 0; i < 6; i++) 
			{
				backgrounds [i].GetComponent<Background> ().direction = 0;
				backgrounds [i] = backgrounds [i + 1];

			}
			backgrounds [6] = first;
			first.transform.position = backgrounds [5].GetComponent<Background>().frontp.position;
			backgrounds [4].GetComponent<Background> ().direction = 1;
			backgrounds [2].GetComponent<Background> ().direction = -1;
		}
		else if (direction == -1) 
		{
			GameObject last = backgrounds [6];
			for (int i = 6; i > 0; i--) 
			{
				backgrounds [i].GetComponent<Background> ().direction = 0;
				backgrounds [i] = backgrounds [i - 1];
			}
			backgrounds [0] = last;
			last.transform.position = backgrounds [1].GetComponent<Background>().backp.position;
			backgrounds [4].GetComponent<Background> ().direction = 1;
			backgrounds [2].GetComponent<Background> ().direction = -1;
		}
	}
}
