using UnityEngine;
using System.Collections;

public class Tapis : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag=="Player")
		{
			
			Marchand.instance.IsPlayerInPosition = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Marchand.instance.IsPlayerInPosition = false;
		}
	}
}
