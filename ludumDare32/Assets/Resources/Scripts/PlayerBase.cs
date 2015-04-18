using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBase : CharacterBase 
{
	// Use this for initialization
	void Start () 
	{
		maxHealth = 100;
		health = maxHealth;
		moveSpeed = 1.8f;
		base.Start ();
//
//		levelSys = new Dictionary<int,List<int>> ();
//		
//		for(int i=1;i<=10;i++)
//		{
//			// a list in the order of maxWeight, max exp, max health
//			List<int> tmp = new List<int>();
//			tmp.Add(1 + (5 * i));
//			tmp.Add(100 + (50 * i));
//			tmp.Add(100 + (20 * i));
//			levelSys.Add (i,tmp);
//		}

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		for(int i=1;i<=10;i++)
		{
			Debug.Log(levelSys[i][0]); 
			Debug.Log (levelSys[i][1]);
			Debug.Log (levelSys[i][2]);
		}
		if(Input.GetMouseButtonDown(0))
		{
			if(hasItem)
			{
				Debug.Log(curWeight);
				Debug.Log(maxWeight);
				
				float throw_speed = max_throw_speed - ((curWeight / maxWeight) * max_throw_speed);
				Debug.Log (throw_speed);
				item.GetComponent<ObjectBase>().Fire(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), throw_speed);
				hasItem = false;
			}
			else if(gameObjects.Count > 0)
			{
				pick_up ();
			}
		}

		float xSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
		float ySpeed = Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime;
		moveVec = new Vector2 (xSpeed, ySpeed);
		transform.Translate(moveVec);

	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.CompareTag("Item"))
		{
			addItem(c.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.CompareTag("Item"))
		{
			removeItem(c.gameObject);
		}
	}

}
