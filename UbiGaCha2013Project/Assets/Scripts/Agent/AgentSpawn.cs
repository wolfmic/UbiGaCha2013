using UnityEngine;
using System.Collections;
using System;

public class AgentSpawn : MonoBehaviour {

    float xMin = 0.0f,
           xMax = 0.0f;
    
    public GameObject CrowdAgent,   gameVariables, instance;
    GameObject[] floors, marks;
    int numberOfObjects, nbrOfFloor;


	// Use this for initialization
    void Start()
    {
        gameVariables = GameObject.Find("World");
        floors = GameObject.FindGameObjectsWithTag("Floor");
        marks = GameObject.FindGameObjectsWithTag("Mark");
        numberOfObjects = gameVariables.GetComponent<GameVariables>().getNbrMax();
        nbrOfFloor = gameVariables.GetComponent<GameVariables>().getNbrFloor();
        
        for (int i = 0; i < nbrOfFloor; i++)
        {
            for (int j = 0; j < marks.Length;j++ )
            {
                Debug.Log("Mark[" + j + "].y = " + marks[j].transform.position.y + " Floor[" + i + "].y" + floors[i].transform.position.y);
                Debug.Log(Math.Abs(marks[j].transform.position.y - floors[i].transform.position.y) <= 0.001f);

                if(marks[j].transform.position.y == floors[i].transform.position.y ||
                   Math.Abs(marks[j].transform.position.y - floors[i].transform.position.y) <= 0.001f)
                {
                    if (this.xMin == 0.0f)
                        this.xMin = marks[j].transform.position.x;
                    else
                        this.xMax = marks[j].transform.position.x;
                }

                if(this.xMin > this.xMax)
                {
                    float temp = this.xMin;
                    this.xMin = this.xMax;
                    this.xMax = temp;
                }

                Debug.Log("Min = " + this.xMin + ", Max = " + this.xMax);
                

            }

            for (int j = 0; j < numberOfObjects / nbrOfFloor; ++j)
             {
                    Vector3 pos = new Vector3(UnityEngine.Random.Range(this.xMin, this.xMax), floors[i].transform.position.y, 0.0f);
                    instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<AgentBehavior>().floor = i;
                    instance.GetComponent<AgentBehavior>().xMin = this.xMin;
                    instance.GetComponent<AgentBehavior>().xMax = this.xMax;

             }

            this.xMin = this.xMax = 0.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

  
}
