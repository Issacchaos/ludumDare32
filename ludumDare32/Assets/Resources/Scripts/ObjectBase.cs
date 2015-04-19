using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

	public int weight;
	public float size_x; //image
	public int size_y; //image
	public bool picked_up = false;
	public bool thrown = false;
	public GameObject who_threw;

	private GameObject pivot;
	public Vector3 target = Vector3.zero;
	private float speed;
	private string id;

	// Use this for initialization
	void Start () {
		//just in case something goes wrong
		speed = 2.0f;

		size_x = GetComponent<SpriteRenderer> ().sprite.bounds.size.x;


		Debug.Log (size_x);
	}
	
	// Update is called once per frame
	void Update () {
		if (picked_up && target == Vector3.zero) {
			transform.position = new Vector3(pivot.transform.position.x + 0.15f + size_x/2, pivot.transform.position.y, pivot.transform.position.z);
			//transform.RotateAround(pivot.transform.position, pivot.transform.forward, Time.deltaTime * 50.0f);
		}
	}

	void FixedUpdate(){
		if (target != Vector3.zero) {
			transform.position = transform.position + target.normalized * Time.deltaTime * speed;
			transform.Rotate(Vector3.forward, Time.deltaTime * 500.0f);
		}
	}

	public void Picked_up(GameObject pos){
		who_threw = pos;
		transform.position = new Vector3(pos.transform.position.x + 0.15f + size_x/2, pos.transform.position.y, pos.transform.position.z);
		pivot = pos;

		picked_up = true;
	}

	public void Fire(Vector3 mouse_pos, float throw_speed){
		target = mouse_pos - transform.position;
		thrown = true;
		speed = throw_speed;
	}	

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.CompareTag("Wall"))
		{
			target = Vector3.zero;
		}
	}

}
