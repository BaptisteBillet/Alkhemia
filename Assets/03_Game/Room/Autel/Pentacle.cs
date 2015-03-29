using UnityEngine;
using System.Collections;

public class Pentacle : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag=="Player")
		{
			
			Autel.instance.IsPlayerInPosition = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Autel.instance.IsPlayerInPosition = false;
		}
	}
}
