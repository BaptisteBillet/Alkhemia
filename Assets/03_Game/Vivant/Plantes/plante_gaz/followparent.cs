using UnityEngine;
using System.Collections;

public class followparent : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		this.transform.position = this.transform.parent.position;

	}
}
