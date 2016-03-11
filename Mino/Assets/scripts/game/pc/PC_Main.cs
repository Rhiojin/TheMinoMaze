using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PC_Main : MonoBehaviour {

	[Header("UI Holders")]
	public GameObject inGameUI;
	/*
	[Header("AP")]
	public Text apText;
	//public Text webAPtext;
	public int ap;
	public int apMax;
*/
	[Header("Stats")]
	public bool dead;
	public bool hasMainKey;

	[Header("Inv")]
	public PC_Inv invGM;


	//audio 
	[Header("Audio")]
	public AudioSource audioSorc;
	public AudioClip[] audioClips;

	[Header("Minotaur")]
	public float conspicuous = 0;
	public float slidingScale = 50;

	public Manager managerScript; // MANAGER FOR WHAT????
	int quick = 0;

	bool myTurn = true;
	turnManager turnScript;


	void Start(){
		///need to have a fade in black out
		/// 
		dead = false;
		turnScript = GameObject.Find("Manager").GetComponent<turnManager>();
		InvokeRepeating ("GiveMino", 2f, Random.Range (2, 6));
		//turnScript.OnPcTurn += NewTurn;

		Invoke ("Go", 1f);
	}




	void Go(){
		transform.Translate (Vector3.forward * 2f);
	}

	void OnTriggerEnter(Collider hit){
		if (hit.CompareTag ("enemy")) {
			Death (hit.gameObject.transform.position);
		}

		if (hit.CompareTag ("Puddle")) {
			Puddle ();
		}
		if(hit.CompareTag("Item")){
			CollectItem(hit.gameObject.name);
			hit.gameObject.SetActive(false);
		}
	}
	
	public void Move(Vector3 pos){
		//if (ap > 0 && myTurn) {
		if(!dead){
			transform.position = pos;
			Step ();
		//	ap--;
		//	UpdateAPText();
		}
	}

	public void Move2Door(Vector3 pos){
		//	ap--;
		//	UpdateAPText();
		if (!dead) {
			transform.position = pos;
			Step ();
		}
	}
				
	/*
	public void NewTurn(){
		print("player turn");
		myTurn = true;
		ap = apMax;
		//inGameUI.SetActive (true);
		UpdateAPText ();
	}
	// END TURN BUTTON
	public void EndTurn(){
		if(myTurn)
		{
			turnScript.PcEndTurn(transform.position);
			myTurn = false;
			//inGameUI.SetActive (false);
		}
	}

*/

	void GiveMino(){
		turnScript.PcEndTurn(transform.position);
	}

	void Puddle(){
		//small nearby alert
		// puddle noise
		conspicuous += 3;
	}

	void Step(){
		audioSorc.clip = audioClips [0];
		audioSorc.Play ();
		conspicuous += 1;
	}
		
	public void Death(Vector3 pos){
		Debug.Log ("DEATH");
		dead = true;
		inGameUI.SetActive (false);
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<Rigidbody> ().AddForceAtPosition (Vector3.forward, pos);
		invGM.Death ();
	}


	/*
	void UpdateAPText(){
		apText.text = "AP: " + ap.ToString ();
		//webAPtext.text = apText.text;
	}
*/

	// specific objs --------------------------------------------------------------


	//items
	void CollectItem(string name){
		if (name == "Torch") {
			CollectTorch ();
		}
		if (name == "Key") {
			CollectKey ();
		}
	}
	// torch ID 1
	void CollectTorch(){
		invGM.PlusItem (1, 10);
	}
	//key ID 2
	void CollectKey(){
		hasMainKey = true;
		invGM.PlusItem (2, 1);
	}
}
