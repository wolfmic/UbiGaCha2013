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
        Debug.Log("Start spawn");
        gameVariables = GameObject.Find("World");
        floors = GameObject.FindGameObjectsWithTag("Floor");
        numberOfObjects = gameVariables.GetComponent<GameVariables>().getNbrMax();
        nbrOfFloor = gameVariables.GetComponent<GameVariables>().getNbrFloor();

        for (int i = 0; i < gameVariables.GetComponent<GameVariables>().getNbrFloor(); i++)
        {
            if (floors[i].GetComponent<FloorClass>().spawnable == false)
                nbrOfFloor--;
        }

        for (int i = 0; i < gameVariables.GetComponent<GameVariables>().getNbrFloor(); i++)
        {
            if (floors[i].GetComponent<FloorClass>().spawnable == false)
                continue;

            this.xMax = floors[i].renderer.bounds.max.x;
            this.xMin = floors[i].renderer.bounds.min.x;

            for (int j = 0; j < numberOfObjects / nbrOfFloor; ++j)
             {
                    Vector3 pos = new Vector3(UnityEngine.Random.Range(this.xMin, this.xMax), floors[i].renderer.bounds.max.y + 0.110f, 0.0f);
                    instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<AgentBehavior>().transform.Translate(Vector3.up * instance.renderer.bounds.extents.y);
                    instance.GetComponent<AgentBehavior>().floor = floors[i].GetComponent<FloorClass>().id;
                    instance.GetComponent<AgentBehavior>().xMin = this.xMin;
                    instance.GetComponent<AgentBehavior>().xMax = this.xMax;
                    instance.GetComponent<AgentBehavior>().yGround = floors[i].renderer.bounds.max.y + instance.renderer.bounds.extents.y + 0.110f;

             }

            this.xMin = this.xMax = 0.0f;
            
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn(int nbr)
    {
        gameVariables = GameObject.Find("World");
        floors = GameObject.FindGameObjectsWithTag("Floor");
        nbrOfFloor = gameVariables.GetComponent<GameVariables>().getNbrFloor();

        for (int i = 0; i < gameVariables.GetComponent<GameVariables>().getNbrFloor(); i++ )
        {
            if (floors[i].GetComponent<FloorClass>().spawnable == false)
                nbrOfFloor--;
        }

            for (int i = 0; i < gameVariables.GetComponent<GameVariables>().getNbrFloor(); i++)
            {
                if (floors[i].GetComponent<FloorClass>().spawnable == false)
                    continue;
                this.xMax = floors[i].renderer.bounds.max.x;
                this.xMin = floors[i].renderer.bounds.min.x;

                for (int j = 0; j < nbr / nbrOfFloor; ++j)
                {
                    Vector3 pos = new Vector3(UnityEngine.Random.Range(this.xMin, this.xMax), floors[i].renderer.bounds.max.y + 0.110f, 0.0f);
                    instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<AgentBehavior>().transform.Translate(Vector3.up * instance.renderer.bounds.extents.y);
                    instance.GetComponent<AgentBehavior>().floor = floors[i].GetComponent<FloorClass>().id;
                    instance.GetComponent<AgentBehavior>().xMin = this.xMin;
                    instance.GetComponent<AgentBehavior>().xMax = this.xMax;
                    instance.GetComponent<AgentBehavior>().yGround = floors[i].renderer.bounds.max.y + instance.renderer.bounds.extents.y + 0.110f;

                }

                this.xMin = this.xMax = 0.0f;
            }
    }

  
}
