using UnityEngine;
using System.Collections;

//even location in the array can be removed to create doors, traps etc. odd numbers must always persist to avoid locked out locations

public class makemaze : MonoBehaviour {
	// makemaze.js  - MichShire Feb2015
	
	public GameObject mazecube;
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
		
		
		for (int i = 0; i <width; i++)  {
			//print ("array " + i + " = " + mazearray[i]);
			for (int j = 0; j <height; j++)  {
				var st=mazearray[i];
				//print ("mazei= " + i + mazearray[i] );
				//print ("substring= " + st.Substring(0,1) );
				if (st.Substring(j,1)=="X")  {      // make a block if 'X' ...
					ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
					ptype.transform.position = new Vector3(j * cubesize, 0.5f+(cubesize/2), i *cubesize);
					ptype.transform.localScale = new Vector3(cubesize, cubesize, cubesize);
					
					if (brick != null)  { ptype.GetComponent<Renderer>().material = brick; }
					ptype.transform.parent = transform;
				}
				// just to show colored blocks every second block
				t=!t;
				if (t==true && (i==0 || i==2 || i==4 ||i==6 || i==8 || i==10 || i==12)){
					//    ptype.GetComponent.<Renderer>().material.color = Color.red;
				}

			}
		}
		//t=1;
		//return;
		managerScript.GetSpawnPoint(xpos);
		managerScript.GetExitPoint(xpos2, ypos2);
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
