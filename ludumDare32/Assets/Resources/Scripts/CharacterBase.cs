using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour 
{

	public float health;
	public float maxHealth;
	public float moveSpeed;
	public float maxWeight = 1f;
	public float curWeight = 0f;
	public int level = 1;
	public int exp = 0;
	public float maxExp;
	public float max_throw_speed = 5.0f;
	public int maxLevel = 10;

	public bool hasItem = false;
	public bool dead = false;

	public GameObject item;
	public List<GameObject> gameObjects;
	public Dictionary<int,List<float>> levelSys;

	protected Vector3 moveVec = Vector3.zero;


	// Use this for initialization
	protected virtual void Start () 
	{
		gameObjects = new List<GameObject> ();
		levelSys = new Dictionary<int,List<float>> ();

		curWeight = 0;
		for(int i=1;i<=10;i++)
		{
			// a list in the order of maxWeight, max exp, max health
			List<float> tmp = new List<float>();
			tmp.Add(1 + (5 * i));
			tmp.Add(100 + (200 * i));
			tmp.Add(100 + (20 * i));
			levelSys.Add (i,tmp);
			foreach(float f in tmp)
			{
				Debug.LogError(f);
			}
		}


	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void addItem(GameObject c)
	{
		gameObjects.Add (c);
	}

	public void removeItem(GameObject c)
	{
		gameObjects.Remove(c);
	}

	public void takeDamage(float dmg)
	{
		Debug.LogWarning (dmg);

		if((health - dmg) <= 0.0f)
		{
			dead = true;
		}
		health -= dmg;
	}

	void addExp(int val)
	{
		if((exp + val) >= maxExp)
		{
			if(level < maxLevel)
			{
				level += 1;
				maxWeight = levelSys[level][0];
				maxExp = levelSys[level][1];
				float tmp = maxHealth;
				maxHealth = levelSys[level][2];
				if(health == tmp)
	             {
					health = maxHealth;
	             }
			}
		}
	}

	public void pick_up()
	{
		float tmpDist = 0.0f;
		float tmpDist2 = 0.0f;
		GameObject closest = null;
		GameObject thrownClosest = null;
		for(int i=0;i<gameObjects.Count;i++)
		{
			ObjectBase scr = gameObjects[i].GetComponent<ObjectBase>();
			if(scr.weight >= maxWeight)
			{
				continue;
			}
			else
			{
				float tmp = Vector3.Distance(transform.position,gameObjects[i].transform.position);
				if(scr.weight <= (maxWeight/2) && scr.thrown)
				{
					if(tmp < tmpDist2 || tmpDist2 == 0.0f)
					{
						tmpDist2 = tmp;
						thrownClosest = gameObjects[i];
					}
				}
				if(tmp < tmpDist || tmpDist == 0.0f)
				{
					closest = gameObjects[i];
					tmpDist = tmp;
				}
			}
		}
		
		if(thrownClosest)
		{
			item = thrownClosest;
			curWeight = thrownClosest.GetComponent<ObjectBase>().weight;
			thrownClosest.GetComponent<ObjectBase>().target = Vector3.zero;
			hasItem = true;
			item.SendMessage ("Picked_up", gameObject);
			item.GetComponent<BoxCollider2D> ().isTrigger = true;
			item.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
		else if(closest)
		{
			item = closest;
			curWeight = closest.GetComponent<ObjectBase>().weight;
			hasItem = true;
			item.SendMessage ("Picked_up", gameObject);
			item.GetComponent<BoxCollider2D> ().isTrigger = true;
			item.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
		

	}

}
