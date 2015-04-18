﻿using UnityEngine;
using System.Collections;

public class PlayerBase : CharacterBase 
{

	private Vector3 moveVec = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		maxHealth = 100;
		health = maxHealth;
		moveSpeed = 5;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		print (Camera.main.ScreenToWorldPoint(Input.mousePosition));

		if(Input.GetMouseButtonDown(0))
		{
			if(hasItem)
			{
				print (Camera.main.ScreenToWorldPoint(Input.mousePosition));
				item.SendMessage("Fire",new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
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
