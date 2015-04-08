using UnityEngine;
using System.Collections;

public class trois : MonoBehaviour {

	public Animator anim;

	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown("4"))
	{
		anim.SetTrigger("trois");
	}





	}
}
