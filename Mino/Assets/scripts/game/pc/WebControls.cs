using UnityEngine;
using System.Collections;

public class WebControls : MonoBehaviour {

	float cellSize;
	float L_Thresh;
	float R_Thresh;
	float U_Thresh;
	float D_Thresh;
	float rotateSpeed = 120;

	public PC_Main pcControl;
	public Transform cam;
	void Start () {
		
		L_Thresh = (Screen.width*0.01f)*0.1f;
		R_Thresh = (Screen.width*0.01f)*0.9f;
		D_Thresh = (Screen.height*0.01f)*0.1f;
		U_Thresh = (Screen.height*0.01f)*0.9f;


		cellSize = GameObject.Find("MazeGen").GetComponent<makemaze>().cubesize;
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(Input.mousePosition.x < L_Thresh) transform.Rotate(0,-rotateSpeed*Time.deltaTime,0);
//		else if(Input.mousePosition.x > R_Thresh) transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
//		else if(Input.mousePosition.x < D_Thresh) transform.Rotate(rotateSpeed*Time.deltaTime,0,0);
//		else if(Input.mousePosition.x > R_Thresh) transform.Rotate(-rotateSpeed*Time.deltaTime,0,0);


		if(Input.GetKey(KeyCode.A)) transform.Rotate(0,-rotateSpeed*Time.deltaTime,0);
		if(Input.GetKey(KeyCode.D)) transform.Rotate(0,rotateSpeed*Time.deltaTime,0);

		if(Input.GetKey(KeyCode.W)) cam.transform.Rotate(-rotateSpeed*Time.deltaTime,0,0);
		if(Input.GetKey(KeyCode.S)) cam.transform.Rotate(rotateSpeed*Time.deltaTime,0,0);

		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit ,1*cellSize))
			{
				if(hit.collider.CompareTag("Cell"))
					pcControl.Move(hit.transform.position);
			}
		}


	}
}
