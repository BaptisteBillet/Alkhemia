using UnityEngine;
using System.Collections;

public class Finish_script : MonoBehaviour {

	Animator anim;

	public GameObject artifice;

	private GameObject A_left;
	private GameObject A_right;

	void OnEnable()
	{
		anim = GetComponent<Animator>();

		anim.ResetTrigger("go");
		anim.SetTrigger("go");
	}

	IEnumerator Start_Fire()
	{
		float delta=0;

			A_left=Instantiate(artifice, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation)as GameObject;
			A_right=Instantiate(artifice, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation)as GameObject;

		while(delta<10)
		{
			delta += 0.1f;

			A_left.transform.position = new Vector3(this.transform.position.x+delta, this.transform.position.y, this.transform.position.z);
			A_right.transform.position = new Vector3(this.transform.position.x-delta, this.transform.position.y, this.transform.position.z);

			yield return new WaitForSeconds(0.000000001f);
		}

		Destroy(A_left);
		Destroy(A_right);

	}

}
