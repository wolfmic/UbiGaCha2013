using UnityEngine;
using System;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	private const float SCROLL_SPEED = 1;

	private bool _running = false;
	private GameObject _creditsSprite;
	private GameObject _menuEntries;
	private Vector3 _initialCreditsPosition;
	private Vector3 _initialMenuEntriesPosition;

	// Use this for initialization
	void Start () {
		_creditsSprite = GameObject.Find ("Credits");
		_menuEntries = GameObject.Find ("MenuEntries");

		_initialCreditsPosition = _creditsSprite.transform.position;
		_initialMenuEntriesPosition = _menuEntries.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (_running == false)
			return;
		_creditsSprite.transform.Translate(Vector2.up * SCROLL_SPEED * Time.deltaTime);
		if (_creditsSprite.transform.position.y > (_creditsSprite.renderer.bounds.size.y / 2f + 0.5)) {
			_running = false;
			_creditsSprite.transform.position = _initialCreditsPosition;
			_menuEntries.transform.position = _initialMenuEntriesPosition;
		}
	}

	void Activate() {
		_running = true;
		_menuEntries.transform.Translate(Vector3.up * 10f);
	}
}
