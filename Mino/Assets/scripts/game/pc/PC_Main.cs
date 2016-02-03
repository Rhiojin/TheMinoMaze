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
	private int InvID;
	private int InvMax;

	[Header("Key")]
	public bool hasMainKey;
	public Image keyImg;

	//audio 
	AudioSource Audio;

	[Header("Minotaur")]
	public float conspicuous = 0;
	public float slidingScale = 50;

	public Manager managerScript;
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
		if(Input.GetKeyDown(KeyCode.M))
		{
			print("attempting crazy code");
			managerScript.SpawnMinotaur(transform.position);
		}
	}


	void Go(){
		transform.Translate (Vector3.forward * 1);
	}

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
		UpdateAPText ();
		//inGameUI.SetActive (true);

	}
	// END TURN BUTTON
	public void EndTurn(){
		if(myTurn)
		{
			myTurn = false;
			turnScript.PcEndTurn(transform.position);
			//inGameUI.SetActive (false);
		}
	}


	void Puddle(){
		//small nearby alert
		// puddle noise
		conspicuous += 3;
	}

	void Step(){
		//play step noise
	}
		

	
	void UpdateAPText(){
		apText.text = "AP: " + ap.ToString ();
		//webAPtext.text = apText.text;
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

	// specific objs --------------------------------------------------------------

	//key
	void CollectKey(){
		hasMainKey = true;
		keyImg.color = Color.white;
	}



}
