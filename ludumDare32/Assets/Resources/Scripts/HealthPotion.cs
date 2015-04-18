using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {
	
	private int amount = 50;

	public void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Player") || col.CompareTag("Enemy")) {
			heal(col.gameObject);
		}

	}


	public void heal(GameObject p)
	{
		CharacterBase c = p.GetComponent<CharacterBase>();
		c.health += amount;
		c.health = Mathf.Clamp (c.health, 0, c.maxHealth);
		Destroy (this);
	}


}