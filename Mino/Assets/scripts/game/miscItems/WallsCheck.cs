using UnityEngine;
using System.Collections;

public class WallsCheck : MonoBehaviour {
	[Tooltip("Use if item needs two oposite walls")] 
	public bool corridor;//needs -Vector3.right and Vector3.right 
	private int corridorCount;
	[Tooltip("Use if item needs two close walls = corner")] 
	public bool corner; //needs Vector3.forward and Vector3.right
	[Tooltip("Use if item needs to be against a wall")] 
	public bool wallMounted; //needs Vector3.right
	private int checkTimes;
	// Use this for initialization
	void Start () {
		if(corridor){ Invoke("corridorCheck",2f);}
	}

	void corridorCheck(){
		if (checkTimes > 4) {
			Destroy (gameObject);
		}
	// need to check if left is against a wall, if not then rotate
	// if four times without wall delete
	// need to check if right is touching a wall, if not delete
		Debug.DrawLine(transform.position,transform.position+Vector3.right*2.5f, Color.cyan,2);
		Debug.DrawLine(transform.position,transform.position+Vector3.left*2.5f, Color.cyan,2);
		RaycastHit hit;
		//left + right
		if (Physics.Raycast (transform.position, transform.position+Vector3.right, out hit, 0.1f) && Physics.Raycast (transform.position, transform.position+Vector3.left, out hit, 0.1f) && hit.collider.CompareTag("Wall")) {
			Debug.Log (hit.collider.name+" I'm cool");
		}
		else
		{
			transform.eulerAngles += new Vector3(0,90,0);
			checkTimes++;
			Invoke("corridorCheck", 1f);
			return;
		}
	}
}
