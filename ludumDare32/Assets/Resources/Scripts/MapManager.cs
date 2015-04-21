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
	public GameObject shitStarter;
	public bool start = false;
	public bool gameOver = false;
	public int wave = 1;
	public AudioSource intro;
	public AudioSource outro;
	public AudioSource uwatMate;
	public bool mate = false;
	
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

		AudioSource[] audio = GetComponents<AudioSource> ();
		intro = audio [0];
		outro = audio [1];
		uwatMate = audio [2];



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
			intro.Stop ();
			uwatMate.Play ();

			int numEnemies = 0;
			if(wave == 1)
			{
				numEnemies = 11;
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
			if(shitStarter)
			{
				activeEnemies.Add (shitStarter);
				shitStarter = null;
			}
			wave += 1;
			if(wave > 3)
			{
				gameOver = true;
			}
		}
		if(uwatMate.isPlaying == true && start == true)
		{
			
		}	
		else if(uwatMate.isPlaying == false && start == true)
		{

			if(!outro.isPlaying)
				outro.Play ();
		}
	
	}
}
