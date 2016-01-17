using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

	Vector3 spawnPoint = Vector3.zero;
	public GameObject PC;
	public GameObject exitCanvas;

	public Text tiltReadout;
	Vector3 tilt;

	float cellSize;
	Vector3 startCell;
	Vector3 currentCell;
	Vector3 minoSpawnPoint;
	Vector3 rayDirection;
	int blockCount = 0;
	bool wallHit = false;
	Ray m_ray;
	RaycastHit m_hit;
	float rayTimeout = 0;
	Vector3[] allDirections = new Vector3[4];
	int firstRayDirectionIndex = 0;
	int secondRayDirectionIndex = 0;
	bool spawnPointFound = false;

	void Start ()
	{
		tiltReadout.text = "";
		cellSize = GameObject.Find("MazeGen").GetComponent<makemaze>().cubesize;
		allDirections[0] = Vector3.forward;
		allDirections[1] = Vector3.right;
		allDirections[2] = -Vector3.forward;
		allDirections[3] = -Vector3.right;
	}
	
	// Update is called once per frame
//	void Update () 
//	{
//		
//		tilt = Input.acceleration;
//		tiltReadout.text = tilt.ToString();
//
//	}

	public void GetSpawnPoint(float xpos)
	{
		spawnPoint.x = xpos;
		spawnPoint.y = 1.5f;
		PC.transform.position = spawnPoint;
	}

	public void GetExitPoint(float xpos, float zpos)
	{
		spawnPoint.x = xpos;
		spawnPoint.z = zpos;
		spawnPoint.y = 1.5f;
		exitCanvas.transform.position = spawnPoint;
	}

	public void SpawnMinotaur(Vector3 pos)
	{
		startCell = pos;
		currentCell = startCell;
		wallHit = false;
		rayTimeout = 0;
		firstRayDirectionIndex = 0;
		secondRayDirectionIndex = 0;

		rayDirection = Vector3.forward;
		spawnPointFound = false;

		//StartCoroutine( CalculateSpawnPos(startCell) );
		CalculateSpawnPos(startCell);

	}

	void CalculateSpawnPos(Vector3 position)
	{
		// may need to keep track of directions checked
		
		//while ray has not hit a wall or calculation has not taken too long
		while(wallHit == false || rayTimeout < 2 || spawnPointFound == false)
		{
			rayDirection = allDirections[firstRayDirectionIndex]* -1 ; // need to be opposite

			if(Physics.Raycast(position, allDirections[firstRayDirectionIndex], out m_hit,1*cellSize))
			{
				Debug.DrawLine(position,m_hit.point, Color.cyan,2);
				if(m_hit.collider.CompareTag("Cell"))
				{
					print("found a cell");
					//block counting to gauge distance travelled
					blockCount++;
					//while loop continues from new point
					position = m_hit.transform.position;
					currentCell = m_hit.transform.position;

					GameObject ptype;
					ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
					ptype.transform.position = minoSpawnPoint;
				}
				else if(m_hit.collider.CompareTag("Wall"))
				{
					print("found a wall");
					wallHit = true;
					//if block count is > 1 continue search
					if(blockCount >= 1)
					{
						print("continuing");
						wallHit = false;
						rayTimeout = 0;
						blockCount = 0;
						secondRayDirectionIndex = 0;


						while(wallHit == false || rayTimeout < 2)
						{
							//avoid doubling back
							if(rayDirection == allDirections[secondRayDirectionIndex]) secondRayDirectionIndex++;

							if(Physics.Raycast(currentCell, allDirections[secondRayDirectionIndex], out m_hit,1*cellSize))
							{
								Debug.DrawLine(position,m_hit.point, Color.cyan,2);
								if(m_hit.collider.CompareTag("Cell"))
								{
									blockCount++;
									print("found a cell, 2nd level");
									currentCell = m_hit.transform.position;
								}
								else if(m_hit.collider.CompareTag("Wall"))
								{
									print("found a wall, 2nd level");
									wallHit = true;
									if(blockCount >= 1)
									{
										print("and ending");
										rayTimeout = 3;
										minoSpawnPoint = currentCell;
										spawnPointFound = true;
										break;
									}
									else
									{
										//let loop run again with new direction
										print("wall too close, changing direction, 2nd level");
										wallHit = false;
										secondRayDirectionIndex++;
										if(secondRayDirectionIndex >= allDirections.Length)
										{
											break;
										}
									}
								}
								else{
									print("incorrect collision");
									wallHit = true;
									//rayTimeout = 3;
									break;
								}
							}
							//yield return null;
							rayTimeout += Time.deltaTime;
						}

					}
					//otherwise start from origin in a new direction
					else
					{
						print("but not continuing, changing direction on first ray");
						//Recursive
						firstRayDirectionIndex++;
						if(firstRayDirectionIndex >= allDirections.Length)
						{
							break;
							rayTimeout = 3;
						}
					}

				}
				else{
					print("incorrect collision");
					rayTimeout = 3; 
					wallHit = true;
					break;
				}

			}
			else{
				print("ray didnt find anything");
			}
			//yield return null;
			rayTimeout += Time.deltaTime;
		}

		if(spawnPointFound)
		{
			print("awww yiss");
			GameObject ptype;
			ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
			ptype.transform.position = minoSpawnPoint;
			ptype.name = ("YO MOOOMMMAAAA");
		}
		else
		{
			print("crap!");
		}

	}

}
