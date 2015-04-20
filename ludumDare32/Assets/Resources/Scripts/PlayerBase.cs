using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBase : CharacterBase 
{

	public GameObject hb;
	public int lives = 3;
	public Transform respawn;
	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();

		maxWeight = levelSys [level] [0];
		maxExp = levelSys[level][1];
		maxHealth = levelSys[level][2];
		health = maxHealth;
		moveSpeed = 1.8f;
		mAnim = GetComponent<Animator> ();
		mAnim.SetTrigger ("idle");

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

	void Update()
	{
		if (dead)
			killed ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
//		for(int i=1;i<=10;i++)
//		{
//			Debug.Log(levelSys[i][0]); 
//			Debug.Log (levelSys[i][1]);
//			Debug.Log (levelSys[i][2]);
//		}
		if(Input.GetMouseButtonDown(0))
		{
			if(hasItem)
			{
				Debug.Log(curWeight);
				Debug.Log(maxWeight);
				
				float throw_speed = max_throw_speed - ((curWeight / maxWeight) * (max_throw_speed/2));
				float dmg = maxHealth * (curWeight/maxWeight);

				item.GetComponent<ObjectBase>().Fire(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), throw_speed, dmg);
				item.GetComponent<BoxCollider2D>().isTrigger = false;
				item.GetComponent<Rigidbody2D>().isKinematic = false;
				hasItem = false;
				curWeight = 0.0f;
			}
			else if(gameObjects.Count > 0)
			{
				pick_up ();
			}
		}

		float xSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
		float ySpeed = Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime;

//		if(xSpeed > 0 && !mAnim.GetCurrentAnimatorStateInfo(0).IsName("Right"))
//		{
//			mAnim.SetTrigger ("movingRight");
//		}
//		else if( xSpeed < 0 && !mAnim.GetCurrentAnimatorStateInfo(0).IsName("Left"))
//		{
//			mAnim.SetTrigger("movingLeft");
//		}
//		else if( ySpeed < 0 && !mAnim.GetCurrentAnimatorStateInfo(0).IsName("Down"))
//		{
//			mAnim.SetTrigger("movingDown");
//		}
//		else if( ySpeed > 0 && !mAnim.GetCurrentAnimatorStateInfo(0).IsName("Up"))
//		{
//			mAnim.SetTrigger("movingUp");
//		}
//		else if(xSpeed == 0.0f && ySpeed == 0.0f && !mAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
//		{
//			//Debug.Log ("idled");
//			mAnim.SetTrigger("idle");
//		}
		mAnim.SetFloat ("xSpeed", xSpeed);
		mAnim.SetFloat ("ySpeed", ySpeed);
		if (xSpeed == 0.0f && ySpeed == 0.0f && !mAnim.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
		{
			mAnim.SetTrigger("idle");
		}
		moveVec = new Vector2 (xSpeed, ySpeed);
		transform.Translate(moveVec);
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();

		if (hasItem)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			mousePos.z = transform.position.z;
			Vector3 newPos = (mousePos - transform.position).normalized;
			item.transform.position = transform.position + newPos * 0.8f;
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.CompareTag("Item"))
		{
			if(!gameObjects.Contains(c.gameObject))
			{
				addItem(c.gameObject);
			}

			if(maxWeight <= c.GetComponent<ObjectBase>().weight){
				c.SendMessage("changeSprite", "enterB");
			}
			if(maxWeight > c.GetComponent<ObjectBase>().weight){
				c.SendMessage("changeSprite", "enterG");
			}
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.CompareTag("Item"))
		{	if(gameObjects.Contains(c.gameObject))
			{
				removeItem(c.gameObject);
			}
			c.SendMessage("changeSprite", "leave");
		}
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if(c.gameObject.CompareTag("Item"))
		{
			ObjectBase obj = c.gameObject.GetComponent<ObjectBase>();
			if(obj.who_threw != gameObject)
			{
				if(obj.thrown){
					Debug.LogWarning (obj.GetComponent<ObjectBase>().damage);
					takeDamage(obj.GetComponent<ObjectBase>().damage);
				}
			}
			else
			{
				obj.GetComponent<BoxCollider2D>().isTrigger = true;
			}
			
		}
	}

	public void killed()
	{
		if(lives > 0)
		{
			transform.position = respawn.position;
			health = maxHealth;
			curWeight = 0;
			exp = 0;
			lives -= 1;
			dead = false;
		}
		else
		{
			if(item != null)
				item.GetComponent<ObjectBase>().picked_up = false;
			
			if(hb)
			{
				hb.SetActive(false);
			}
			GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().gameOver = true;
			Destroy (GetComponent<PlayerBase> ());
		}

	}

}
