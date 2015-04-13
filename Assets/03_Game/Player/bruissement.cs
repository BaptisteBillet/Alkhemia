using UnityEngine;
using System.Collections;

public class bruissement : MonoBehaviour {

	public Player player;

	public GameObject feuilles;

	// Update is called once per frame
	void Update () {
	
		if(player.moveSpeed>0)
		{
			feuilles.SetActive(true);
		}
		else
		{
			feuilles.SetActive(false);
		}

	}
}
