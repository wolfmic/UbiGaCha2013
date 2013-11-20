using UnityEngine;
using System.Collections;

public class openDoorScript : MonoBehaviour {

    public float timer = 0.0f;
    private float curTimer = 0.0f;

    private bool open = false;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
     
        if (timer > curTimer && open == true) {
            curTimer += Time.deltaTime;
        }

        if (curTimer > timer) {
            
            timer = 0.0f;
            curTimer = 0.0f;
            open = false;

            GameObject.Find("MainCamera").transform.position = new Vector3(0.6f, -0.6f, -10f);




        }
	}

    void openDoor() {
        timer = 2;
        open = true;

    }

}
