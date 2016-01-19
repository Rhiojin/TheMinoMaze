using UnityEngine;
using System.Collections;

public class minoControl : MonoBehaviour {

	//TODO -
	//Logic is - 
	//find pc 
	// travel to pc
	// if 1 block away from pc - shit on that fool
	// if not find random direction -> travel in direction
	// sub AP
	//find PC

	bool minoTurn = false;
	int AP = 3;
	public int maxAp = 3;


	turnManager turnScript;


	void Start(){
		///need to have a fade in black out
		/// 
		/// 
		/// 
		turnScript = GameObject.Find("Manager").GetComponent<turnManager>();
		turnScript.OnMinoTurn += NewTurn;

	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(AP > 0)	
		{
			//MOVE
		}
		else
		{
			Endturn();
		}

	}

	public void NewTurn()
	{
		minoTurn = true;
		AP = maxAp;
	}

	public void Endturn()
	{
		if(minoTurn)
		{
			minoTurn = false;
			turnScript.MinoEndTurn();
		}
	}
}
