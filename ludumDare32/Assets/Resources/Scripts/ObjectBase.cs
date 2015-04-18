using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

	public int weight;
	public int size_x; //image
	public int size_y; //image
	public bool picked_up = false;

	private GameObject pivot;
	private Vector3 target = Vector3.zero;

	// Use this for initialization
	void Start () {
	
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
			transform.position = transform.position + target.normalized * Time.deltaTime * 10.0f;
		}
	}

	public void Picked_up(GameObject pos){
		transform.position = new Vector3(pos.transform.position.x + 1.0f, pos.transform.position.y, pos.transform.position.z);
		pivot = pos;

		picked_up = true;
	}

	public void Fire(Vector3 mouse_pos){
		Debug.Log (mouse_pos);
		target = mouse_pos - transform.position;
	}	
}
