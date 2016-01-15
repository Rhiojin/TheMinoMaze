using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PC_Main : MonoBehaviour {

	[Header("AP")]
	public Text apText;
	public int ap;
	public int apMax;

	[Header("Inv")]
	private int InvID;
	private int InvMax;

	[Header("Key")]
	public bool hasMainKey;
	public Image keyImg;

	//audio 
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
			UpdateAPText();
		}
	}

	public void Move2Door(Vector3 pos){
			ap--;
			UpdateAPText();
			transform.position = pos;
			Step ();
	}

	public void PickUp(GameObject obj){
		if(ap > 0){
			if(obj.CompareTag("Key")){ CollectKey();}
			ap--;
			UpdateAPText();
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
		UpdateAPText ();
	}
	
	void UpdateAPText(){
		apText.text = "AP: " + ap.ToString ();
	}

	// INV -----------------------------------------------------------------------
	public void NextInv(){
		InvID++;
		if (InvID > InvMax) {
			InvID = 0;
		}
		UpdateInv ();
	}

	public void PrevInv(){
		InvID--;
		if (InvID < 0) {
			InvID = InvMax;
		}
		UpdateInv ();
	}

	void UpdateInv(){
		if (InvID == 0) {
			// nothing 
			// change UI PIC
			// change hand to have nothing
		}
	}

}
