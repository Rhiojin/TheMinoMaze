using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Dirty : MonoBehaviour {
	Image image;
	Animation Anim;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		Anim = GetComponent<Animation> ();

	}
	void Update(){
		if (Anim.isPlaying) {
			image.SetVerticesDirty();
		}
	}
}
