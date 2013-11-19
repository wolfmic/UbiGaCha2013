using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {

    public float velocity = 12.0f, velocity_max = 12.0f;
    public int level = 5;
    public float range_detection = 0.5f, bell_range = 1.5f;
    public int floor = 0;

    public float startX = 0.0f, startY = 0.0f;
    private Vector3 direction = Vector3.zero;
    public bool onFloor = true;
    private bool onStairs = false;
    private GameObject[] floors;
    private GameObject world;

    private bool freeze = false;
    public bool levelClear = false;
  
	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(this.startX, this.startY, 0.0f);
        this.world = GameObject.FindGameObjectWithTag("World");
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

        if (Input.GetButtonDown("Fire1"))
            UseBellSkill();
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

            if(enemies[i] != null && !IsInRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().isRanged)
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

            Debug.Log(floors.Length);
            for (int i = 0; i < floors.Length; i++ )
            {
                Debug.Log(i + " : ID = " + floors[i].GetInstanceID());
            }

                for (int i = 0; i < floors.Length; i++)
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

    bool IsInBellSkillRange(GameObject enemy)
    {
        if (Math.Abs(enemy.transform.position.x - this.transform.position.x) <= bell_range)
            return true;
        else
            return false;
    }

    void UseBellSkill()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

        for (int i = 0; i< world.GetComponent<GameVariables>().getNbrMax(); i++)
        {
            if (IsInBellSkillRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().floor == this.floor && !enemies[i].GetComponent<AgentBehavior>().jump)
            {
                float coeff = 15 - Math.Abs(enemies[i].transform.position.x - this.transform.position.x);
                enemies[i].GetComponent<AgentBehavior>().f_speed_jump += (1.5f * coeff)/10.0f;
                enemies[i].GetComponent<AgentBehavior>().setJump();
            }
        }
    }

    void SetFreeze(bool activation)
    {
        freeze = activation;
    }

    void SetLastBell(bool b)
    {
        if (b)
        {
            levelClear = true;
            GameObject[] bells = GameObject.FindGameObjectsWithTag("Bell");
            foreach (GameObject bell in bells)
            {
                bell.SendMessage("SetLevelClear");
            }
        }
    }


    void SetRank(int i)
    {
        if (level == i)
        {
            level++;
        }
        else
        {
            level = 0;
        }
    }
}

