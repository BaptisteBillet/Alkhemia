using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {

    public int last_actual;

    public int quantite;        //quel quantité

    public bool active;         //Si la potion est utilisé 

    public string statut;       //Le statut de la potion //normal //feu ...

    // En cas de tir
    public int   actif_depense;   //Depense de la potion
    public float   actif_vitesse;   //Vitesse de parcours de la bullet

    // En cas de sort
    public int      sort_depense;     //Depense du sort
    public string   sort_mode;        //"toggle"

    public bool sort_actif;         //Si le sort interrupteur est sur on
    public float sort_temps;          //Si le sort s'utilise en timer
    
    //Lien avec le player
    public int numero; //La position de la potion dans l'inventaire.

    //Lieu de lancement;
    public GameObject fleche;

    //Fréquence de tir
    public float frequence;

	// Use this for initialization
	void Start () 
    {
        quantite = 100;
        active = false;
	}

	//PAR JEROME: J'ai fait gerer ca par toutes les potions, quand on change de potion ça reset ça
	protected int gachette_actif;
	public void SwappedPotion()
	{
		StopCoroutine("debit");
		gachette_actif = 0;
	}
}
