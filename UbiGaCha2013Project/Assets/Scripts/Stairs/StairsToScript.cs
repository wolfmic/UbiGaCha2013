using UnityEngine;
using System.Collections;

public class StairsToScript : MonoBehaviour {

    public Vector3 dir;



    void Start() {



    }

    void Update() {

    }


    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            Debug.Log("SET DIRECTION");
            other.GetComponent<Movements>().setDirection(dir);
        }

        
    
    }



}