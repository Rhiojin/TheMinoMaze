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

		StartCoroutine( CalculateSpawnPos(startCell,Vector3.forward) );

	}

	IEnumerator CalculateSpawnPos(Vector3 position,Vector3 rayDirection)
	{
		// may need to keep track of directions checked
		
		//while ray has not hit a wall or calculation has not taken too long
		while(wallHit == false || rayTimeout < 2)
		{
			if(Physics.Raycast(position, rayDirection, out m_hit,1*cellSize))
			{
				if(m_hit.collider.CompareTag("Cell"))
				{
					//block counting to gauge distance travelled
					blockCount++;
					//while loop continues from new point
					position = m_hit.transform.position;
				}
				else if(m_hit.collider.CompareTag("Wall"))
				{
					wallHit = true;
					//if block count is > 1 continue search
					if(blockCount > 1)
					{
						wallHit = false;
						rayTimeout = 0;
						blockCount = 0;


						while(wallHit == false || rayTimeout < 2)
						{
							if(Physics.Raycast(allDirections[secondRayDirectionIndex], rayDirection, out m_hit,1*cellSize))
							{
								if(m_hit.collider.CompareTag("Cell"))
								{
									blockCount++;
								}
								else if(m_hit.collider.CompareTag("Wall"))
								{
									wallHit = true;
									if(blockCount >= 1)
									{
									}
									else
									{
										//let loop run again with new direction
										secondRayDirectionIndex++;
										if(secondRayDirectionIndex >= allDirections.Length)
										{
											break;
										}
									}
								}
							}
							yield return null;
							rayTimeout += Time.deltaTime;
						}

					}
					//otherwise start from origin in a new direction
					else
					{
						//Recursive
					}

				}
				//otherwise start from origin in a new direction
				else
				{
					//Recursive
				}
			}
			yield return null;
			rayTimeout += Time.deltaTime;
		}



	}

}
