using UnityEngine;
using System.Collections;

public class BellBehaviour : MonoBehaviour {

	public bool LastBell;
	public int Rank;
	private bool _levelClear = false;
    private MusicMaster _musicMaster;

	// Use this for initialization
	void Start () {
        _musicMaster = GameObject.Find("MusicMaster").GetComponent<MusicMaster>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Ring() {
        this.GetComponentInChildren<Animator>().SetTrigger("Ring");
        _musicMaster.PlayDucking(this.audio);
    }
	
	public void SetLevelClear() {
		_levelClear = true;
	}
}
