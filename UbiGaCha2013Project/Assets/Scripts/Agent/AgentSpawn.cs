using UnityEngine;
using System.Collections;

public class AgentSpawn : MonoBehaviour {

    float xMin = 1.0f,
           xMax = 47.5f;
    
    public GameObject CrowdAgent,   gameVariables, instance;
    int numberOfObjects, nbrOfFloor;


	// Use this for initialization
    void Start()
    {
        gameVariables = GameObject.Find("World");
        numberOfObjects = gameVariables.GetComponent<GameVariables>().getNbrMax();
        nbrOfFloor = gameVariables.GetComponent<GameVariables>().nbrFloor;

        for (int i = 0; i < nbrOfFloor; i++)
        {
            for (int j = 0; j < numberOfObjects / nbrOfFloor; ++j)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), 5.0f + (i * 9.0f), 0.0f);
                instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
                instance.GetComponent<AgentBehavior>().floor = i;

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

  
}
