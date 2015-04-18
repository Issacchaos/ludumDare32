﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour 
{

	public int health;
	public int maxHealth;
	public int moveSpeed;
	public int maxWeight = 0;
	public int curWeight = 0;
	public int level = 1;
	public int exp = 0;
	public int maxExp;

	public bool hasItem = false;
	public bool dead = false;

	public GameObject item;
	public List<GameObject> gameObjects;


	// Use this for initialization
	void Start () 
	{
		gameObjects = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void addItem(GameObject c)
	{
		gameObjects.Add (c);
	}

	void removeItem(GameObject c)
	{
		gameObjects.Remove(c);
	}

	void takeDamage(int dmg)
	{
		if((health - dmg) <= 0)
		{
			dead = true;
		}
		health -= dmg;
	}

	void addExp(int val)
	{
		if((exp + val) >= maxExp)
		{
			level += 1;
		}
	}

	public void pick_up()
	{
		float tmpDist = 0.0f;
		GameObject closest = null;
		for(int i=0;i<gameObjects.Count;i++)
		{
			ObjectBase scr = gameObjects[i].GetComponent<ObjectBase>();
			if(scr.weight > maxWeight)
			{
				continue;
			}
			else
			{
				float tmp = Vector3.Distance(transform.position,gameObjects[i].transform.position);
				if(tmp < tmpDist || tmpDist == 0.0f)
				{
					closest = gameObjects[i];
					tmpDist = tmp;
				}
			}
		}
		item = closest;
		hasItem = true;
		item.SendMessage ("Picked_up", transform.position);
	}

}
