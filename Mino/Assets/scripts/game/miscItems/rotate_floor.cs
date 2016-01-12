using UnityEngine;
using System.Collections;

public class rotate_floor : MonoBehaviour {


	// RHEO FIX IT
	// Use this for initialization
	void Start () {
		Quaternion rot = transform.localRotation; 
		rot.y = Random.Range(-360,360);
		transform.localRotation = rot;
		//transform.localRotation.y = Random.rotation.y;
		//Destroy (this);
	}
}
