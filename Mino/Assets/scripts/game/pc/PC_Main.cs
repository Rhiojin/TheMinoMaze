using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PC_Main : MonoBehaviour {
	public bool hasMainKey;
	public Image keyImg;
	AudioSource Audio;


	void OnTriggerEnter(Collider hit){
		if (hit.CompareTag ("Puddle")) {
			Puddle ();
		}
		if(hit.CompareTag("Key")){
			CollectKey();
			hit.gameObject.SetActive(false);
		}
	}

	public void Move(Vector3 pos){
		transform.position = pos;
		Step ();
	}

	public void Move2Door(Vector3 pos){
		if (hasMainKey) {
			transform.position = pos;
			Step ();
		}	
	}
	void Puddle(){
		//small nearby alert
		// puddle noise
	}

	void Step(){
		//play step noise
	}

	//key
	void CollectKey(){
		hasMainKey = true;
		keyImg.color = Color.white;
	}
}
