using UnityEngine;
using System.Collections;

public class HUD_inventaire : MonoBehaviour {


    public GameObject[] place= new GameObject[10];

    public GameObject I_touffe;
    public GameObject I_fraise;
    public GameObject I_fruit_rare;
    public GameObject I_cendre;
    public GameObject I_essence;
    public GameObject I_pommedepin;
	public GameObject I_champignon;
	public GameObject I_venin;
	public GameObject I_rare;

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
	
    public void drop_inventaire(int index)
    {
        player_script.inventaire[index] = null;
        player_script.place_inventaire++;

    }

    void actualisation_inventaire()
    {
        for(int i=0;i<player_script.inventaire.Length;i++)
        {
            if (i <= 10)
            {
                if (player_script.inventaire[i] != null)
                {

                    ingredient_script = (Ingredient)player_script.inventaire[i].GetComponent(typeof(Ingredient));

                    if (place[i].transform.childCount == 0)
                    {
                        if (ingredient_script.name == "I_Fraise")
                        {
                            ingredient = Instantiate(I_fraise) as GameObject;
                            ingredient.transform.parent = place[i].transform;
                            ingredient.transform.localPosition = new Vector3(0, 0, 0);
                            ingredient.transform.localScale = new Vector3(1, 1, 1);
                            ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                        }

                        if (ingredient_script.name == "I_cendre")
						{
							
                            ingredient = Instantiate(I_cendre) as GameObject;
                            ingredient.transform.parent = place[i].transform;
                            ingredient.transform.localPosition = new Vector3(0, 0, 0);
                            ingredient.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                        }

                        if (ingredient_script.name == "I_essence")
                        {
                            ingredient = Instantiate(I_essence) as GameObject;
                            ingredient.transform.parent = place[i].transform;
                            ingredient.transform.localPosition = new Vector3(0, 0, 0);
                            ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                            ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                        }

                        if (ingredient_script.name == "I_touffe")
                        {
                            ingredient = Instantiate(I_touffe) as GameObject;
                            ingredient.transform.parent = place[i].transform;
                            ingredient.transform.localPosition = new Vector3(0, 0, 0);
                            ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                            ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                        }

                        if (ingredient_script.name == "I_pommedepin")
                        {
                            ingredient = Instantiate(I_pommedepin) as GameObject;
                            ingredient.transform.parent = place[i].transform;
                            ingredient.transform.localPosition = new Vector3(0, 0, 0);
                            ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                            ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                        }
                        if (ingredient_script.name == "I_champignon")
                        {
                            ingredient = Instantiate(I_champignon) as GameObject;
                            ingredient.transform.parent = place[i].transform;
                            ingredient.transform.localPosition = new Vector3(0, 0, 0);
                            ingredient.transform.localScale = new Vector3(1f, 1f, 1);
                            ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
							QuestEventManager.quest_emit(QuestEventManagerType.ADD_MUSH);
                        }


						if (ingredient_script.name == "I_venin")
						{
							ingredient = Instantiate(I_venin) as GameObject;
							ingredient.transform.parent = place[i].transform;
							ingredient.transform.localPosition = new Vector3(0, 0, 0);
							ingredient.transform.localScale = new Vector3(1f, 1f, 1);
							ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
							QuestEventManager.quest_emit(QuestEventManagerType.ADD_VENON);

						}

						if (ingredient_script.name == "I_rare")
						{
							ingredient = Instantiate(I_rare) as GameObject;
							ingredient.transform.parent = place[i].transform;
							ingredient.transform.localPosition = new Vector3(0, 0, 0);
							ingredient.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
							ingredient.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
							QuestEventManager.quest_emit(QuestEventManagerType.ADD_GOLDEN);

						}
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
