using UnityEngine;
using System.Collections;

public class bouton_sacoche : MonoBehaviour {

	public GameObject bouton_xboxY;
	public GameObject bouton_clavier;


	// Update is called once per frame
	void Update () {
	
		if(ControllerManager.instance.m_XboxMode==true)
		{
			bouton_xboxY.SetActive(true);
			bouton_clavier.SetActive(false);
		}
		else
		{
			bouton_clavier.SetActive(true);
			bouton_xboxY.SetActive(false);
		}


	}
}
