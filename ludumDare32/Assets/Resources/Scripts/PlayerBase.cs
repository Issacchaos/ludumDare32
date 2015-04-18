using UnityEngine;
using System.Collections;

public class PlayerBase : CharacterBase 
{

	private Vector3 moveVec = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		moveSpeed = 25;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			moveVec += transform.up;
		}
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			moveVec += (-transform.right);
		}
		if (Input.GetKeyDown (KeyCode.D)) 
		{
			moveVec += transform.right;
		}
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			moveVec += (-transform.up);
		}
		moveVec.Normalize();
		transform.position += (moveVec * moveSpeed * Time.deltaTime);
		moveVec = Vector3.zero;
	}
}
