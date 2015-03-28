using UnityEngine;
using System.Collections;

public class Potion_pierre : Potion {

	// Use this for initialization

    public GameObject bullet_prefab;
    private GameObject bullet;
    private Bullet bullet_script;

    private Player player_script; //Accès au script player de player
    private Player_Potion player_potion_script; //Accès au script player_potion

    public GameObject sort_prefab;
    private GameObject sort;

    /// <TWEEKABLE>
     public bool  impact_mort; 
     public int   impact_degat;

     public bool  duree_mort;
     public int   duree_temps;
     public int   duree_degat; 
     public float duree_couldown;
     public float duree_intervalle;
    /// </TWEEKABLE>

    //Pour le refresh de la duree du sort
     private float refresh;


    //Pour la manette xbox
    private int gachette_actif;
    private Vector3 last_xbox;
    private bool gachette_sort;


    //Spécial potion de pierre
    private float max_speed_player;



	void Start () 
    {

        player_script = (Player)transform.parent.gameObject.GetComponent(typeof(Player));
        player_potion_script = (Player_Potion)transform.parent.gameObject.GetComponent(typeof(Player_Potion));


       

        last_actual = Donjon.id_room_actual;

        gachette_actif = 0;
	}
	
    void initialisation_bullet()
    {
        //INITIALISATION bullet

        bullet_script = (Bullet)bullet_prefab.gameObject.GetComponent(typeof(Bullet));

        bullet_script.name = "bullet_pierre";                  //La potion envoie des bullets avec ce nom
        bullet_script.statut_bullet = statut;                      //Avec ce statut

        bullet_script.impact_mort = false;                   //Les bullets meurt à l'impact?
        bullet_script.impact_degat = impact_degat;           //A l'impact les bullets font ces dégâts

        bullet_script.duree_mort = true;                    //Les bullets meurt avec le temps?
        bullet_script.duree_temps = duree_temps;            //Au bout de combien de temps?
        bullet_script.duree_degat = duree_degat;            //Les dps
        bullet_script.duree_couldown = duree_couldown;      //Les dps se font pendant ce temps
        bullet_script.duree_intervalle = duree_intervalle;  //Le s de dps



        //
        active = false;
    }


    IEnumerator debit ()
    {

        yield return new WaitForSeconds(frequence);
        gachette_actif = 0;
        yield return null;
    }

	// Update is called once per frame
	void Update () 
    {

        /////////////////////////////////////////////////////////////////////////
        //ACTIF
        ////////////////////////////////////////////////////////////////////////

        #region actif
        if ((Input.GetAxis("TriggersR_1") == 1 || Input.GetMouseButton(0)) && gachette_actif == 0 && BagManager.instance.m_CanShoot == true)// && (Mathf.Abs(Input.GetAxis("R_XAxis_1")) + Mathf.Abs(Input.GetAxis("R_YAxis_1")) >= 0.9f))
        {    
            gachette_actif = 1;
         
        }

        if(gachette_actif==0)
        {
            player_potion_script.tir_actif = false;
        }



        if (gachette_actif == 1 && player_potion_script.potion_actuel == numero)
        {
            gachette_actif = 2;
            StartCoroutine(debit());

            player_potion_script.tir_actif = true;

            //CALCUL de la direction de la souris
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            //Vector3 dir = (Input.mousePosition - sp).normalized;

            Vector3 dir = fleche.transform.right ;

           
            if(quantite-actif_depense>=0) //Si il reste assez de potion
            {

                //INSTANTIATION
                initialisation_bullet(); //Parametre de la bullet
                bullet = Instantiate(bullet_prefab, fleche.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation

                //AJOUT en temps que child
                bullet.transform.parent = transform;
                //bullet.transform.position = transform.position;

                //ENVOI dans une direction
                bullet.GetComponent<Rigidbody2D>().velocity=(dir*actif_vitesse);
                //bullet.rigidbody2D.velocity = new Vector2(dir.x,dir.y) * actif_vitesse * Time.deltaTime;
                //On consome la potion
                quantite -= actif_depense;
            }



        }
        #endregion
        //On enleve toutes les bullets si le joueur sort de la map
        if (last_actual != Donjon.id_room_actual)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "bullet")
                {
                    GameObject.Destroy(child.gameObject);
                }
            }

            last_actual = Donjon.id_room_actual;
        }

        /////////////////////////////////////////////////////////////////////////
        //SORT
        ////////////////////////////////////////////////////////////////////////

        if (Input.GetAxis("TriggersL_1") == 0)
        {
            gachette_sort = false;
        }
        else if(Input.GetAxis("TriggersL_1") == 1)
        {
            gachette_sort = true;
        }




        if (((Input.GetMouseButtonDown(1) || gachette_sort == true) && quantite - sort_depense >= 0) && player_potion_script.potion_actuel == numero )
        {
    
            //Dépense
            quantite -= sort_depense;

           if(active==false)
           {
            active = true;
            //MAJ du statut du joueur
            player_script.statut = "normal";

            //INSTANTIATION
            sort = Instantiate(sort_prefab) as GameObject; //Instantiation

            //AJOUT en temps que child
            sort.transform.parent = transform;
            sort.transform.position = transform.position;

            refresh = sort_temps;

            max_speed_player = player_script.max_moveSpeed;

            
            
            //DESTRUCTION au bon d'un moment
            StartCoroutine(fin_sort());

           }
            else
           {
               refresh = sort_temps;
           }
        }

	}

    IEnumerator fin_sort()
    {
        while(refresh>=0)
        {
            player_script.immortel = true;
            player_script.max_moveSpeed = 0;
            refresh--;
            yield return new WaitForSeconds(1f);
        }

        //On devient en pierre
        player_script.immortel = false;
        player_script.max_moveSpeed = max_speed_player;
        Destroy(sort.gameObject);
        active = false;
        if (player_script.statut == "normal")
        {
            player_script.statut = "normal";
        }

        yield return null;
    }


    

}
