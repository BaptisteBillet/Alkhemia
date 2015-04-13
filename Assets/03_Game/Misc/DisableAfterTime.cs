using UnityEngine;
using System.Collections;

public class DisableAfterTime : MonoBehaviour {

	public bool no_time;
	public float time;

	// Use this for initialization
	void Start()
	{
		if (no_time == false)
		{
			this.gameObject.SetActive(false);
		}

	}

	public void Disable()
	{
		StartCoroutine(Disable_coroutine());
	}

	private IEnumerator Disable_coroutine ()
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(false);
	}

}
