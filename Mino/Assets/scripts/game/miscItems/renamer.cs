using UnityEngine;
using System.Collections;

public class renamer : MonoBehaviour {

	public string name;

	// Use this for initialization
	void Start () {
		gameObject.name = name;
		Destroy (this);
	}
	

}
