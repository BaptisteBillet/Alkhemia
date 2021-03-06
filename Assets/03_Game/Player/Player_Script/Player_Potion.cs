﻿using UnityEngine;
using System.Collections;

public class Player_Potion : MonoBehaviour
{
    private Potion potion_instance;

    //Tableau des potions en 0 1 2 3 
    public Potion[] tab_potion;

    //Accès aux scripts
    public Potion potion_script_0;
    public Potion potion_script_1;
    public Potion potion_script_2;
    public Potion potion_script_3;

    //Les potions
    public Potion potion_feu;     //potion_script = (Potion)potion_feu.GetComponent(typeof(Potion));
    public Potion potion_toxic; //potion_script = (Potion)potion_toxic.GetComponent(typeof(Potion));
    public Potion potion_vent;
    public Potion potion_pierre;
   

    //La potion utilisé actuellement
    public int potion_actuel;

    public GameObject fleche;

    public Animator anim;
    public bool tir_actif; //Pour les animations

	public ballon_selection potion1;
	public ballon_selection potion2;

    // Use this for initialization
    void Awake()
    {
        //Initialisation variables
        potion_actuel = 0; //La potion actuel est la première
        tab_potion = new Potion[4]; //on indique 4 potions 0 1 2 et 3


        ////FEU

        //On rend existance une potion
        potion_instance = Instantiate(potion_feu) as Potion; //Instantiation
        potion_instance.transform.parent = transform; //CHILD
        potion_instance.transform.position = transform.position;

        potion_instance.fleche = fleche;

        //Pour le moment cette potion devient la première du tableau, et elle est active
        tab_potion[0] = potion_instance;
        //On s'assure de pouvoir y accéder en tout temps.
        potion_script_0 = (Potion)tab_potion[0].GetComponent(typeof(Potion));
        //Et on lui indique son numéro
        potion_script_0.numero = 0;

        ///TOXIC

        //On rend existance une potion
        potion_instance = Instantiate(potion_toxic) as Potion; //Instantiation
        potion_instance.transform.parent = transform; //CHILD
        potion_instance.transform.position = transform.position;

        potion_instance.fleche = fleche;

        //Pour le moment cette potion devient la deuxième du tableau, et elle est inactive
        tab_potion[1] = potion_instance;
        //On s'assure de pouvoir y accéder en tout temps.
        potion_script_1 = (Potion)tab_potion[1].GetComponent(typeof(Potion));

        //Et on lui indique son numéro
        potion_script_1.numero = 1;

        /*
        ///VENT

        //On rend existance une potion
        potion_instance = Instantiate(potion_vent) as Potion; //Instantiation
        potion_instance.transform.parent = transform; //CHILD
        potion_instance.transform.position = transform.position;

        potion_instance.fleche = fleche;

        //Pour le moment cette potion devient la deuxième du tableau, et elle est inactive
        tab_potion[2] = potion_instance;
        //On s'assure de pouvoir y accéder en tout temps.
        potion_script_2 = (Potion)tab_potion[2].GetComponent(typeof(Potion));

        //Et on lui indique son numéro
        potion_script_2.numero = 2;

        ///VENT

        //On rend existance une potion
        potion_instance = Instantiate(potion_pierre) as Potion; //Instantiation
        potion_instance.transform.parent = transform; //CHILD
        potion_instance.transform.position = transform.position;

        potion_instance.fleche = fleche;

        //Pour le moment cette potion devient la deuxième du tableau, et elle est inactive
        tab_potion[3] = potion_instance;
        //On s'assure de pouvoir y accéder en tout temps.
        potion_script_3 = (Potion)tab_potion[3].GetComponent(typeof(Potion));

        //Et on lui indique son numéro
        potion_script_3.numero = 3;
        */

    }

    void Update()
    {
        //Selection de la potion 1
        if (Input.GetKeyDown("1") || Input.GetAxis("DPad_YAxis_1") > 0)
        {
			potion1.play_animation = true;
            if(tab_potion[0]!=null) //Si il existe une potion à cet emplacement
            {
                if (potion_script_0.active == false) //Si cette potion n'est pas l'ancienne
				{
					SoundManagerEvent.emit(SoundManagerType.PLAYER_POTION);
                    //tab_potion[potion_actuel].gameObject.SetActive(false);  //On désactive la potion active actuel, ancienne
                    potion_actuel = 0;                                          //Cette potion devient l'actuel
                    potion_script_0.active = false;                              //On active la potion
					potion_script_1.SwappedPotion();
					potion_script_0.SwappedPotion();
                    //tab_potion[potion_actuel].gameObject.SetActive(true);   //On active la potion active nouvelle
                }
            }
        }
        else if (Input.GetKeyDown("2") ||Input.GetAxis("DPad_XAxis_1") > 0)
        {
			potion2.play_animation = true;
            if (tab_potion[1] != null)
            {
                if (potion_script_1.active == false)
				{
					SoundManagerEvent.emit(SoundManagerType.PLAYER_POTION);
                    //tab_potion[potion_actuel].gameObject.SetActive(false);  //On désactive la potion active actuel, ancienne
                    potion_actuel = 1;                                          //Cette potion devient l'actuel
					potion_script_1.active = false;                              //On active la potion
					potion_script_0.SwappedPotion();
					potion_script_1.SwappedPotion();
                    //tab_potion[potion_actuel].gameObject.SetActive(true);   //On active la potion active nouvelle
                }
            }
        }
        else if (Input.GetKeyDown("3") ||Input.GetAxis("DPad_YAxis_1")<0)
        {
            if (tab_potion[2] != null)
            {
                if (potion_script_2.active == false)
				{

                    //tab_potion[potion_actuel].gameObject.SetActive(false);  //On désactive la potion active actuel, ancienne
                    potion_actuel = 2;                                          //Cette potion devient l'actuel
                    potion_script_2.active = false;                              //On active la potion
                    //tab_potion[potion_actuel].gameObject.SetActive(true);   //On active la potion active nouvelle
                }
            }
        }
        else if (Input.GetKeyDown("4") || Input.GetAxis("DPad_XAxis_1") < 0)
        {
            if (tab_potion[3] != null)
            {
                if (potion_script_3.active == false)
                {
                    //tab_potion[potion_actuel].gameObject.SetActive(false);  //On désactive la potion active actuel, ancienne
                    potion_actuel = 3;                                          //Cette potion devient l'actuel
                    potion_script_3.active = false;                              //On active la potion
                    //tab_potion[potion_actuel].gameObject.SetActive(true);   //On active la potion active nouvelle
                }
            }
        }


        anim.SetBool("tir_actif", tir_actif);


    }


}
