using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

	Vector3 spawnPoint = Vector3.zero;
	public GameObject PC;
	public GameObject exitCanvas;

	public Text tiltReadout;
	Vector3 tilt;

	void Start ()
	{
		tiltReadout.text = "";
	}
	
	// Update is called once per frame
//	void Update () 
//	{
//		
//		tilt = Input.acceleration;
//		tiltReadout.text = tilt.ToString();
//
//	}

	public void GetSpawnPoint(float xpos)
	{
		spawnPoint.x = xpos;
		PC.transform.position = spawnPoint;
	}

	public void GetExitPoint(float xpos, float zpos)
	{
		spawnPoint.x = xpos;
		spawnPoint.z = zpos;
		spawnPoint.y = 0.5f;
		exitCanvas.transform.position = spawnPoint;
	}
}
