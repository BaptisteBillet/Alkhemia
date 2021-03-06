﻿using UnityEngine;
using System.Collections;

public class Toile : MonoBehaviour {

	public Player player_script;
	public float ralentissement;

	private float movespeed_standard=2.5f;

	private bool IsSlow;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "toile")
		{
			player_script.max_moveSpeed = ralentissement;
			IsSlow = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "toile")
		{
			player_script.max_moveSpeed = movespeed_standard;
			IsSlow = false;
		}
	}

	
}
