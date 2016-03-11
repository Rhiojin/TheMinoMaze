using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PC_Inv : MonoBehaviour {

	[Header("UI")]
	public Image invImageHolder;
	public Text invName;
	public Text invAmount;

	[Header("Stats")]
	public int ID;
	bool usingTorch;
	public float timer;
	[Header("GameObjects")]
	public GameObject itemInHand;

	[Header("Items")]
	public InvItem[] items; 

	void Start(){
		SetItem (ID);
	}

	[System.Serializable]
	public class InvItem{
		public string name; 
		public int amount;
		public Sprite img;
		public GameObject obj;
	}

	void Update(){
		if (usingTorch) {
			timer += 1 * Time.deltaTime;
			if (timer > 10) {
				MinusAmount ();
				timer = 0;
			}
		}
	}

	public void Right(){
		ID++;
		if (ID >= items.Length) {
			ID = 0;
		}
		SetItem (ID);
	} 

	public void Left(){
		ID--;
		if (ID < 0) {
			ID = items.Length-1;
		}
		SetItem(ID);
	}

	void SetItem(int i){
		invName.text = items [i].name; 
		invAmount.text = items [i].amount.ToString();
		invImageHolder.sprite = items [i].img;
		UseTorch (false);

		if (items[i].amount == 0 || i == 0) {
			invAmount.text = "";
			ObjInHand (0);
		}

		if (items [i].amount > 0) {
			ObjInHand (i);
		} 
		if (ID == 1) {
			UseTorch (true);
			//timer = 0;	
		}
	}

	public void PlusItem(int id, int amount){
		items [id].amount+=amount;
	}

	public void MinusAmount(){
		if (items [ID].amount == 0) {
			ObjInHand (0);
			if (ID == 1) {
				UseTorch (false);
			}
			return;
		} else {
			items [ID].amount--;
			invAmount.text = items [ID].amount.ToString();
		}
	}

	void UseTorch(bool are){
		usingTorch = are;
	}

	public void Death(){
		itemInHand.transform.parent = null;
		itemInHand.GetComponent<BoxCollider> ().isTrigger = false;
		itemInHand.GetComponent<Rigidbody> ().useGravity = true;
		Invoke ("BackToMainMenu", 5f);
	}

	void BackToMainMenu(){
		Application.LoadLevel (0);
	}

	void ObjInHand(int i){
		foreach (InvItem item in items) {
			item.obj.transform.position = new Vector3 (-10, -10, -10);
			item.obj.transform.SetParent (null);
		}
			
		items [i].obj.gameObject.transform.position = itemInHand.transform.position;
		items [i].obj.gameObject.transform.transform.rotation = itemInHand.transform.rotation;
		items [i].obj.gameObject.transform.SetParent (itemInHand.transform);
	}

}
