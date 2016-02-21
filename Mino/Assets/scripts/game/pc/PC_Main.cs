using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PC_Main : MonoBehaviour {

	[Header("UI Holders")]
	public GameObject inGameUI;

	[Header("AP")]
	public Text apText;
	//public Text webAPtext;
	public int ap;
	public int apMax;

	[Header("Inv")]
	public PC_Inv invGM;

	[Header("Key")]
	public bool hasMainKey;
	public Image keyImg;

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
		turnScript = GameObject.Find("Manager").GetComponent<turnManager>();
		turnScript.OnPcTurn += NewTurn;

		Invoke ("Go", 1f);
	}

	void Update()
	{
		/// can we delete this??
		if(Input.GetKeyDown(KeyCode.M))
		{
			print("attempting crazy code");
			managerScript.SpawnMinotaur(transform.position);
		}
	}


	void Go(){
		transform.Translate (Vector3.forward * 2f);
	}

	void OnTriggerEnter(Collider hit){
		if (hit.CompareTag ("Puddle")) {
			Puddle ();
		}
		if(hit.CompareTag("Key")){
			CollectKey();
			hit.gameObject.SetActive(false);
		}
		if(hit.CompareTag("Item")){
			CollectItem(hit.gameObject.name);
			hit.gameObject.SetActive(false);
		}
	}
	
	public void Move(Vector3 pos){
		if (ap > 0 && myTurn) {
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
		

	
	void UpdateAPText(){
		apText.text = "AP: " + ap.ToString ();
		//webAPtext.text = apText.text;
	}
		

	// specific objs --------------------------------------------------------------

	//key
	void CollectKey(){
		hasMainKey = true;
		keyImg.color = Color.white;
	}
	//items
	void CollectItem(string name){
		if (name == "Torch") {
			CollectTorch ();
		}
	}
	// torch
	void CollectTorch(){
		invGM.PlusItem (1, 10);
	}
}
