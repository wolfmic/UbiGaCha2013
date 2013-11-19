using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {

    public float f_speed = 12.0f, f_speedMax = 12.0f;
    public int level = 5;
    int range_detection = 5, bell_range = 15;
    public int floor = 1;

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(this.transform.position.x, 5.0f + (floor * 9.0f), this.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

        UpdateSpeed();

	    if(Input.GetKey("right"))
        {
            if (this.transform.position.x < 47.5f)
                this.transform.Translate(Vector3.right * Time.deltaTime * f_speed);
              
        }

        if (Input.GetKey("left"))
        {
            if (this.transform.position.x > 0.0f)
                this.transform.Translate(Vector3.left * Time.deltaTime * f_speed);
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
                this.f_speed -= this.f_speed * enemies[i].GetComponent<AgentBehavior>().coeff_reduc;
                if (this.f_speed < 0)
                    this.f_speed = 0;
            }

            if(!IsInRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().isRanged)
            {
                enemies[i].GetComponent<AgentBehavior>().setIsRange();
                this.f_speed += this.f_speedMax * enemies[i].GetComponent<AgentBehavior>().coeff_reduc;
                if (this.f_speed > this.f_speedMax)
                    this.f_speed = this.f_speedMax;
            }
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

