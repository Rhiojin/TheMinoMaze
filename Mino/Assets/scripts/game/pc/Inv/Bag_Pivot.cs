using UnityEngine;
using System.Collections;

public class Bag_Pivot : MonoBehaviour {

	GameObject head;
	Vector3 temp;
	void Start(){
		head = GameObject.Find ("Head");
		InvokeRepeating ("CheckRot", 1, 1);
	}


	void CheckRot(){
		if (head.transform.eulerAngles.x > 270 && head.transform.eulerAngles.x < 360) {
			//front
			if (head.transform.eulerAngles.y > 0 && head.transform.eulerAngles.y < 45) {
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0, transform.eulerAngles.z);
			}
			if (head.transform.eulerAngles.y > 315 && head.transform.eulerAngles.y < 360) {
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0, transform.eulerAngles.z);
			}
			//left
			if (head.transform.eulerAngles.y > 225 && head.transform.eulerAngles.y < 315) {
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 270, transform.eulerAngles.z);
			}

			//right
			if (head.transform.eulerAngles.y > 45 && head.transform.eulerAngles.y < 135) {
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 90, transform.eulerAngles.z);
			}
			//behind
			if (head.transform.eulerAngles.y > 135 && head.transform.eulerAngles.y < 225) {
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 180, transform.eulerAngles.z);
			}
		}
	}
}
