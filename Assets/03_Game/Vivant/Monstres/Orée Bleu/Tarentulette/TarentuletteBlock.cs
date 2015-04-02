using UnityEngine;
using System.Collections;

public class TarentuletteBlock : MonoBehaviour {

	public GameObject bloqueur;

	public GameObject position1;
	public GameObject position2;
	public GameObject position3;
	public GameObject position4;

	public bool IsBlock;

	public void LetsBlock()
	{
		IsBlock = true;

		position1 = Instantiate(bloqueur, position1.transform.position, bloqueur.transform.rotation) as GameObject;
		position2=Instantiate(bloqueur, position2.transform.position, bloqueur.transform.rotation) as GameObject ;
		position3=Instantiate(bloqueur, position3.transform.position, bloqueur.transform.rotation) as GameObject ;
		position4=Instantiate(bloqueur, position4.transform.position, bloqueur.transform.rotation) as GameObject ;

        position1.transform.parent = this.gameObject.transform.parent.parent;
        position2.transform.parent = this.gameObject.transform.parent.parent;
        position3.transform.parent = this.gameObject.transform.parent.parent;
        position4.transform.parent = this.gameObject.transform.parent.parent;
	}

}
