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

	[Header("Minotaur")]
	public float conspicuous = 0;
	public float slidingScale = 50;

	public Manager managerScript;
	int quick = 0;

	void Start(){
		///need to have a fade in black out

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
		MinotaurSpawnChance();
	}


	void Puddle(){
		//small nearby alert
		// puddle noise
		conspicuous += 3;
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

	void MinotaurSpawnChance()
	{
		//print("Have Fun!");
//		float chance = conspicuous * 5;
//		chance = Random.Range(0+chance, 100);
//		if(chance >= slidingScale)
//		{
//			print("attempting crazy code");
//			//managerScript.SpawnMinotaur(transform.position);
//		}

	}



}
