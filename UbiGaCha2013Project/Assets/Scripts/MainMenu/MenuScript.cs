using UnityEngine;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {

	private GameObject _cursor;
	private List<GameObject> _menuEntries;
	private int _menuIndex;

	public bool Enabled = true;

	// Use this for initialization
	void Start () {
		_menuIndex = 0;
		_cursor = GameObject.Find ("Cursor");
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
			_menuIndex = System.Math.Min(_menuEntries.Count - 1, _menuIndex + 1);
		}
		if (Input.GetKeyDown ("up")) {
			updateCursor = true;
			_menuIndex = System.Math.Max(0, _menuIndex - 1);
		}
		if (Input.GetKeyDown("space")) {
			MonoBehaviour obj;

			obj = _menuEntries[_menuIndex].GetComponent<CreditsScript>();
			if (obj != null) {
				obj.SendMessage("Activate");
			}
		}
		// move cursor if needed
		if (updateCursor) {
			this.UpdateCursor();
		}
	}

	void UpdateCursor() {
		_cursor.transform.position = _menuEntries[_menuIndex].transform.position;
	}
}
