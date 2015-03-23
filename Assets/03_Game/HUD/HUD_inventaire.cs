using UnityEngine;
using System.Collections;

public class HUD_inventaire : MonoBehaviour {


    public GameObject[] place;

    public GameObject I_touffe;
    public GameObject I_fraise;
    public GameObject I_fruit_rare;
    public GameObject I_cendre;
    public GameObject I_essence;

    public GameObject player;
    private Player player_script;

    private Ingredient ingredient_script;

    private bool ajout;

    private GameObject ingredient;

	// Use this for initialization
	void Start () {
        ajout = false;
        player_script = (Player)player.GetComponent(typeof(Player));
       
        
	}
	
    void actualisation_inventaire()
    {
        for(int i=0;i<player_script.inventaire.Length;i++)
        {
            //player_script = (Player)player.GetComponent(typeof(Player));
            if(player_script.inventaire[i]!=null)
            {
                ingredient_script = (Ingredient)player_script.inventaire[i].GetComponent(typeof(Ingredient));

                if (place[i].transform.childCount==0)
                {

                    if (ingredient_script.name == "fraise")
                    {
                        ingredient = Instantiate(I_fraise) as GameObject;
                        ingredient.transform.parent = place[i].transform;
                        ingredient.transform.localPosition = new Vector3(0, 0, 0);
                        ingredient.transform.localScale = new Vector3(1, 1, 1);
                        ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        
                    }

                    if (ingredient_script.name == "cendre")
                    {
                        ingredient = Instantiate(I_cendre) as GameObject;
                        ingredient.transform.parent = place[i].transform;
                        ingredient.transform.localPosition = new Vector3(0, 0, 0);
                        ingredient.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                    }

                    if (ingredient_script.name == "essence")
                    {
                        ingredient = Instantiate(I_essence) as GameObject;
                        ingredient.transform.parent = place[i].transform;
                        ingredient.transform.localPosition = new Vector3(0, 0, 0);
                        ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                        ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                    }

                    if (ingredient_script.name == "touffe")
                    {
                        ingredient = Instantiate(I_touffe) as GameObject;
                        ingredient.transform.parent = place[i].transform;
                        ingredient.transform.localPosition = new Vector3(0, 0, 0);
                        ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                        ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                    }

                    if (ingredient_script.name == "rare")
                    {
                        ingredient = Instantiate(I_fruit_rare) as GameObject;
                        ingredient.transform.parent = place[i].transform;
                        ingredient.transform.localPosition = new Vector3(0, 0, 0);
                        ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                        ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                    }
                }
            }

        }


        ajout = false;
    }

	// Update is called once per frame
	void Update () 
    {
	    
            actualisation_inventaire();
        

	}
}
