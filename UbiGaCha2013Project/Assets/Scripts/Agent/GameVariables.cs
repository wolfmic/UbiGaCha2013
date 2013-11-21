using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameVariables : MonoBehaviour {
	
	public int maxNbrEnemies = 10;
	int currentNbrEnemies = 10;
	public int ratioLvl0Enemies = 10, ratioLvl1Enemies,ratioLvl2Enemies, ratioLvl3Enemies,ratioLvl4Enemies;
	int toDelete = 0;
	int nbrFloor = 5, ratioLvlEnemies;
	GameObject player;
	GameObject[] enemies, floors;
	public GameObject CrowdAgent, instance;
	
	public Sprite custom1;
	public Sprite custom2;
	public Sprite custom3;
	public Sprite custom4;
	
	
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
		
		switch(player.GetComponent<PlayerControl>().level)
		{
		case 1: ratioLvlEnemies = ratioLvl0Enemies; break;
		case 2: ratioLvlEnemies = ratioLvl1Enemies; break;
		case 3: ratioLvlEnemies = ratioLvl2Enemies; break;
		case 4: ratioLvlEnemies = ratioLvl3Enemies; break;
		case 5: ratioLvlEnemies = ratioLvl4Enemies; break;
		}
		
		if (this.maxNbrEnemies < player.GetComponent<PlayerControl>().level * ratioLvlEnemies)
			AddEnemies();
		
		if (this.maxNbrEnemies > player.GetComponent<PlayerControl>().level * ratioLvlEnemies)
			DeleteEnemies();
		
	}
	
	void DeleteEnemies()
	{
		List<int> toDelete = new List<int>();
		List<int> invisibleFloor = new List<int>();
		Camera mainCam = Camera.main;
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCam);
		
		int nbrDel = maxNbrEnemies - player.GetComponent<PlayerControl>().level * ratioLvlEnemies;
		int nbrPerFloor;
		int countDel;
		
		for(int i = 0; i<enemies.Length; i++)
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
				if ((enemies[j].GetComponent<AgentBehavior>().floor == invisibleFloor[i]))
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
		
		maxNbrEnemies = player.GetComponent<PlayerControl>().level * ratioLvlEnemies;
		GameObject[] pouet = GameObject.FindGameObjectsWithTag("Enemies");
		Debug.Log(pouet.Length);
		
	}
	
	void AddEnemies()
	{
		int nbrAdd = player.GetComponent<PlayerControl>().level * ratioLvlEnemies - maxNbrEnemies;
		floors[player.GetComponent<PlayerControl>().floor].GetComponent<FloorClass>().spawnable = false;
		GameObject.FindGameObjectWithTag("World").GetComponent<AgentSpawn>().Spawn(nbrAdd);
		
		maxNbrEnemies = player.GetComponent<PlayerControl>().level * ratioLvlEnemies;
		
	}
	
	
	
}
