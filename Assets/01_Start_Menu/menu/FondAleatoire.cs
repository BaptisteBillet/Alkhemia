using UnityEngine;
using System.Collections;

public class FondAleatoire : MonoBehaviour {

	public GameObject[] alea;
	float nbr_alea;
	// Use this for initialization
	void Start () {
		nbr_alea = Random.Range(0, alea.Length);

		alea[(int)nbr_alea].SetActive(true);


	}

}
