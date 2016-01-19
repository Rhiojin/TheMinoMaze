using UnityEngine;
using System.Collections;

public class turnManager : MonoBehaviour {

	public enum CurrentTurn{
		playerTurn,
		minoTurn
	};
	public delegate void OnTurnChange();
	public event OnTurnChange OnPcTurn;
	public event OnTurnChange OnMinoTurn;
	public CurrentTurn turnTracker;

	Manager managerScript;

	void Start () 
	{
		managerScript = GetComponent<Manager>();
		turnTracker = CurrentTurn.playerTurn;
	}

	void FixedUpdate()
	{
		
	}

	// Update is called once per frame
	public void PcEndTurn (Vector3 pcPos) 
	{
		turnTracker = CurrentTurn.minoTurn;
		if(OnMinoTurn != null)
		{
			OnMinoTurn();
		}
		else
		{
			if(managerScript.SpawnMinotaur(pcPos))
			{
				print("spawn sucessful");
			}
			else
			{
				print("spawn failed");
			}
			MinoEndTurn();
		}

	}

	public void MinoEndTurn()
	{
		turnTracker = CurrentTurn.playerTurn;
		if(OnPcTurn != null)
		{
			OnPcTurn();
		}
	}
}
