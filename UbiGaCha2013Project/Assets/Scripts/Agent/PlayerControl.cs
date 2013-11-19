using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {

    public float velocity = 12.0f, velocity_max = 12.0f;
    public int level = 5;
    int range_detection = 5, bell_range = 15;
    public int floor = 1;

    private Vector3 direction = Vector3.zero;
    public bool onFloor = true;
    private bool onStairs = false;
    private GameObject[] floors;

    private bool freeze = false;
    public bool levelClear = false;
    private int rank = 0;
	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(1.119886f , 0.08295452f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

        UpdateSpeed();


        if (onFloor == true)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * -Vector3.left * velocity * Time.deltaTime);
        }

        if (onStairs == true)
        {

            this.transform.Translate(Input.GetAxis("Vertical") * direction * velocity / 126 * Time.deltaTime);
        }


        if (Input.GetKeyUp("up"))
            this.level++;

        if (Input.GetKeyUp("down"))
            this.level--;

        if (Input.GetKeyUp("space"))
            UseBell();
	}

    void UpdateSpeed()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        
        for(int i=0; i < enemies.Length; i++)
        {
            if(IsInRange(enemies[i]) && !enemies[i].GetComponent<AgentBehavior>().isRanged && enemies[i].GetComponent<AgentBehavior>().floor == this.floor)
            {
                enemies[i].GetComponent<AgentBehavior>().setIsRange();
                this.velocity -= this.velocity * enemies[i].GetComponent<AgentBehavior>().coeff_reduc;
                if (this.velocity < 0)
                    this.velocity = 0;
            }

            if(!IsInRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().isRanged)
            {
                enemies[i].GetComponent<AgentBehavior>().setIsRange();
                this.velocity += this.velocity_max * enemies[i].GetComponent<AgentBehavior>().coeff_reduc;
                if (this.velocity > this.velocity_max)
                    this.velocity = this.velocity_max;
            }
        }
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stairs")
        {
            onStairs = true;
        }
        if (other.tag == "Floor")
        {
            onFloor = true;
            floors = GameObject.FindGameObjectsWithTag("Floor");

            for(int i= 0; i< floors.Length; i++)
            {
                Debug.Log(floors[i].rigidbody2D.GetInstanceID() == other.rigidbody2D.GetInstanceID());
                if (floors[i].rigidbody2D.GetInstanceID() == other.rigidbody2D.GetInstanceID())
                    floor = i;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stairs")
        {
            onStairs = false;
        }
        if (other.tag == "Floor")
        {
            onFloor = false;
            floor = -1;
        }
    }

    bool IsInRange(GameObject enemy)
    {
        if (Math.Abs(enemy.transform.position.x - this.transform.position.x) <= range_detection)
            return true;
        else
            return false;
    }

    bool IsInBellRange(GameObject enemy)
    {
        if (Math.Abs(enemy.transform.position.x - this.transform.position.x) <= bell_range)
            return true;
        else
            return false;
    }

    void UseBell()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        
        for(int i = 0; i < enemies.Length; i++)
        {
            if (IsInBellRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().floor == this.floor && !enemies[i].GetComponent<AgentBehavior>().jump)
            {
                float coeff = 15 - Math.Abs(enemies[i].transform.position.x - this.transform.position.x);
                enemies[i].GetComponent<AgentBehavior>().f_speed_jump += 1.5f * coeff;
                enemies[i].GetComponent<AgentBehavior>().setJump();
            }
        }
    }
}

