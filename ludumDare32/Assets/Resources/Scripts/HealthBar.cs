using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public float healthWidth = 1;
	public float currentHealth;
	public float newWidth;
	public Image healthBar;

	// Use this for initialization
	void Start () {
		healthBar = gameObject.GetComponent<Image>();
		currentHealth = gameObject.GetComponentInParent<CharacterBase> ().health;
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = gameObject.GetComponentInParent<CharacterBase> ().health;
		newWidth = healthWidth * (currentHealth / 100); 
		ResizeBar (healthBar, newWidth);

	}

	private void ResizeBar(Image bar, float percent)
	{
		if(bar)
		{
			bar.fillAmount = percent;
		}
	}
}



