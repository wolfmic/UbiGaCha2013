using UnityEngine;
using System;
using System.Collections;

public class PlayerBell : MonoBehaviour {

	public Action BellRingTerminatedHandler;
	public Action BellCooldownTerminatedHandler;

	private Animator _anim;
	//private Game

	// Use this for initialization
	void Start () {
		_anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Ring(string level) {
		_anim.SetTrigger("Bubble" + level);
	}

	void OnBellRingTerminated() {
		if (this.BellRingTerminatedHandler != null) {
			this.BellRingTerminatedHandler();
		}
	}

	void OnBellCooldownTerminatedHandler() {
		if (this.BellCooldownTerminatedHandler != null) {
			this.BellCooldownTerminatedHandler();
		}
	}


}
