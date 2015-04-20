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
	public bool active = false;
	public GameObject hb;
	public GameObject killedBy;

	protected override void Start(){
		base.Start ();
		maxWeight = levelSys [level] [0];
		maxExp = levelSys [level] [1];
		maxHealth = levelSys [level] [2];

		health = maxHealth;
		mAnim = GetComponent<Animator>();
	}

	void Update()
	{
		if (dead)
			Killed ();
	}

	void FixedUpdate()
	{
		if(active)
		{
			hb.SetActive(true);
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
					
					if(item != null && item.GetComponent<ObjectBase>().thrown != true)
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
					float throw_speed = max_throw_speed - (curWeight / maxWeight * max_throw_speed);
					//float angle = Random.Range(-10f, 10f);
					//float angle = 0.0f;
					float dmg = maxHealth * (curWeight/maxWeight);
					Vector3 tVec = enemy.transform.position;
					item.GetComponent<ObjectBase>().Fire(tVec, throw_speed, dmg);
					item.GetComponent<BoxCollider2D>().isTrigger = false;
					item.GetComponent<Rigidbody2D>().isKinematic = false;
					hasItem = false;
					curWeight = 0.0f;
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
			mAnim.SetFloat ("xSpeed", moveVec.x);
			mAnim.SetFloat ("ySpeed", moveVec.y);
			transform.Translate(moveVec * Time.deltaTime);
		}
		else
		{
			hb.SetActive(false);
		}
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

	public void Killed()
	{

		GameObject.FindGameObjectWithTag ("MapManager").GetComponent<MapManager> ().activeEnemies.Remove (gameObject);
		GameObject.FindGameObjectWithTag ("MapManager").GetComponent<MapManager> ().nonactiveEnemies.Remove (gameObject);

		//dead = true;
		enabled = false;
		if(item != null)
			item.GetComponent<ObjectBase>().picked_up = false;

		GetComponent<ObjectBase>().enabled = true;
		gameObject.layer = LayerMask.NameToLayer("CollideItem");
		tag = "Item";
		GetComponent<Rigidbody2D>().fixedAngle = false;
		Destroy (GetComponent<CircleCollider2D> ());
		Destroy (GetComponent<Enemy> ());
		Destroy (hb);
		//Destroy (GetComponent<Animator> ());
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(active)
		{
			if(col.CompareTag("Item") && !hasItem)
			{
				ObjectBase obj = col.GetComponent<ObjectBase>();
				if(obj != null)
				{
					if(maxWeight > obj.weight)
					{
						if(obj.thrown)
						{
							int tmp = Random.Range (1,10);
							if(tmp == 1 || tmp == 2)
							{
								obj.Picked_up(gameObject);
								item = obj.gameObject;
								hasItem = true;
								curWeight = obj.weight;
							}
						}
						else
						{
							obj.Picked_up(gameObject);
							item = obj.gameObject;
							hasItem = true;
							curWeight = obj.weight;
						}

					}
				}
			}
		}
	}

	void OnCollisionEnter2D (Collision2D c)
	{
			if(c.gameObject.CompareTag("Item"))
			{
				ObjectBase obj = c.gameObject.GetComponent<ObjectBase>();
				if(obj.who_threw != gameObject)
				{
					if(obj.thrown && active)
					{
						takeDamage(obj.GetComponent<ObjectBase>().damage);
						if(dead)
						{
							Debug.Log("modified exp");
							obj.who_threw.GetComponent<CharacterBase>().addExp((baseExpWorth * level));
						}
					}
					else if(obj.thrown && !active)
					{
						if(!GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().start)
						{
							GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().start = true;
						}

					}
				}
				else
				{

					obj.GetComponent<BoxCollider2D>().isTrigger = true;
				}

			}
		
	}

	void OnTriggerExit2D(Collider2D c){
		if(active)
		{
			if (c.gameObject.CompareTag ("Item")) {
				c.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			}
		}
	}
}
