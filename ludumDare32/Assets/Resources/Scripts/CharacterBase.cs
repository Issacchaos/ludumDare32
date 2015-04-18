using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour 
{

	public int health;
	public int maxHealth;
	public float moveSpeed;
	public int maxWeight = 1;
	public int curWeight = 0;
	public int level = 1;
	public int exp = 0;
	public int maxExp;
	public float max_throw_speed = 5.0f;
	public int maxLevel = 10;

	public bool hasItem = false;
	public bool dead = false;

	public GameObject item;
	public List<GameObject> gameObjects;
	public Dictionary<int,List<int>> levelSys;

	protected Vector3 moveVec = Vector3.zero;


	// Use this for initialization
	protected void Start () 
	{
		gameObjects = new List<GameObject> ();
		levelSys = new Dictionary<int,List<int>> ();

		for(int i=1;i<=10;i++)
		{
			// a list in the order of maxWeight, max exp, max health
			List<int> tmp = new List<int>();
			tmp.Add(1 + (5 * i));
			tmp.Add(100 + (50 * i));
			tmp.Add(100 + (20 * i));
			levelSys.Add (i,tmp);
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
			if(level < maxLevel)
			{
				level += 1;
				maxWeight = levelSys[level][0];
				maxExp = levelSys[level][1];
				int tmp = maxHealth;
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
		GameObject closest = null;
		Debug.Log (gameObjects[0]);
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
				if(tmp < tmpDist || tmpDist == 0.0f)
				{
					closest = gameObjects[i];
					tmpDist = tmp;
				}
			}
		}

		if(item)
		{
			item = closest;
			hasItem = true;
			item.SendMessage ("Picked_up", gameObject);
			item.GetComponent<BoxCollider2D> ().isTrigger = true;
			item.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
		

	}

}
