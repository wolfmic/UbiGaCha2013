using UnityEngine;
using System.Collections;

public class NextOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		GameObject.Find("Music").GetComponent<MusicPlayer>().Next();
	}
}
