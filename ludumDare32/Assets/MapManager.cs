using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour 
{
	public int maxItems = 20;

	public List<int> itemIds;
	public List<Transform> itemTransforms;
	public GameObject[] itemLocations;
	public GameObject[] enemies;
	// Use this for initialization
	void Start () 
	{
		itemIds = new List<int> ();
		for(int i=0;i<maxItems;i++)
		{
			itemIds.Add (i);
		}

		itemTransforms = new List<Transform> ();
		itemLocations = GameObject.FindGameObjectsWithTag ("itemLoc");
		for(int i=0;i<itemLocations.Length;i++)
		{
			itemTransforms.Add (itemLocations[i].transform);
		}

		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
