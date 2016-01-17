using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuControl : MonoBehaviour {
	
	public GameObject panelInfo;
	public GameObject panelSurvey;

	public void Play(){
		Application.LoadLevel ("01");
	}

	public void Clear(){
		panelInfo.SetActive (false);
		panelSurvey.SetActive (false);
	}

	public void Survey(){
		panelSurvey.SetActive (true);
	}

	public void Info(){
		panelSurvey.SetActive (true);
	}

	public void Quit(){
		Application.Quit ();
	}
}
