using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	float cellSize;
	float moveSpeed =3;

	turnManager turnScript;

	bool moving = false;
	bool foundNextPos = false;
	bool calculating = false;
	float searchTimeout = 0;

	Vector3 nextPos = Vector3.zero;
	Vector3[] allDirections = new Vector3[4];
	int firstRayDirectionIndex = 0;
	Vector3 direction = Vector3.zero;
	RaycastHit m_hit;
	List<Vector3> possibleNextPos = new List<Vector3>();


	void Start(){
		///need to have a fade in black out
		/// 
		/// 
		/// 
		turnScript = GameObject.Find("Manager").GetComponent<turnManager>();
		cellSize = GameObject.Find("MazeGen").GetComponent<makemaze>().cubesize;
		turnScript.OnMinoTurn += NewTurn;

		allDirections[0] = transform.forward;
		allDirections[1] = transform.right;
		allDirections[2] = -transform.forward;
		allDirections[3] = -transform.right;

	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(minoTurn)
		{
			if(AP > 0)	
			{
				Move();
			}
			else
			{
				Endturn();
			}
		}

	}

	public void NewTurn()
	{
		print("mino turn");
		minoTurn = true;
		moving = false;
		foundNextPos = false; 
		searchTimeout = 0;
		firstRayDirectionIndex = 0;

		AP = maxAp;
		possibleNextPos.Clear();
	}

	public void Endturn()
	{
		if(minoTurn)
		{
			minoTurn = false;
			turnScript.MinoEndTurn();
		}
	}

	void Move()
	{
		if(!foundNextPos && !calculating && minoTurn)
		{
			allDirections[0] = transform.forward;
			allDirections[1] = transform.right;
			allDirections[2] = -transform.forward;
			allDirections[3] = -transform.right;
			print("calculating next pos");
			CalculateNextPos();
		}
		else
		{
			if(moving == false) moving = true;
			transform.LookAt(nextPos);

			transform.position += transform.forward*moveSpeed*Time.deltaTime;
			if(CheckRange())
			{
				print("used 1 Ap, "+AP.ToString()+" ap left");
				moving = false;
				foundNextPos = false; 
				searchTimeout = 0;
				firstRayDirectionIndex = 0;
				AP--;
				possibleNextPos.Clear();
			}
		}
	}

	void CalculateNextPos()
	{
		calculating = true;
		//firstRayDirectionIndex = Random.Range(0,allDirections.Length);
		direction = transform.forward;

		while(foundNextPos == false && searchTimeout < 3)
		{
			if(Physics.Raycast(transform.position, direction, out m_hit,1*cellSize))
			{
				Debug.DrawLine(transform.position,m_hit.point, Color.cyan,5);
				if(m_hit.collider.CompareTag("Cell"))
				{
					print("found a cell");
//					foundNextPos = true;
//					nextPos = m_hit.transform.position;


					//Get all possible directions on calculate
					possibleNextPos.Add(m_hit.transform.position);

					if(firstRayDirectionIndex >= allDirections.Length)
					{
						foundNextPos = true;
						if(possibleNextPos.Count > 0)
						{
							nextPos = possibleNextPos[Random.Range(0,possibleNextPos.Count)];
						}
						else
						{
							print("failed to find a next pos");
							foundNextPos = true;
							Endturn();
						}
					}
					else
					{
						print("changing direction");
						direction = allDirections[firstRayDirectionIndex];
						firstRayDirectionIndex++;
					}



				}
				else if(m_hit.collider.CompareTag("Wall"))
				{
//					if( Random.value < 0.5f)
//						direction = allDirections[firstRayDirectionIndex];



					if(firstRayDirectionIndex >= allDirections.Length)
					{
						foundNextPos = true;
						if(possibleNextPos.Count > 0)
						{
							nextPos = possibleNextPos[Random.Range(0,possibleNextPos.Count)];
						}
						else
						{
							print("failed to find a next pos");
							foundNextPos = true;
							Endturn();
						}
					}
					else
					{
						direction = allDirections[firstRayDirectionIndex];
						firstRayDirectionIndex++;
					}
				}
			}
			else
			{
				

				if(firstRayDirectionIndex >= allDirections.Length)
				{
					//firstRayDirectionIndex = allDirections.Length-1;
					foundNextPos = true;
					//nextPos = transform.position;
					print("didnt find next pos");
				}
				else
				{
					direction = allDirections[firstRayDirectionIndex];
					firstRayDirectionIndex++;
				}
			}
			searchTimeout += 0.02f;
			//yield return null;
		}
		calculating = false;
	}

	bool CheckRange()
	{
		if(Vector3.Distance(transform.position, nextPos) <= 0.15f)
		{
			return true;
		}
		else return false;
	}
}
