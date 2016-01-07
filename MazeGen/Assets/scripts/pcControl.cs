using UnityEngine;
using System.Collections;

public class pcControl : MonoBehaviour {

	float speed = 2.3f;
	float rotateSpeed = 150;
	Vector3 tilt;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		#if UNITY_EDITOR
		if(Input.GetKey(KeyCode.W))
		{
			transform.position += transform.forward*speed*Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.A))
		{
			//transform.position -= Vector3.right*speed*Time.deltaTime;
			transform.Rotate(0,-rotateSpeed*Time.deltaTime,0);
		}

		if(Input.GetKey(KeyCode.S))
		{
			transform.position -= transform.forward*speed*Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.D))
		{
			//transform.position += Vector3.right*speed*Time.deltaTime;
			transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
		}
#elif !UNITY_EDITOR
		tilt = Input.acceleration;
		tilt.y+=1;
		if(tilt.y >= 0.1f)
		{
			transform.position += transform.forward*speed*Time.deltaTime;
		}

		if(tilt.y <= -0.1f)
		{
			transform.position -= transform.forward*speed*Time.deltaTime;
		}

		if(tilt.x < -0.1f)
		{
			transform.Rotate(0,-rotateSpeed*Time.deltaTime,0);
		}

		if(tilt.x > 0.1f)
		{
			transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
		}
#endif


	}

	void OnCollisionEnter(Collision col)
	{
		if(col.collider.CompareTag("exit"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
