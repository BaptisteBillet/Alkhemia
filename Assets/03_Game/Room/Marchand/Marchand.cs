using UnityEngine;
using System.Collections;

public class Marchand : MonoBehaviour {
	#region Singleton
	static private Marchand s_Instance;
	static public Marchand instance
	{
		get
		{
			return s_Instance;
		}
	}

	void Awake()
	{

		if (s_Instance == null)
			s_Instance = this;
		//DontDestroyOnLoad(this);
	}
	#endregion

	public GameObject tapis;

	public GameObject effet;
	private GameObject effect_instance;

	public GameObject fraise;

	private GameObject origin;

	public bool IsPlayerInPosition;
	public bool ReadyForChange;

	private bool ouvert;

	GameObject marchandise_instance;
	void Start()
	{
		ouvert = false;
		ReadyForChange = false;
	}
    /*
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (IsPlayerInPosition == true && ouvert==false)
			{
				ReadyForChange = true;
				ouvert = true;
				Ouverture_sac.instance.IsouvertureForce = true;
				Ouverture_sac.instance.ouvertureForce = true;
			}
		}

	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			ouvert = false;
			Ouverture_sac.instance.IsouvertureForce = false;
		}
	}

	public void autel_activation(GameObject Origin)
	{
		origin=Origin;
		//StartCoroutine(autel_effet());
		apparition();
	}

	IEnumerator autel_effet()
	{
		effect_instance=Instantiate(effet, tapis.transform.position, effet.transform.rotation) as GameObject ;
		yield return new WaitForSeconds(3);
		apparition();
		yield return new WaitForSeconds(3);
		Destroy(effect_instance);

	}

	void apparition()
	{

		marchandise_instance = Instantiate(fraise, tapis.transform.position, fraise.transform.rotation) as GameObject ;
		marchandise_instance.GetComponent<SpriteRenderer>().sortingLayerName = "Element";
		marchandise_instance.GetComponent<SpriteRenderer>().sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;                                                                    
		
	}
    */

}
