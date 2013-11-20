using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {

    public float velocity = 0.5f;

    private Vector3 direction = Vector3.zero;
    private bool onFloor = true;
    private bool onStairs = false;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (onFloor == true) {
            this.transform.Translate(Input.GetAxis("Horizontal") * -Vector3.left * velocity * Time.deltaTime);
        }

        if (onStairs == true) {
            
            this.transform.Translate(Input.GetAxis("Vertical") * direction * velocity / 126 * Time.deltaTime);
        }


    }

    public void setDirection(Vector3 dir) {
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Stairs") {
            onStairs = true;
        }
        if (other.tag == "Floor") {
            onFloor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Stairs") {
            onStairs = false;
        }
        if (other.tag == "Floor") {
            onFloor = false;
        }
    }



}
