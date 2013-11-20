using UnityEngine;
using System.Collections;

public class Customization : MonoBehaviour {

    public Sprite cust1;
    int count = 0;
    GameObject world;
	// Use this for initialization
	void Start () {
        world = GameObject.FindGameObjectWithTag("World");
        this.GetComponent<SpriteRenderer>().sprite = world.GetComponent<GameVariables>().custom1;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp("d"))
        {
            if (count == 3)
                count = 0;
            else
                count++;

            switch(count)
            {
                case 0: this.GetComponent<SpriteRenderer>().sprite = world.GetComponent<GameVariables>().custom1; break;
                case 1: this.GetComponent<SpriteRenderer>().sprite = world.GetComponent<GameVariables>().custom2; break;
                case 2: this.GetComponent<SpriteRenderer>().sprite = world.GetComponent<GameVariables>().custom3; break;
                case 3: this.GetComponent<SpriteRenderer>().sprite = world.GetComponent<GameVariables>().custom4; break;
            }
            
        }
	
	}
}
