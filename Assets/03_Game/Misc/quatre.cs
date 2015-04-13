using UnityEngine;
using System.Collections;

public class quatre : MonoBehaviour {

	public Animator anim;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("5"))
		{
			anim.SetTrigger("quatre");
		}
	}

}
