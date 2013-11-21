using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public string NextLevelName;

	private Animator _anim;
	private bool _opened;

	// Use this for initialization
	void Start () {
		_anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Open() {
		_anim.SetTrigger("Open");
		_opened = true;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player" && _opened) {
			Application.LoadLevel(this.NextLevelName);
		}
	}


}
