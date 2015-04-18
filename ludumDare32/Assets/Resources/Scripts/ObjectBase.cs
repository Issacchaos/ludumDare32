using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

	public int weight;
	public int size_x; //image
	public int size_y; //image
	public bool picked_up = false;

	private Transform pivot;
	private Vector3 target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (picked_up) {
			transform.RotateAround(pivot.position, pivot.forward, Time.deltaTime * 10.0f);
		}
	}

	void FixedUpdate(){
		if (target != null) {
			transform.Translate(target * Time.deltaTime);
		}
	}

	public void Picked_up(Transform pos){
		transform.position = new Vector3(pos.position.x + 5.0f, pos.position.y, pos.position.z);
		pivot = pos;

		picked_up = true;
	}

	public void Fire(Vector3 mouse_pos){
		target = mouse_pos;
	}	
}
