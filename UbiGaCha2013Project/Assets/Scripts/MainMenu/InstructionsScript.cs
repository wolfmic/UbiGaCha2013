using UnityEngine;
using System.Collections;

public class InstructionsScript : MonoBehaviour {

    private GameObject tiles1, tiles2, tiles3, tiles4, tiles5;
    private GameObject _backgroundNoTitle;
    private GameObject _currentDisplay;
    private int current = 0;
    private bool _running = false;
    private GameObject _menuSelectSoundEmitter;

	// Use this for initialization
	void Start () {
        tiles1 = GameObject.Find("MenuInstru01");
        tiles2 = GameObject.Find("MenuInstru02");
        tiles3 = GameObject.Find("MenuInstru03");
        tiles4 = GameObject.Find("MenuInstru04");
        tiles5 = GameObject.Find("MenuInstru05");
        _backgroundNoTitle = GameObject.Find("BackgroundNoTitle");
        _currentDisplay = new GameObject();

        _currentDisplay.transform.position = _backgroundNoTitle.transform.position;
        _menuSelectSoundEmitter = GameObject.Find("MenuSelectSoundEmitter");
    }
	
	// Update is called once per frame
	void Update () {

        if (_running == false)
            return;

        if(Input.GetKeyUp("right"))
        {
            current++;
            _menuSelectSoundEmitter.audio.Play();
        }

        if(Input.GetKeyUp("left"))
        {
            current--;
            if (current <= 0)
                current = 0;
            _menuSelectSoundEmitter.audio.Play();
        }

        switch(current)
        {
            case 0:
                {
                    tiles1.renderer.sortingOrder = 4;
                    tiles2.renderer.sortingOrder = 3;
                    tiles3.renderer.sortingOrder = 3;
                    tiles4.renderer.sortingOrder = 3;
                    tiles5.renderer.sortingOrder = 3;
                } break;

            case 1:
                {
                    tiles1.renderer.sortingOrder = 3;
                    tiles2.renderer.sortingOrder = 4;
                    tiles3.renderer.sortingOrder = 3;
                    tiles4.renderer.sortingOrder = 3;
                    tiles5.renderer.sortingOrder = 3;
                } break;

            case 2:
                {
                    tiles1.renderer.sortingOrder = 3;
                    tiles2.renderer.sortingOrder = 3;
                    tiles3.renderer.sortingOrder = 4;
                    tiles4.renderer.sortingOrder = 3;
                    tiles5.renderer.sortingOrder = 3;
                } break;

            case 3:
                {
                    tiles1.renderer.sortingOrder = 3;
                    tiles2.renderer.sortingOrder = 3;
                    tiles3.renderer.sortingOrder = 3;
                    tiles4.renderer.sortingOrder = 4;
                    tiles5.renderer.sortingOrder = 3;
                } break;

            case 4:
                {
                    tiles1.renderer.sortingOrder = 3;
                    tiles2.renderer.sortingOrder = 3;
                    tiles3.renderer.sortingOrder = 3;
                    tiles4.renderer.sortingOrder = 3;
                    tiles5.renderer.sortingOrder = 4;
                } break;

            case 5:
                {
                    tiles1.renderer.sortingOrder = 0;
                    tiles2.renderer.sortingOrder = 0;
                    tiles3.renderer.sortingOrder = 0;
                    tiles4.renderer.sortingOrder = 0;
                    tiles5.renderer.sortingOrder = 0;
                    _backgroundNoTitle.renderer.sortingOrder = 0;
					this.current = 0;
                    _running = false;
                } break;
        }

	
	}

    void Activate()
    {
        _running = true;
        _backgroundNoTitle.renderer.sortingOrder = 4;
    }
}
