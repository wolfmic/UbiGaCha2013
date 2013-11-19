using UnityEngine;
using System.Collections;

public class Dukable : MonoBehaviour {

	private float _ducking = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Duck(float value) {
		ClearDucking();
		_ducking = value;
		this.audio.volume *= _ducking;
	}

	void ClearDucking() {
		this.audio.volume *= 1 / _ducking;
		_ducking = 0f;
	}

}
