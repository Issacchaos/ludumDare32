using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

	public int weight;
	public int size_x; //image
	public int size_y; //image
	public bool picked_up = false;
	public Transform player_pos;

	private Transform pivot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (picked_up) {
			//Rotate around the mouse position
			Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player_pos.position;
			Vector3 obj = transform.position - player_pos.position;

			float angle = Vector2.Angle(new Vector2(obj.x, obj.y), new Vector2(mouse.x, mouse.y));
		
			Debug.Log (angle);
			Debug.Log (mouse);
			Debug.Log(obj);

			//pivot.position = target.position;
			//pivot.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}

	void FixedUpdate(){

	}

	public void Picked_up(Transform pos){
		transform.position = new Vector3(pos.position.x + 5.0f, pos.position.y, pos.position.z);
		pivot = pos;

		picked_up = true;
	}
}
