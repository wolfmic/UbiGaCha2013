using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameVariables : MonoBehaviour {

   public int maxNbrEnemies = 10;
   int currentNbrEnemies = 50;
   int toDelete = 0;
   int nbrFloor = 5;
   GameObject player;
   GameObject[] enemies, floors;
   public GameObject CrowdAgent, instance;
   

   public int getNbrMax() { return maxNbrEnemies; }
   public int getNbrFloor() { return floors.Length; }

    void Start()
   {
       floors = GameObject.FindGameObjectsWithTag("Floor");
   }
  /* void Update()
   {
       player = GameObject.FindGameObjectWithTag("Player");
       enemies = GameObject.FindGameObjectsWithTag("Enemies");

       if (this.maxNbrEnemies < player.GetComponent<PlayerControl>().level * 10)
           AddEnemies();

       if (this.maxNbrEnemies > player.GetComponent<PlayerControl>().level * 10)
           DeleteEnemies();

   }*/

    void DeleteEnemies()
   {
       List<int> toDelete = new List<int>();
       List<int> invisibleFloor = new List<int>();
       Camera mainCam = Camera.main;
       Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCam);

       int nbrDel =  maxNbrEnemies - player.GetComponent<PlayerControl>().level * 10;
       int nbrPerFloor;
        int countDel;
 
        for(int i = 0; i<maxNbrEnemies; i++)
        {
            if (!GeometryUtility.TestPlanesAABB(planes, enemies[i].collider.bounds) && !invisibleFloor.Contains((int)enemies[i].GetComponent<AgentBehavior>().floor))
                invisibleFloor.Add((int)enemies[i].GetComponent<AgentBehavior>().floor);
        }

        Debug.Log(invisibleFloor.Count);
        nbrPerFloor = nbrDel/invisibleFloor.Count;

        for (int i = 0; i < invisibleFloor.Count; i++ )
        {
            countDel = 0;

            for (int j = 0; j < maxNbrEnemies; j++)
            {
                if ((int)enemies[j].GetComponent<AgentBehavior>().floor == invisibleFloor[i])
                {
                    toDelete.Add(j);
                    countDel++;
                }

                if (countDel >= nbrPerFloor)
                    j = maxNbrEnemies + 1;
            }
        }

        for (int i = 0; i < toDelete.Count; i++)
        {
            Destroy(enemies[toDelete[i]]);
        }

        maxNbrEnemies = player.GetComponent<PlayerControl>().level * 10;
        GameObject[] pouet = GameObject.FindGameObjectsWithTag("Enemies");
        Debug.Log(pouet.Length);

   }

    void AddEnemies()
    {
        int nbrAdd = player.GetComponent<PlayerControl>().level * 10 - maxNbrEnemies;
        int nbrPerFloor = nbrAdd / (nbrFloor - 1);
        int actualFloor = player.GetComponent<PlayerControl>().floor;

        for (int j = 0; j < nbrFloor; j++)
        {
            if(j != actualFloor)
            { 
                for (int i = 0; i < nbrPerFloor; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(1.0f, 47.5f), 5.0f + (j * 9.0f), 0.0f);
                    instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<AgentBehavior>().floor = j;
                }
            }
        }

        if(nbrAdd % (nbrFloor -1) != 0)
        {
            for(int i = 0; i < nbrAdd % (nbrFloor -1); i++ )
            {
                Vector3 pos = new Vector3(Random.Range(1.0f, 47.5f), 5.0f + ((nbrFloor-1) * 9.0f), 0.0f);
                instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
                instance.GetComponent<AgentBehavior>().floor = nbrFloor - 1;
            }
        }
        maxNbrEnemies = player.GetComponent<PlayerControl>().level * 10;
        GameObject[] pouet = GameObject.FindGameObjectsWithTag("Enemies");
        Debug.Log(pouet.Length);
  
    }

   

}
