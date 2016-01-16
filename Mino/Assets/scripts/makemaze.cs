using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//even location in the array can be removed to create doors, traps etc. odd numbers must always persist to avoid locked out locations

public class makemaze : MonoBehaviour {
	// makemaze.js  - MichShire Feb2015
	[Header("Prefabs")]
	public GameObject mazecube;
	public GameObject cell;
	public GameObject door;
	public GameObject key;
	public GameObject[] miscItems;
	[Header("Stats")]
	public float cubesize=3;
	public Material brick;
	private string MazeString = "";
	private int width;
	private int height;
	private static string[] mazearray;
	private bool t = false ;
	GameObject ptype;
	public Manager managerScript;
	float xpos;
	float xpos2;
	float ypos2;

	List<Vector3> pathway = new List<Vector3>();
	
	void Start() {
		//yield return new WaitForSeconds(1);
		//MazeString = mazecube.GetComponent<mazegen>().MazeString;
		MazeString = GetComponent<mazegen>().MazeString;
		width = GetComponent<mazegen>().width;
		height = GetComponent<mazegen>().height;
		print ("mazegen width = " + width + " height = " + height +" array = \n" +MazeString);
		makeMaze();
	}
	
	void makeMaze() {
		
		var mazearray = MazeString.Split("\n"[0]);
		//print (mazearray[0]);
		//mod = mazearray[0];  index = 9;
		//mazearray[0] = mod.Substring(0, index) + '0' + mod.Substring(index + 1);
		
		RemoveBlocks(mazearray);

		int thirdWayW = Mathf.RoundToInt(width/3);
		int thirdWayH = Mathf.RoundToInt(height/3);
		
		for (int i = 0; i <width; i++)  {
			//print ("array " + i + " = " + mazearray[i]);
			for (int j = 0; j <height; j++)  
			{
//				if(i > thirdWayW && i < thirdWayW*2 && j > thirdWayH && j < thirdWayH*2)
//				{
//					if(i%2 != 0)
//					{
//						//make hazard or door or something
//
//					}
//				}
				var st=mazearray[i];
				//print ("mazei= " + i + mazearray[i] );
				//print ("substring= " + st.Substring(0,1) );
				if (st.Substring(j,1)=="X")  {      // make a block if 'X' ...
					ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
					ptype.transform.position = new Vector3(j * cubesize, 0.5f+(cubesize/2), i *cubesize);
					ptype.transform.localScale = new Vector3(cubesize, cubesize, cubesize);
					
					if (brick != null)  { ptype.GetComponent<Renderer>().material = brick; }
					ptype.transform.parent = transform;
					ptype.tag = "Wall";
				}
				else{
					//save to array to add key later - but the keey should usually come before the door?, maybe?

					Vector3 point = new Vector3(j*cubesize,0, i*cubesize);
					pathway.Add(point);
				}


				//this if block isnt working as intended but w/e for now
				if(i >= thirdWayW*2 && i < thirdWayW*3 && j >= thirdWayH*2 && j < thirdWayH*3)
				{
					if(i%2 != 0)
					{
						//make hazard or door or something
						ptype.GetComponent<Renderer>().material.color = Color.green;
					}
				}
				t=!t;
				// just to show colored blocks every second block these are the maze building blocks
				if (t==true && (i==0 || i==2 || i==4 ||i==6 || i==8 || i==10 || i==12)){
					    ptype.GetComponent<Renderer>().material.color = Color.red;
				}

			}
		}
		//t=1;
		//return;
		managerScript.GetSpawnPoint(xpos);
		managerScript.GetExitPoint(xpos2, ypos2);
		AddPathway ();
		PlaceKey ();
	}

	void PlaceKey(){
		int r = Random.Range (0, pathway.Count - 4);
		GameObject keyTemp;

		keyTemp = (GameObject)Instantiate (key, pathway [r], Quaternion.identity);
		keyTemp.transform.position = new Vector3 (keyTemp.transform.position.x, 1f, keyTemp.transform.position.z);
		keyTemp = null;
	}

	void MiscItem(Transform point){
		GameObject miscTemp;
		int r = Random.Range (0, 100);
		int rItem = Random.Range (0, miscItems.Length);
		if (r < 20) {
			miscTemp = (GameObject)Instantiate(miscItems[rItem], point.position, point.rotation);
			miscTemp = null;
		}
	}

	void AddPathway(){
		GameObject cellTemp;

		for(int i = 0; i< pathway.Count; i++){
			if(i == 0 || i == pathway.Count-1){
				Debug.Log("HIT");
				cellTemp = (GameObject)Instantiate(door, new Vector3(pathway[i].x, 1.5f, pathway[i].z), Quaternion.identity);
				cellTemp.transform.rotation = new Quaternion(0,-180,0,0);
				cellTemp.name = "Exit";
				if(i ==0){
					//start
					cellTemp.name = "Entrance";
					cellTemp.transform.rotation = new Quaternion(0,0,0,0);
					cellTemp.GetComponent<doorBox>().Entrance();
				}
			}
			else{
				cellTemp = (GameObject)Instantiate(cell, new Vector3(pathway[i].x, 1.5f, pathway[i].z), Quaternion.identity);
				cellTemp.name = "Cell("+i.ToString()+")";
				MiscItem(cellTemp.transform);
			}
			cellTemp=null;
		}
	}

	// ====================
	void RemoveBlocks(string[] mazearray) {
		string mod; int index; 

		// entrance
		mod = mazearray[0];  
		index = GetOddNumber();
		xpos = index*cubesize;
		mazearray[0] = mod.Substring(0, index) + '0' + mod.Substring(index + 1);
		
		// exit
		mod = mazearray[height-1];  
		//index = width-2;
		index = GetOddNumber();
		xpos2 = index*cubesize;
		ypos2 = (height-1)*cubesize;
		mazearray[height-1] = mod.Substring(0, index) + '0' + mod.Substring(index + 1);
	}

	int GetOddNumber()
	{
		int d = Random.Range(0,width);
		if(d%2 == 0) d++;
		if(d == width) d -=2;
		return d;
	}
}
