using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

	public int weight;
	public int size_x; //image
	public int size_y; //image
	public bool picked_up = false;
	public bool thrown = false;

	private GameObject pivot;
	private Vector3 target = Vector3.zero;
	private float speed;

	// Use this for initialization
	void Start () {
		//just in case something goes wrong
		speed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (picked_up && target == Vector3.zero) {
			transform.position = new Vector3(pivot.transform.position.x + 1.0f, pivot.transform.position.y, pivot.transform.position.z);
			//transform.RotateAround(pivot.transform.position, pivot.transform.forward, Time.deltaTime * 50.0f);
		}
	}

	void FixedUpdate(){
		if (target != Vector3.zero) {
			transform.position = transform.position + target.normalized * Time.deltaTime * speed;
		}
	}

	public void Picked_up(GameObject pos){
		transform.position = new Vector3(pos.transform.position.x + 1.0f, pos.transform.position.y, pos.transform.position.z);
		pivot = pos;

		picked_up = true;
	}

	public void Fire(Vector3 mouse_pos, float throw_speed){
		target = mouse_pos - transform.position;
		thrown = true;
		speed = throw_speed;
	}	
}
