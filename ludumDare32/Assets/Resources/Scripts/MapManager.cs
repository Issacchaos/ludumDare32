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
	public List<GameObject> nonactiveEnemies;
	public bool start = false;
	public bool gameOver = false;
	public int wave = 1;
	
	// Use this for initialization
	void Start () 
	{
		itemIds = new List<int> ();
		for(int i=0;i<maxItems;i++)
		{
			itemIds.Add (i);
		}
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		activeEnemies = new List<GameObject> ();
		nonactiveEnemies = new List<GameObject> ();
		foreach(GameObject g in enemies)
		{
			nonactiveEnemies.Add (g);
		}

//		itemTransforms = new List<Transform> ();
//		itemLocations = GameObject.FindGameObjectsWithTag ("itemLoc");
//		for(int i=0;i<itemLocations.Length;i++)
//		{
//			itemTransforms.Add (itemLocations[i].transform);
//		}


	}
	
	// Update is called once per frame
	void Update () 
	{
		if(start && activeEnemies.Count == 0 && !gameOver)
		{
			int numEnemies = 0;
			if(wave == 1)
			{
				numEnemies = 12;
//				foreach(GameObject g in nonactiveEnemies)
//				{
//					g.GetComponent<Enemy>().active = true;
//				}
			}
			if(wave == 2)
			{
				numEnemies = 10;
				//				foreach(GameObject g in nonactiveEnemies)
				//				{
				//					g.GetComponent<Enemy>().active = true;
				//				}
			}
			if(wave == 3)
			{
				numEnemies = 6;
				//				foreach(GameObject g in nonactiveEnemies)
				//				{
				//					g.GetComponent<Enemy>().active = true;
				//				}
			}
			for(int i=0;i< numEnemies;i++)
			{
				int tmp = UnityEngine.Random.Range(0,nonactiveEnemies.Count);
				if(nonactiveEnemies[tmp].GetComponent<Enemy>().active)
				{
					foreach(GameObject g in nonactiveEnemies)
					{
						if( g.GetComponent<Enemy>().active == false)
						{
							g.GetComponent<Enemy>().active = true;
							activeEnemies.Add (g);
							break;
						}
					}
				}
				else
				{
					nonactiveEnemies[tmp].GetComponent<Enemy>().active = true;
					activeEnemies.Add (nonactiveEnemies[tmp]);
				}
				Enemy scr = nonactiveEnemies[tmp].GetComponent<Enemy>();
				scr.level = wave;
				scr.maxHealth = scr.levelSys[scr.level][2];
				scr.health = scr.maxHealth;
				scr.maxWeight = scr.levelSys[scr.level][0];
				scr.maxExp = scr.levelSys[scr.level][1];
			}
			wave += 1;
			if(wave > 3)
			{
				gameOver = true;
			}
		}
	
	}
}
