using UnityEngine;
using System;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	private const float SCROLL_SPEED = 0.5f;

	private bool _running = false;
	private GameObject _creditsSprite;
	private GameObject _backgroundNoTitle;
	private Vector3 _initialCreditsPosition;

	// Use this for initialization
	void Start () {
		_creditsSprite = GameObject.Find ("Credits");
		_backgroundNoTitle = GameObject.Find("BackgroundNoTitle");

		_initialCreditsPosition = _creditsSprite.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (_running == false)
			return;
		_creditsSprite.transform.Translate(Vector2.up * SCROLL_SPEED * Time.deltaTime);
		if (_creditsSprite.transform.position.y > (_creditsSprite.renderer.bounds.size.y / 2f + 1)) {
			_backgroundNoTitle.renderer.sortingOrder = 0;
			_running = false;
			_creditsSprite.transform.position = _initialCreditsPosition;
		}
	}

	void Activate() {
		_running = true;
		_backgroundNoTitle.renderer.sortingOrder = 4;
	}
}
