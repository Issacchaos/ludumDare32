using UnityEngine;
using System.Collections;

public class PlayerBase : CharacterBase 
{

	private Vector3 moveVec = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		maxHealth = 100;
		health = maxHealth;
		moveSpeed = 10;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(gameObjects.Count > 0 && !hasItem && Input.GetMouseButtonDown(0))
		{
			pick_up ();
		}

		float xSpeed = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		float ySpeed = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;
		moveVec = new Vector3 (xSpeed, ySpeed, 0.0f);
		transform.position += moveVec;

	}

}
