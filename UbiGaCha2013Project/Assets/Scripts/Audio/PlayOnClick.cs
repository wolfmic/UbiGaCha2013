using UnityEngine;
using System.Collections;

public class PlayOnClick : MonoBehaviour {

	public float ducking = -1f;

	private MusicMaster _musicMaster;

	// Use this for initialization
	void Start () {
		_musicMaster = GameObject.Find("MusicMaster").GetComponent<MusicMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			this.Play();
	}

	void Play() {
		_musicMaster.PlayDucking(this.audio);
	}
}
