using UnityEngine;
using System;
using System.Collections.Generic;

public class AgentSpawn : MonoBehaviour {

	private static class FloorArea
	{

		public static Rect FromCollider2D(Collider2D collider)
		{
			PolygonCollider2D polygonCollider;
			BoxCollider2D boxCollider;

			polygonCollider = collider as PolygonCollider2D;
			if (polygonCollider != null)
				return FromPolygonCollider2D(polygonCollider);
			boxCollider = collider as BoxCollider2D;
			if (boxCollider != null)
				return FromBoxCollider2D(boxCollider);
			throw new ArgumentException("Invalid collider to build a rect");
		}

		public static Rect FromPolygonCollider2D(PolygonCollider2D polygonCollider)
		{
			Rect area = new Rect();

			area.xMin = polygonCollider.points[0].x;
			area.yMin = polygonCollider.points[0].y;
			area.xMax = area.x;
			area.yMax = area.y;
			foreach (var point in polygonCollider.points) {
				area.xMin = Mathf.Min (area.xMin, point.x);
				area.xMax = Mathf.Max (area.xMax, point.x);
				area.yMin = Mathf.Min (area.yMin, point.y);
				area.yMax = Mathf.Max (area.yMax, point.y);
			}
			return area;
		}

		public static Rect FromBoxCollider2D(BoxCollider2D boxCollider)
		{
			return new Rect(
				boxCollider.center.x - boxCollider.size.x / 2f + boxCollider.transform.position.x,
				boxCollider.center.y - boxCollider.size.y / 2f + boxCollider.transform.position.y,
				boxCollider.size.x,
				boxCollider.size.y
				);
		}
	}

	private struct Floor
	{
		public readonly GameObject Object;
		public readonly Rect Area;

		public Floor(GameObject obj, Rect area)
		{
			this.Object = obj;
			this.Area = area;
		}
	}

	public GameObject CrowdAgent;
	public GameObject gameVariables;
	List<Floor> spawnableFloors;
    int numberOfObjects;


	// Use this for initialization
    void Start()
    {
		GameObject[] floors;

        Debug.Log("Start spawn");
        this.gameVariables = GameObject.Find("World");
        floors = GameObject.FindGameObjectsWithTag("Floor");
        this.numberOfObjects = gameVariables.GetComponent<GameVariables>().getNbrMax();
		this.spawnableFloors = new List<Floor>();

		Debug.Log(floors.Length + " nb of floors");
        for (int i = 0; i < floors.Length; i++)
        {
            if (floors[i].GetComponent<FloorClass>().spawnable == true)
			{
				this.spawnableFloors.Add(new Floor(floors[i], FloorArea.FromCollider2D(floors[i].collider2D)));
			}
        }
		Debug.Log(spawnableFloors.Count + " nb of spawnable floors");

		this.Spawn(this.numberOfObjects);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn(int nbr)
    {
		Debug.Log("Spawn(" + nbr + ") will result in " + (this.spawnableFloors.Count * (nbr / this.spawnableFloors.Count)) + " entities spawned");
		// instantie les entites demandees a travers tous les floors spawnable
		foreach (var floor in this.spawnableFloors) {
			float xMax = floor.Area.xMax;
			float xMin = floor.Area.xMin;

			Debug.Log("Entity spawn span : x [" + xMin + ", " + xMax + "], y = " + floor.Area.yMin);
			for (int j = 0; j < nbr / this.spawnableFloors.Count; ++j) {
				Vector3 pos = new Vector3(UnityEngine.Random.Range(xMin, xMax), floor.Area.yMin + 0.110f, 0.0f);
				GameObject instance = Instantiate(CrowdAgent, pos, Quaternion.identity) as GameObject;
				instance.GetComponent<AgentBehavior>().transform.Translate(Vector3.up * instance.renderer.bounds.extents.y);
				instance.GetComponent<AgentBehavior>().floor = floor.Object.GetComponent<FloorClass>().id;
				instance.GetComponent<AgentBehavior>().xMin = xMin;
				instance.GetComponent<AgentBehavior>().xMax = xMax;
				instance.GetComponent<AgentBehavior>().yGround = floor.Area.yMin + instance.renderer.bounds.extents.y + 0.110f;
			}
		}
    }

  
}
