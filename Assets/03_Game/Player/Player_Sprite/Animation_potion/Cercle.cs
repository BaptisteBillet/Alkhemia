using UnityEngine;
using System.Collections;

public class Cercle : MonoBehaviour {

	public GameObject cercle;
	private GameObject cercle_instantiate;

	public Player player_script;

	public void SpawnCercle()
	{
		cercle_instantiate = Instantiate(cercle, this.transform.position, cercle.transform.rotation)as GameObject;
		Destroy(cercle_instantiate, 1f);
		StartCoroutine(wait());
		
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds(1);
		player_script.CanMove = true;
	}


}
