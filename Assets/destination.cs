using UnityEngine;
using System.Collections;

public class destination : MonoBehaviour {

	public Balade balade;
	// Update is called once per frame
	void Update () {

		this.transform.position = balade.destination;
	}
}
