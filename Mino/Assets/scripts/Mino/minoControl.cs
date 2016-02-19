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
	int pcSearchIndex = 0;
	bool pcFound = false;
	Vector3 direction = Vector3.zero;
	RaycastHit m_hit;
	List<Vector3> possibleNextPos = new List<Vector3>();
	bool inRangeForAttack = false;


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
		pcSearchIndex = 0;

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
			//update local positoning for raycasts
			allDirections[0] = transform.forward;
			allDirections[1] = transform.right;
			allDirections[2] = -transform.forward;
			allDirections[3] = -transform.right;
			print("calculating next pos");
			pcFound = false;
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

		//need to first do a search here for pc
		//CURRENTLY BROKEN
//		while(pcSearchIndex < allDirections.Length && !pcFound)
//		{
//			//search 4 cells out
//			if(Physics.Raycast(transform.position, allDirections[pcSearchIndex], out m_hit,4*cellSize))
//			{
//				Debug.DrawLine(transform.position,m_hit.point, Color.red,5);
//				if(m_hit.collider.CompareTag("Wall"))
//				{
//					print("SEARCH");
//					pcSearchIndex++;
//
//				}
//				else if(m_hit.collider.CompareTag("Player"))
//				{
//					//end search
//					pcSearchIndex = allDirections.Length;
//
//					print("AND DESTROY");
//					pcFound = true;
//					pcSearchIndex = allDirections.Length;
//					if(Physics.Raycast(transform.position, allDirections[pcSearchIndex], out m_hit,1*cellSize))
//					{
//						Debug.DrawLine(transform.position,m_hit.point, Color.red,5);
//						if(m_hit.collider.CompareTag("Cell"))
//						{
//							
//							nextPos = m_hit.transform.position;
//
//						}
//					}
//				}
//			}
//			else
//			{
//				//if nothing hit
//				pcSearchIndex++;
//			}
//			yield return null;
//		}


	

		int m_layermask = 1 << LayerMask.NameToLayer("playerLayer");
		foreach(Vector3 dir in allDirections)
		{
			print("SEARCH");
			if(Physics.Raycast(transform.position, dir, out m_hit,4*cellSize,m_layermask))
			{
				Debug.DrawLine(transform.position,m_hit.point, Color.red,5);
				if(m_hit.collider.CompareTag("Player"))
				{
					print("AND DESTROY");
					print(m_hit.distance);
					pcSearchIndex = allDirections.Length;
				
					if(m_hit.distance < 2.2f)
					{
						print("IM IN RANGE FOR DEATH!");
						pcFound = true;
						foundNextPos = true;
						nextPos = transform.position;
					}	
					else
					{
						if(Physics.Raycast(transform.position, dir, out m_hit,1*cellSize))
						{
							Debug.DrawLine(transform.position,m_hit.point, Color.red,5);
							if(m_hit.collider.CompareTag("Cell"))
							{
								//found direct path 
								pcFound = true;
								foundNextPos = true;
								nextPos = m_hit.transform.position;

							}
						}
					}
				}
			}
		}





		//if pc not found
		while(foundNextPos == false && searchTimeout < 3 && !pcFound)
		{
			if(Physics.Raycast(transform.position, direction, out m_hit,1*cellSize))
			{
				Debug.DrawLine(transform.position,m_hit.point, Color.cyan,5);
				if(m_hit.collider.CompareTag("Cell"))
				{
					print("found a cell");
//					foundNextPos = true;
//					nextPos = m_hit.transform.position;

					if(direction == transform.forward)
					{
						//stop search to bias towards forward travel
						firstRayDirectionIndex = allDirections.Length;
					}

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
						//increment after assignment to avoid index errors
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
						//increment after assignment to avoid index errors
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
