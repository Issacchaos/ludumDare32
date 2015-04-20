using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour 
{
	public int maxItems = 20;
	public List<int> itemIds;
	public List<Transform> itemTransforms;
	public GameObject[] itemLocations;
	public GameObject[] enemies;
	public List<GameObject> activeEnemies;
	public bool start = false;
	public bool gameOver = false;
	public int wave = 3;
	
	// Use this for initialization
	void Start () 
	{
		itemIds = new List<int> ();
		for(int i=0;i<maxItems;i++)
		{
			itemIds.Add (i);
		}

		activeEnemies = new List<GameObject> ();

//		itemTransforms = new List<Transform> ();
//		itemLocations = GameObject.FindGameObjectsWithTag ("itemLoc");
//		for(int i=0;i<itemLocations.Length;i++)
//		{
//			itemTransforms.Add (itemLocations[i].transform);
//		}
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(start && activeEnemies.Count == 0)
		{
			for(int i=0;i< enemies.Length/(2^wave);i++)
			{
				int tmp = UnityEngine.Random.Range(0,enemies.Length);
				enemies[tmp].GetComponent<Enemy>().active = true;
				activeEnemies.Add (enemies[tmp]);
			}
			wave -= 1;
			if(wave < 0)
			{
				gameOver = true;
			}
		}
	
	}
}
