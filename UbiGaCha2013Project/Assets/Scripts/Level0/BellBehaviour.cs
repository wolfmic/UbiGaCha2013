using UnityEngine;
using System.Collections;

public class BellBehaviour : MonoBehaviour {

	public bool lastBell;
	public int rank;
	public float timerMax = 5;
	public GameObject character;
	private bool interactionPossible = false;
	private bool running = false;
	private bool levelClear = false;
	public float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Interact();
		RunAction ();
	}

	void SetAnimation(bool activation) {
		if (activation)
		{
			gameObject.SendMessage("SetTotalCells",3);
		}
		else
		{
			gameObject.SendMessage("SetTotalCells",1);
		}
	}

	void RunAction() {
		if (running)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				running = false;
				SetAnimation(false);
				character.SendMessage("SetFreeze",false);
			}
		}
	}
	
	void Interact() {
		if ((interactionPossible) && (!levelClear))
		{
			if ((Input.GetKey(KeyCode.E)) && (!running))
			{
				timer = timerMax;
				running = true;
				SetAnimation(true);
				character.SendMessage("SetFreeze",true);
				character.SendMessage("SetRank",rank);
				character.SendMessage("SetLastBell",lastBell);
			}
		}
	}
	
	void SetLevelClear() {
		levelClear = true;
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other == character.collider2D)
		{
			interactionPossible = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other == character.collider2D)
		{
			interactionPossible = false;
		}
	}
}
