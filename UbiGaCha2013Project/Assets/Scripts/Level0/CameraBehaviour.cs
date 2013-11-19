using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public GameObject character;

	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.position);
		Vector3 targetPosition = character.transform.TransformPoint (new Vector3 (0, -10, 0));
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition,ref velocity, smoothTime);


		//transform.position = character.transform.TransformPoint (new Vector3 (0, 0, -10));
	}
}
