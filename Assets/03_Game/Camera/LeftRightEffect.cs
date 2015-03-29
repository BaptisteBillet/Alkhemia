using UnityEngine;
using System.Collections;

public class LeftRightEffect : MonoBehaviour {

    float m_Rotation=0.01f;

	void Start()
	{
		StartCoroutine(rotate());
	}

	IEnumerator rotate()
	{
		while (this.gameObject != null)
		{
			//LES DEUX

			while ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("L_XAxis_1") < 0) || Input.GetKey(KeyCode.Q)) && (Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("L_XAxis_1") > 0) || Input.GetKey(KeyCode.D)))
			{
					if (this.gameObject.transform.rotation.z > 0)
					{
						this.gameObject.transform.eulerAngles = new Vector3(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z - m_Rotation);
					}
					else if (this.gameObject.transform.rotation.z < 0)
					{
						this.gameObject.transform.eulerAngles = new Vector3(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z + m_Rotation);
					}
					yield return new WaitForSeconds(0.01f);
			}

			Debug.Log(this.gameObject.transform.eulerAngles.z);
				//GAUCHE
				
					while ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("L_XAxis_1") < 0) || Input.GetKey(KeyCode.Q)))
					{
						if (this.gameObject.transform.eulerAngles.z !=0.5f )
						{
							this.gameObject.transform.eulerAngles += new Vector3(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z + m_Rotation);
						}
						yield return new WaitForSeconds(0.01f);
					}
				
				//DROITE
				
					while ((Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("L_XAxis_1") > 0) || Input.GetKey(KeyCode.D)))
					{
						if (this.gameObject.transform.eulerAngles.z != 355)
						{
							this.gameObject.transform.eulerAngles += new Vector3(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z - m_Rotation);
						}
						yield return new WaitForSeconds(0.01f);
					}

					yield return new WaitForSeconds(0.1f);
			}
			
			
		
	}

	
}
