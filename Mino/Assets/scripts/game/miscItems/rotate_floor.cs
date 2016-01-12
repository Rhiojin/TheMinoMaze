using UnityEngine;
using System.Collections;

public class rotate_floor : MonoBehaviour {


	// RHEO FIX IT
	// Use this for initialization
	void Start () {
		Vector3 rot = transform.localEulerAngles; 
		rot.y = Random.Range(0,360);
		transform.localEulerAngles = rot;
		//transform.localRotation.y = Random.rotation.y;
		//Destroy (this);
	}
}
