using UnityEngine;
using System.Collections;

public class potion_hud : MonoBehaviour {

    public int numero_potion;
    public int numero_apparition;

    public GameObject Player;
    private Player_Potion player_potion_script;
    private Potion potion_script;

    public GameObject contenu;

    private int potion_max;
    public int potion_actuel; //pourcentage

    public GameObject selection;

	// Use this for initialization
	void Start () {


        player_potion_script = (Player_Potion)Player.GetComponent(typeof(Player_Potion));
        
        switch(numero_potion)
        {
                case 1:
                potion_script= player_potion_script.potion_script_0;
                break;
                case 2:
                potion_script= player_potion_script.potion_script_1;
                break;
                case 3:
                potion_script= player_potion_script.potion_script_2;
                break;
                case 4:
                potion_script= player_potion_script.potion_script_3;
                break;

        }

        potion_max = potion_script.quantite;
       
        potion_actuel = potion_script.quantite * 100 / potion_max;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //Transmission en pourcentage
        potion_actuel = potion_script.quantite * 100 / potion_max;

        contenu.transform.localPosition = new Vector3(contenu.transform.localPosition.x, -0.7f+(-(potion_actuel)*-0.01f)-1, contenu.transform.localPosition.z);


        if(player_potion_script.potion_actuel==numero_apparition)
        {
            selection.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
        }

	}
}
