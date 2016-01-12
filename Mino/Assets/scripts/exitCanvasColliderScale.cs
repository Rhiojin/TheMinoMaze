using UnityEngine;
using System.Collections;

public class exitCanvasColliderScale : MonoBehaviour {

	// Use this for initialization
	public makemaze mazeScript;
	void Start ()
	{
	
		BoxCollider c = GetComponent<BoxCollider>();
		float s = mazeScript.cubesize;
		c.size *= s; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
