using UnityEngine;
using System;
using System.Collections;

public class PlayerAnimationEventHandler : MonoBehaviour {

    public Action CarillonHitHandler;
    public Action HammerDroppedEventHandler;
	public Action BellRingHandler;
	public Action BellRingTerminatedHandler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCarillonHit() {
        if (this.CarillonHitHandler != null) {
            this.CarillonHitHandler();
        }
    }

    void OnHammerDropped() {
        if (this.HammerDroppedEventHandler != null) {
            this.HammerDroppedEventHandler();
        }
    }

	void OnBellRing() {
		if (this.BellRingHandler != null) {
			this.BellRingHandler();
		}
	}

	void OnBellRingTerminated() {
		if (this.BellRingTerminatedHandler != null) {
			this.BellRingTerminatedHandler();
		}
	}

}
