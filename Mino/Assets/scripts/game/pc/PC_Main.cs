using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PC_Main : MonoBehaviour {

	[Header("Stats")]
	public bool dead;
	public bool hasMainKey;

	[Header("Inv")]
	public PC_Inv invGM;
	public Bag bag;

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
		dead = false;
		turnScript = GameObject.Find("Manager").GetComponent<turnManager>();
		InvokeRepeating ("GiveMino", 2f, Random.Range (2, 6));
		Invoke ("Go", 1f);
	}




	void Go(){
		transform.Translate (Vector3.forward * 2f);
		bag.close ();
	}

	void OnTriggerEnter(Collider hit){
		if (hit.CompareTag ("Enemy")) {
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

		if(!dead){
			transform.position = pos;
			Step ();
			bag.close ();
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
		dead = true;
		bag.dead ();
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<Rigidbody> ().AddForceAtPosition (Vector3.forward, pos);
		invGM.Death ();
	}
		

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
