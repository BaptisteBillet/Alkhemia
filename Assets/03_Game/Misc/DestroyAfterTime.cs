using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public bool no_time;
	public float time;

	// Use this for initialization
	void Start () {
		if(no_time==false)
		{
			Destroy(this.gameObject, time);	
		}
		
	}

	public void DestoyNow()
	{
		Destroy(this.gameObject, time);	
	}

}
