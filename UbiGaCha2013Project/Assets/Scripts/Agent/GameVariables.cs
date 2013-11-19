using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameVariables : MonoBehaviour {

   public int maxNbrEnemies = 10;
   int currentNbrEnemies = 10;
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
  
    void Update()
   {
       player = GameObject.FindGameObjectWithTag("Player");
       enemies = GameObject.FindGameObjectsWithTag("Enemies");

       if (this.maxNbrEnemies < player.GetComponent<PlayerControl>().level * 10)
           AddEnemies();

       if (this.maxNbrEnemies > player.GetComponent<PlayerControl>().level * 10)
           DeleteEnemies();

   }

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
            if (!GeometryUtility.TestPlanesAABB(planes, enemies[i].renderer.bounds) && !invisibleFloor.Contains((int)enemies[i].GetComponent<AgentBehavior>().floor))
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
        floors[player.GetComponent<PlayerControl>().floor].GetComponent<FloorClass>().spawnable = false;
        GameObject.FindGameObjectWithTag("World").GetComponent<AgentSpawn>().Spawn(nbrAdd);
       
        maxNbrEnemies = player.GetComponent<PlayerControl>().level * 10;
       
    }

   

}
