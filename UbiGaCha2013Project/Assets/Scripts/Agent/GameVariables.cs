using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameVariables : MonoBehaviour {
	
	public int maxNbrEnemies = 10;
	public int[] ratiosLvlEnemies;
	GameObject player;
	GameObject[] enemies, floors;

	public Sprite custom1;
	public Sprite custom2;
	public Sprite custom3;
	public Sprite custom4;
	
	
	public int getNbrMax() { return maxNbrEnemies; }
	public int getNbrFloor() { return floors.Length; }
	
	void Start()
	{
		floors = GameObject.FindGameObjectsWithTag("Floor");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update()
	{
		int ratioLvlEnemies;

		enemies = GameObject.FindGameObjectsWithTag("Enemies");
		ratioLvlEnemies = this.ratiosLvlEnemies[this.player.GetComponent<PlayerControl>().level - 1];

		if (this.maxNbrEnemies < this.player.GetComponent<PlayerControl>().level * ratioLvlEnemies)
			AddEnemies(ratioLvlEnemies);
		
		if (this.maxNbrEnemies > this.player.GetComponent<PlayerControl>().level * ratioLvlEnemies)
			DeleteEnemies(ratioLvlEnemies);
	}

	void DeleteEnemies(int ratioLvlEnemies)
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
		
		Debug.Log("NbInvisible floors : " + invisibleFloor.Count);
		nbrPerFloor = nbrDel/invisibleFloor.Count;
		
		for (int i = 0; i < invisibleFloor.Count; i++ )
		{
			countDel = 0;
			
			for (int j = 0; j < maxNbrEnemies && countDel < nbrPerFloor; j++)
			{
				if ((enemies[j].GetComponent<AgentBehavior>().floor == invisibleFloor[i]))
				{
					toDelete.Add(j);
					countDel++;
				}
			}
		}
		
		for (int i = 0; i < toDelete.Count; i++)
		{
			Destroy(enemies[toDelete[i]]);
		}
		
		maxNbrEnemies = player.GetComponent<PlayerControl>().level * ratioLvlEnemies;
		GameObject[] pouet = GameObject.FindGameObjectsWithTag("Enemies");
		Debug.Log("NbEnemies after delete : " + pouet.Length);
		
	}
	
	void AddEnemies(int ratioLvlEnemies)
	{
		bool playerFloorState;
		int nbrAdd;
		FloorClass playerFloor;

		Debug.Log("AddEnemies()");
		// compute nbr entities to spawn
		nbrAdd = player.GetComponent<PlayerControl>().level * ratioLvlEnemies - maxNbrEnemies;
		// save floor state
		playerFloor = floors[player.GetComponent<PlayerControl>().floor].GetComponent<FloorClass>();
		playerFloorState = playerFloor.spawnable;
		// disable spawn on player's floor
		 playerFloor.spawnable = false;
		// spawn entities
		GameObject.FindGameObjectWithTag("World").GetComponent<AgentSpawn>().Spawn(nbrAdd);
		// restore player's floor state
		playerFloor.spawnable = playerFloorState;
		
		this.maxNbrEnemies = this.player.GetComponent<PlayerControl>().level * ratioLvlEnemies;
	}
	
	
	
}
