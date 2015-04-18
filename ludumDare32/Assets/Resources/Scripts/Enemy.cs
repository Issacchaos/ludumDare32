using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : CharacterBase
{
	public int weight = 0;
	public float damage = 0f;
	public float throwRange = 0f;
	public float maxSeekRange = 0f;
	public int baseExpWorth = 100;
	public bool wandering = false;

	void FixedUpdate()
	{
		bool objectToSeek = false;
		if(!hasItem)
		{
			SortedList<float,Collider2D> sortedItems = FindClosestObject(Physics2D.OverlapCircleAll(transform.position, maxSeekRange, LayerMask.GetMask("CollideItem")));
			if(sortedItems.Count > 0)
			{
				Collider2D item = null;
				do 
				{
					item = sortedItems.Values[0];
					sortedItems.RemoveAt(0);
				} while(maxWeight < item.GetComponent<ObjectBase>().weight && sortedItems.Count > 0);

				if(item != null)
				{
					objectToSeek = true;
					SetMoveVec(item.transform);
				}
			}
		}
		else
		{
			SortedList<float,Collider2D> sortedEnemies = FindClosestObject(Physics2D.OverlapCircleAll(transform.position, throwRange, LayerMask.GetMask(new string[]{"Player", "Enemy"})));
			if(sortedEnemies.Count > 0)
			{
				Collider2D enemy = sortedEnemies.Values[0];

				item.GetComponent<ObjectBase>().Fire(enemy.transform.position);
				hasItem = false;
			}
			else
			{
				sortedEnemies = FindClosestObject(Physics2D.OverlapCircleAll(transform.position, maxSeekRange, LayerMask.GetMask(new string[]{"Player", "Enemy"})));
				if(sortedEnemies.Count > 0)
				{
					objectToSeek = true;
					Collider2D enemy = sortedEnemies.Values[0];

					SetMoveVec(enemy.transform);
				}
			}
		}
		if(!objectToSeek)
		{
			if(!wandering)
			{
				StartCoroutine("Wander");
			}
		}
		else
		{
			StopWandering();
		}

		transform.Translate(moveVec * Time.deltaTime);
	}

	public SortedList<float,Collider2D> FindClosestObject(Collider2D[] colliders)
	{
		SortedList<float,Collider2D> objectColliders = new SortedList<float,Collider2D>();
		if(colliders != null)
		{
			foreach(Collider2D c in colliders)
			{
				if(c.gameObject.Equals(gameObject))
					continue;
				float mag = (c.transform.position - transform.position).magnitude;
				if(!objectColliders.ContainsKey(mag))
				{
					objectColliders.Add(mag, c);
				}
			}
		}
		return objectColliders;
	}

	public void SetMoveVec(Transform obj)
	{
		Vector3 toObj = (obj.position - transform.position).normalized;
		toObj *= moveSpeed;
		moveVec = toObj;
	}

	public IEnumerator Wander()
	{
		wandering = true;
		moveVec.x = Random.Range(-1f, 1f);
		moveVec.y = Random.Range (-1f, 1f);
		moveVec.Normalize();
		moveVec *= moveSpeed;
		yield return new WaitForSeconds(5f);
		wandering = false;
	}

	public void StopWandering()
	{
		StopCoroutine("Wander");
		wandering = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Item") && !hasItem)
		{
			ObjectBase obj = col.GetComponent<ObjectBase>();
			if(maxWeight >= obj.weight)
			{
				obj.Picked_up(gameObject);
				item = obj.gameObject;
				hasItem = true;
			}
		}
	}
}
