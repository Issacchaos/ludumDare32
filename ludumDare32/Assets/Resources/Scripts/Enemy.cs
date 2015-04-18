using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : CharacterBase
{
	public int weight = 0;
	public float damage = 0f;
	public float throwRange = 0f;
	public float pickUpRange = 0f;

	public int baseExpWorth = 100;

	void FixedUpdate()
	{
	}

	void Update()
	{
		if(!hasItem)
		{
			bool itemToPickup = true;
			SortedList<float,Collider2D> sortedItems = FindClosestObject(Physics2D.OverlapCircleAll(transform.position, pickUpRange, LayerMask.GetMask("Item")));
			if(sortedItems.Count > 0)
			{
				Collider2D item = null;
				do 
				{
					item = sortedItems[0];
					sortedItems.RemoveAt(0);
				} while(maxWeight < item.GetComponent<ObjectBase>().weight && sortedItems.Count > 0);

				if(item != null)
				{
					//pickup item
				}
				else
				{
					itemToPickup = false;
				}
			}
			if(!itemToPickup)
			{
			}

		}
		else
		{
			SortedList<float,Collider2D> sortedEnemies = FindClosestObject(Physics2D.OverlapCircleAll(transform.position, throwRange, LayerMask.GetMask(new string[]{"Player", "Enemy"})));
			if(sortedEnemies.Count > 0)
			{
				Collider2D enemy = sortedEnemies[0];
				sortedEnemies.RemoveAt(0);

				//attack enemy
			}
			else
			{
				//move to enemy
			}
		}
	}

	public SortedList<float,Collider2D> FindClosestObject(Collider2D[] colliders)
	{
		SortedList<float,Collider2D> objectColliders = new SortedList<float,Collider2D>();
		if(colliders != null)
		{
			foreach(Collider2D c in colliders)
			{
				objectColliders.Add((c.transform.position - transform.position).magnitude, c);
			}
		}
		return objectColliders;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
<<<<<<< HEAD
		if(col.CompareTag("Item") && !hasItem)
		{
			//pick up item
		}
=======
>>>>>>> origin/master
	}
}
