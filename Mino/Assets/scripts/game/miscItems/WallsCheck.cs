using UnityEngine;
using System.Collections;

public class WallsCheck : MonoBehaviour {
	[Tooltip("Use if item needs two oposite walls")] 
	public bool corridor;//needs -Vector3.right and Vector3.right 
	[Tooltip("Use if item needs two close walls = corner")] 
	public bool corner; //needs Vector3.forward and Vector3.right
	[Tooltip("Use if item needs to be against a wall")] 
	public bool wallMounted; //needs Vector3.right

	// Use this for initialization
	void Start () {
		if(corridor){ corridorCheck();}
	}

	void corridorCheck(){
	// need to check if left is against a wall, if not then rotate
	// if four times without wall delete
	// need to check if right is touching a wall, if not delete

//		if (RayCast (Vector3.left, 1f)) {
//			Debug.Log("HIT");
//			transform.Rotate(new Vector3(-90,0,0));
//			corridorCheck();
//		}

		//left + right
		if (Physics.Raycast (transform.position, transform.right, 0.5f) && Physics.Raycast (-transform.position, transform.right, 0.5f)) {
			transform.eulerAngles += new Vector3(0,90,0);
			print ("!");
		}


	}

	bool RayCast(Vector3 dir, float scale){
		Vector3 point = transform.TransformDirection(dir);
		if (Physics.Raycast (transform.position, point, scale)) {
			return true;
		} else {
			return false;
		}
	}


}
