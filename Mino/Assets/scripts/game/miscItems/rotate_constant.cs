using UnityEngine;
using System.Collections;

public class rotate_constant : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * 1 * Time.deltaTime);
	}
}
