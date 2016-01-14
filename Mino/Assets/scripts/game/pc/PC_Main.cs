using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PC_Main : MonoBehaviour {
	public bool hasMainKey;
	public Image keyImg;
	public Text apText;
	public int ap;
	public int apMax;
	public Button endTurnButton;
	AudioSource Audio;


	void Start(){
		///need to have a fade in black out
		Invoke ("Go", 1f);
	}

	void Go(){
		transform.Translate (Vector3.forward * 1);
	}

	void OnTriggerEnter(Collider hit){
		if (hit.CompareTag ("Puddle")) {
			Puddle ();
		}
	}
	
	public void Move(Vector3 pos){
		if (ap > 0) {
			transform.position = pos;
			Step ();
			ap--;
			UpdateText();
		}
	}

	public void Move2Door(Vector3 pos){
			ap--;
			UpdateText();
			transform.position = pos;
			Step ();
	}

	public void PickUp(GameObject obj){
		if(ap > 0){
			if(obj.CompareTag("Key")){ CollectKey();}
			ap--;
			UpdateText();
			obj.SetActive(false);
		}
	}

	// END TURN BUTTON
	public void EndTurn(){
		//GM END TURN - MONSTERS TURN
		// animation and shit
		NewTurn ();
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

	public void NewTurn(){
		ap = apMax;
		UpdateText ();
	}
	
	void UpdateText(){
		apText.text = "AP: " + ap.ToString ();
	}

}
