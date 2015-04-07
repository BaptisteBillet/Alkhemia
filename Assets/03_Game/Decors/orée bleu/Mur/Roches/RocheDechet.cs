using UnityEngine;
using System.Collections;

public class RocheDechet : MonoBehaviour {

	public GameObject dechet;

	public void Destroy()
	{
		Destroy(this.transform.parent.gameObject);
		Instantiate(dechet, this.transform.position, this.transform.rotation);

	}
}
