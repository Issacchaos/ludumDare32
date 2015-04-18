using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {
	
	private int ammount = 50;

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") || col.gameObject.CompareTag ("Enemy")) 
		{
			heal(col.gameObject);
		}
	}

	public void heal(GameObject p)
	{
		CharacterBase c = p.GetComponent<CharacterBase>();
		c.health += ammount;
		c.health = Mathf.Clamp (c.health, 0, c.maxHealth);
		Destroy (this);
	}


}