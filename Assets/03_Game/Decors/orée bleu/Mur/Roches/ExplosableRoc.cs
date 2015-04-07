using UnityEngine;
using System.Collections;

public class ExplosableRoc : MonoBehaviour
{

	//Prefab d'explosion
	public GameObject explosion_prefab;
	private GameObject explosion;

	public Animator anim;

	void Start()
	{
		if (this.gameObject.GetComponent<Rigidbody2D>() == null)
		{
			Debug.LogError("Le GameObject " + this.gameObject.name + " à besoin d'un rigidbody2D");
		}
		if (this.gameObject.GetComponent<Collider2D>() == null)
		{
			Debug.LogError("Le GameObject " + this.gameObject.name + " à besoin d'un collider non trigger");
		}
		if (explosion_prefab == null)
		{
			Debug.LogError("Le GameObject " + this.gameObject.name + " à besoin de du prefab d'explosion");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "explosion")
		{
			anim.SetTrigger("explose");
		}
	}

}
