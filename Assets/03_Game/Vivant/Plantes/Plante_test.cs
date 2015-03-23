using UnityEngine;
using System.Collections;

public class Plante_test : Vivant {

	// Use this for initialization
	void Start () 
    {
        name = "plante_test";
        statut = "normal";
        life = 100;
        immortel = false;


        producteur = true;
        producteur_ok = true;
        recolte_fait = false;
        depecable = true;

        temps_recolte = 3;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        //EN CAS DE MORT
        if(life<=0)
        {
            Destroy(this.gameObject);
        }



	}
}
