﻿using UnityEngine;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {

	private GameObject _cursor;
	private GameObject _menuSelectSoundEmitter;
	private GameObject _menuValidateSoundEmitter;
	private List<GameObject> _menuEntries;
	private int _menuIndex;

	public bool Enabled = true;

	// Use this for initialization
	void Start () {
		_menuIndex = 0;
		_cursor = GameObject.Find ("Cursor");
		_menuSelectSoundEmitter = GameObject.Find("MenuSelectSoundEmitter");
		_menuValidateSoundEmitter = GameObject.Find("MenuValidateSoundEmitter");
		_menuEntries = new List<GameObject>(GameObject.FindGameObjectsWithTag("MenuEntry"));

		_menuEntries.Sort((a, b) => (int)(b.transform.position.y >= a.transform.position.y ? (b.transform.position.y == a.transform.position.y ? 0 : 1) : -1));
		this.UpdateCursor ();
	}
	
	// Update is called once per frame
	void Update () {
		bool updateCursor = false;

		// be sure we're enabled
		if (this.Enabled == false) {
				return;
		}
		// process input
		if (Input.GetKeyDown ("down")) {
			updateCursor = true;
			_menuEntries[_menuIndex].renderer.material.color = Color.white;
			_menuIndex = System.Math.Min(_menuEntries.Count - 1, _menuIndex + 1);
		}
		if (Input.GetKeyDown ("up")) {
			updateCursor = true;
			_menuEntries[_menuIndex].renderer.material.color = Color.white;
			_menuIndex = System.Math.Max(0, _menuIndex - 1);
		}
		if (Input.GetKeyDown("space")) {
			MonoBehaviour obj;

			obj = _menuEntries[_menuIndex].GetComponent<CreditsScript>();
			if (obj == null)
				obj = _menuEntries[_menuIndex].GetComponent<ExitScript>();
			if (obj == null)
				obj = _menuEntries[_menuIndex].GetComponent<StartScript>();
            if (obj == null)
                obj = _menuEntries[_menuIndex].GetComponent<InstructionsScript>();
			if (obj != null) {
				_menuValidateSoundEmitter.audio.Play();
				obj.SendMessage("Activate");
			}
		}
		// move cursor if needed
		if (updateCursor) {
			_menuSelectSoundEmitter.audio.Play();
			this.UpdateCursor();
		}
	}

	void UpdateCursor() {
		_menuEntries[_menuIndex].renderer.material.color = Color.yellow;
		_cursor.transform.position = _menuEntries[_menuIndex].transform.position;
	}
}
