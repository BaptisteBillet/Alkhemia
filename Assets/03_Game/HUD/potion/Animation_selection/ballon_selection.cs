using UnityEngine;
using System.Collections;

public class ballon_selection : MonoBehaviour {

	public bool play_animation;
	private bool isplaying;
	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
	
		if(play_animation==true)
		{
			if(isplaying==false)
			{
				isplaying = true;
				anim.SetTrigger("go");
			}
		}

	}

	void end()
	{
		play_animation = false;
		isplaying = false;
		anim.ResetTrigger("go");
	}
}
