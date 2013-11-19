using UnityEngine;
using System.Collections;

public class StairsToScript : MonoBehaviour {

    public Vector3 dir;
    public GameObject floorUp, floorDown;


    void Start() {



    }

    void Update() {

    }


    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            Debug.Log("SET DIRECTION");
            other.GetComponent<PlayerControl>().setDirection(dir);
        }

        if(other.tag == "Enemies")
        {
            other.GetComponent<AgentBehavior>().setDirection(dir);
        }

        
    
    }



}