using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    //Références
        //Le donjon
   // public GameObject donjon;
    //private Donjon donjon_script;
        //Les objets en contact
    private Vivant vivant_script;

        //Le cadran et l'aiguille
    public GameObject cadran_prefab;
    private GameObject cadran;
    private Chargement_cadran cadran_script;

    //Point de spawn
    public Transform spawn_up;
    public Transform spawn_right;
    public Transform spawn_down;
    public Transform spawn_left;

    //Variables usuelle
    public bool big_room;

    //Déplacement
    public float moveSpeed;             //Vitesse de déplacement actuel
    public float max_moveSpeed;         //Maximum de vitesse
    public float acceleration;          //Vitesse en acceleration
    public float desceleration;          //Vitesse en desceleration

    private Vector3 velocity;           //Le vecteur de vitesse
                                        //Ces booléens sont true quand ils sont appuyés (input)
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    //Sprite 
    public SpriteRenderer sprite;


    //Statut du joueur
    public string statut; //normal //feu //toxic //vent
    public string statut_temporaire;

    //Mouvement pour l'animation
    public int move; //0 idle // 1 up // 2 right // 3 down // 4 left

    //Pour les contacts 
    public bool immortel;   //Immortalité
    public float life;        //vie
    public float life_max;
    public float life_alerte;
    //Pour les récoltes, depessages etc
    public bool busy;

    public bool recolte_en_cours;
    public bool depecage_en_cours;

    //POUR LA VISEE
    public bool mode_xbox;

    //Animation
    public Animator anim;

    //Timer
    public int general_time;
    public bool stop_timer;
    private bool stop_last;


    //Acces au script de room
    private up_sprite up_script;
    private down_sprite down_script;
    private left_sprite left_script;
    private right_sprite right_script;

    //Inventaire
    public GameObject[] inventaire = new GameObject[1];
    public int place_inventaire;

    //Accès à l'ingredient
    private Ingredient ingredient;

    //temps invincible après touché
    public float temps_invincible;

    //Renouveau du poison, feu...
    private float delay;

	//Renouveau de la toile

    //Deplacement changement de pièces
    private float smooth=5f;
    private float destination;
    private Vector3 arrive;

    private string direction="null";

    //Pour le camera shake de la vie faible
    public bool alertelaunched;

	private Rigidbody2D rigidbody;

	//KNOCKBACK
	private Vector3 knockback_direction;
	private float knockback_force;

	public bool CanMove;
	private bool sort;

	//AFFICHAGE RECOLTE
	public GameObject sprite_E;
	public GameObject sprite_RB;
	

    ///////////////////////////////////////////////////////
	void Awake ()
    {
		CanMove = true;
        //donjon_script = (Donjon)donjon.GetComponent(typeof(Donjon));
		rigidbody = GetComponent<Rigidbody2D>();
        delay = 0; //Doit rester à 0

        #region Initialisation

        place_inventaire = inventaire.Length;

        statut = "normal";
        statut_temporaire = "normal";
        temps_invincible = 1;

        life = life_max;

        general_time = 1;
        /*
       
        big_room = false;
        moveSpeed = 0;
        max_moveSpeed = 2.5f;
        acceleration = 1.0f;
        desceleration = 0.4f;

        immortel = false;
        stop_timer = true;
        
        general_time = 5;

        life = 10;

        busy = false;
        */

        stop_last = stop_timer;
        #endregion

        //Lancement du Timer
       // StartCoroutine(couldown());
        
        //Positionnement layer
        sprite.sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;

        life_alerte = 20 * life_max / 100;
    }

    IEnumerator couldown()
    {
        while (general_time > 0 && stop_timer == false)
        {
                yield return new WaitForSeconds(1);
                general_time--;
        }
        stop_last = stop_timer;
    }


    void Update()
    {

        //Déplacements
        GetComponent<Rigidbody2D>().velocity = velocity*moveSpeed;

        //Vérification de la mort
        if(general_time<=0 || life<=0)
        {
            //on lance l'anim de mort
            anim.SetTrigger("dies");

            Debug.Log("MORT");
            velocity=new Vector3(0,0,0);
            Invoke("GoTo_Gameover", 2f);
        }

        //Vérification d'une relance du couldown eventuellement stoppé par un autre processus
        if(stop_last!=stop_timer)
        {
            if(stop_timer==true)
            {
                stop_last = stop_timer;
                StartCoroutine(couldown());
            }
        }

        //Positionnement layer
        sprite.sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;  //////////////////////////////////////////////à prio j'ai foutu le bordel sur les layers <3 (Ryan)
        
        //system d'alerte
        if(life<=life_alerte && alertelaunched==false)
        {
            alertelaunched = true;
            ShakeManager.instance.LetsShakeLife(this);
        }

		if(CanMove==false && sort==false)
		{
			sort = true;
			anim.SetTrigger("sort");
			//Déplacement

			
			
		}

		if(CanMove==true)
		{
			sort = false;
		}

        
    }


    //Fonction d'appel
    #region Appel
    float sensibilite(string comparatif) //Compare la sensibilité
    {
        //Nécessite un comparatif (electricité +eau = bobo)
        return 1;
    }

	public void SlowWeb()
	{

	}

    public void dps_process(float degat_dps, float duree_dps, float intervalle_dps, string statut_impact, float temps_impact)
    {
        if (delay <= 0)
        {
            delay = duree_dps;
            StartCoroutine(dps(degat_dps, delay, intervalle_dps, statut_impact, temps_impact));
        }
        else if (delay > 0)
        {
            delay = duree_dps;
        }
    }

    //Dégât sur la durée
    public IEnumerator dps(float degat_dps, float duree_dps, float intervalle_dps, string statut_impact, float temps_impact)
    {
        //Si on est pas insensibilisé
        if (statut != statut_impact)
        {
            StartCoroutine(clignote());
            //Le statut temporaire change ex empoisonement
            statut_temporaire = statut_impact;
            //Les dégâts se font sur la duree_dps
            while (duree_dps > 0)
            {

                //On perd de la vie
                life -= degat_dps * sensibilite(statut_impact);

                //On a mal à une certaine fréquence
                yield return new WaitForSeconds(intervalle_dps);
                //On souffre moins longtemps
                duree_dps -= intervalle_dps;
            }
        }

        delay = 0;
    }


    public void impact_process(Transform position_agresseur, float degat_impact, string statut_impact, float temps_impact)
    {
        ShakeManager.instance.LetsShake(true);
        StartCoroutine(impact(position_agresseur,degat_impact, statut_impact, temps_impact));
		SoundManagerEvent.emit(SoundManagerType.DAMAGE);
    }

    //Dégât à l'impact
    public IEnumerator impact(Transform position_agresseur,float degat_impact, string statut_impact, float temps_impact)
    {
		Vector3 dir= (this.transform.position-position_agresseur.position).normalized;
		knockback_direction = dir;


        //Si on est pas insensibilisé
        if (statut != statut_impact || statut_impact=="normal")
        {
            //On perd de la vie à l'impact
            life -= degat_impact * sensibilite(statut_impact);

            //Le statut temporaire deviens le statut à l'impact
            statut_temporaire = statut_impact;

            //Rend invicible brevement 
            StartCoroutine(invincible());

            //Attend que le statut redevienne normal exemple fin de l'empoisonement
            yield return new WaitForSeconds(temps_impact);

            //Le statut redevient "normal"
            statut_temporaire = "normal";
        }
        yield return null;
    }

    //Invincibilité
    IEnumerator invincible()
    {
        StartCoroutine(clignote());
        immortel = true;

        yield return new WaitForSeconds(temps_invincible);

        immortel = false;
        yield return null;
    }


    IEnumerator clignote()
    {
        float temps = temps_invincible;

        float delay = 0.15f;

        while (temps > 0)
        {

            if (sprite.GetComponent<Renderer>().material.color == Color.white)
            {
				if(statut_temporaire=="normal")
				{
					sprite.GetComponent<Renderer>().material.color = Color.red;
				}
				if (statut_temporaire == "toxic")
				{
					sprite.GetComponent<Renderer>().material.color = Color.green;
				}
               
            }
            else
            {
                sprite.GetComponent<Renderer>().material.color = Color.white;
            }

            yield return new WaitForSeconds(delay);

            temps -= delay;
        }

        sprite.GetComponent<Renderer>().material.color = Color.white;
        yield return null;
    }
    #endregion


    //Pièces
    void OnTriggerEnter2D(Collider2D other)
    {
        //SORTIE DE PIECE
        if (other.gameObject.tag == "exit_up")
        {

            up_script = (up_sprite)other.gameObject.GetComponent(typeof(up_sprite));

            if (up_script.open == true)
            {
                /*
                donjon_script.sortie_prise = "up";
                direction="down";*/
                transform.position = spawn_down.transform.position;
                //RoomManager.instance.ChangeRoom();
            }
        }
        if (other.gameObject.tag == "exit_right")
        {
            right_script = (right_sprite)other.gameObject.GetComponent(typeof(right_sprite));
            if (right_script.open == true)
            {
                /*
                donjon_script.sortie_prise = "right";
                direction="left";*/
                transform.position = spawn_left.transform.position;
                //RoomManager.instance.ChangeRoom();
            }
        }
        if (other.gameObject.tag == "exit_down")
        {
            down_script = (down_sprite)other.gameObject.GetComponent(typeof(down_sprite));
            
            if (down_script.open == true)
            {
                /*
                donjon_script.sortie_prise = "down";
                direction="up";*/

                transform.position = spawn_up.transform.position;
                //RoomManager.instance.ChangeRoom();
            }
        }
        if (other.gameObject.tag == "exit_left")
        {
            left_script = (left_sprite)other.gameObject.GetComponent(typeof(left_sprite));

            if (left_script.open == true)
            {
                /*
                donjon_script.sortie_prise = "left";
                direction="right";*/
                transform.position = spawn_right.transform.position;
                //RoomManager.instance.ChangeRoom();
            }
        }
        
    }

    //Récoltes
    void OnTriggerStay2D(Collider2D other)
    {
        if(busy==false)
        {
            if (depecage_en_cours == false)
            {
                recolte_en_cours = true;
                //Contact avec un Vivant 
                if (other.gameObject.tag == "vivant" && other.gameObject!=this.gameObject && other.gameObject!=this.gameObject.transform.parent)
                {
                    vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
					
                    //Récolte possible
                    if (vivant_script.producteur == true && vivant_script.producteur_ok == true && vivant_script.recolte_fait == false)
                    {
						if(ControllerManager.instance.m_XboxMode==true)
						{
							sprite_RB.SetActive(true);
							sprite_E.SetActive(false);
						}
						else
						{
							sprite_RB.SetActive(false);
							sprite_E.SetActive(true);
						}



                        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("A_1"))
                        {
                            //Pour le cadran, on lui défini son temps
                            cadran_script = (Chargement_cadran)cadran_prefab.GetComponent(typeof(Chargement_cadran));
                            cadran_script.timer = vivant_script.temps_recolte;

                            busy = true;
                            if (place_inventaire > 0)
                            {
                                StartCoroutine(recolte(vivant_script.temps_recolte, other.gameObject, other.gameObject.tag));

                                //on lance l'anim de récolte
                                anim.SetTrigger("recolting");
                            }
                        }
                    }

                }
                recolte_en_cours = false;
            }
            if (recolte_en_cours == false)
            {
                depecage_en_cours = true;
                //Contact avec un Vivant 
                if (other.gameObject.tag == "vivant")
                {
                    vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));

                    //Depecage possible
                    if (vivant_script.depecable == true && vivant_script.depecable_fait == false)
                    {

						if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("A_1"))
                        {
                            //Pour le cadran, on lui défini son temps
                            cadran_script = (Chargement_cadran)cadran_prefab.GetComponent(typeof(Chargement_cadran));
                            cadran_script.timer = vivant_script.temps_depecage;
                            

                            busy = true;
                            if (place_inventaire > 0)
                            {
                                StartCoroutine(recolte(vivant_script.temps_recolte, other.gameObject, other.gameObject.tag));
								SoundManagerEvent.emit(SoundManagerType.PLAYER_INTERRACTION );
                                //on lance l'anim de récolte
                                anim.SetTrigger("recolting");
                            }
                        }
                    }

                }
                depecage_en_cours = false;
            }
        }
        
    }

	void OnTriggerExit2D(Collider2D other)
	{
		sprite_E.SetActive(false);
		sprite_RB.SetActive(false);
		
	}

    IEnumerator recolte(float temps, GameObject other, string objet)
    {
		sprite_E.SetActive(false);
		sprite_RB.SetActive(false);
		float life_at_this_time=life;
        if (objet == "vivant" && place_inventaire>0)
        {
			SoundManagerEvent.emit(SoundManagerType.PLAYER_INTERRACTION);
            vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
            cadran = Instantiate(cadran_prefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y +1.5f, this.gameObject.transform.position.z), Quaternion.identity) as GameObject;
            while (temps > 0)
            {

                //Si le joueur presse constament le button de récolte //Si le joueur ne presse aucun autre bouton (sauf visée)
                if ((Input.GetKey(KeyCode.E) || Input.GetButton("A_1"))
                        && !up && !down && !left && !right
                        && Input.GetAxis("TriggersL_1") == 0 && Input.GetAxis("TriggersR_1") == 0
                        && !Input.GetButtonDown("B_1") && !Input.GetButtonDown("X_1") && !Input.GetButtonDown("Y_1")
                        && Input.GetAxis("DPad_XAxis_1") == 0 && Input.GetAxis("DPad_YAxis_1") == 0
                        && !Input.GetButtonDown("Start_1") && !Input.GetButtonDown("Back_1")
						&& life==life_at_this_time
                    )
                {
                    yield return new WaitForSeconds(1);
                    temps -= 1;


                }
                else
                {
                    //le joueur vient d'arreter de récolter, il n'est plus busy
                    busy = false;

                    //on fini l'animation de récolte 
                    anim.SetTrigger("recolting_end");

                    //on détruit le cadran en définissant que le temps est de zéro
                    cadran_script = (Chargement_cadran)cadran.GetComponent(typeof(Chargement_cadran));
                    cadran_script.timer = -1;
                    break;
                }
                //le joueur vient de finir de récolter, il n'est plus busy
                if (temps <= 0)
                {
 
                    //TIMER
                    cadran_script = (Chargement_cadran)cadran.GetComponent(typeof(Chargement_cadran));
                    cadran_script.timer = -1;
                    busy = false;
                    //VIVANT
                    vivant_script.producteur_ok = false;

					SoundManagerEvent.emit(SoundManagerType.PLAYER_PICK);

                    //on fini l'animation de récolte 
                    anim.SetTrigger("recolting_end");
                    Ouverture_sac.instance.letsTremble();
                    bool change = false;

                    for (int i = 0; i < inventaire.Length; i++)   //Pour chaque case de l'inventaire...
                    {
     
                        if (place_inventaire <= 0) //Si l'inventaire est plein
                        {
                            Instantiate(vivant_script.ingredient_recolte[i],vivant_script.transform.position,vivant_script.ingredient_recolte[i].transform.rotation);
                            yield return null;
                        }

                       
                            //NE DEVRAIT JAMAIS ARRIVER
                            if (place_inventaire <= 0)
                            {
                                yield return null;
                            }

                            if (inventaire[i] == null)                       //S'il y a un espace vide
                            {

                                ingredient = vivant_script.ingredient_recolte[0].GetComponent<Ingredient>();
								//ACHIEVEMENT
								/*
								switch(ingredient.name)
								{
									case "I_champignon":
										QuestEventManager.quest_emit(QuestEventManagerType.ADD_MUSH);
										break;

									case "I_venin":
										QuestEventManager.quest_emit(QuestEventManagerType.ADD_VENON);
										break;
								}
								*/
                                ingredient.PlayerPosition = this.transform;
                                ingredient.IsInInventaire = true;
                                ingredient.index_inventaire = i;
                                inventaire[i] = vivant_script.ingredient_recolte[0]; //On met l'ingrédient dedans
                               
                                place_inventaire--;
                                vivant_script.ingredient_recolte[0] = null;
                                change = true;

								

                                break;
                            }
                        

                        if (change == true)
                        {
                            yield return null;
                        }

                    }

                    if (vivant_script.destroy_si_recolte == true)
                    {
                        Destroy(vivant_script.gameObject);
                    }
                }
            }
        }
    }

       

    #region//Déplacements

    void directions()
    {
        
			if (Input.GetKey(KeyCode.UpArrow) || (Input.GetAxis("L_YAxis_1") < 0) || Input.GetKey(KeyCode.Z))
			{
				up = true;
			}
			else
			{
				up = false;
			}
			//
			if (Input.GetKey(KeyCode.DownArrow) || (Input.GetAxis("L_YAxis_1") > 0) || Input.GetKey(KeyCode.S))
			{
				down = true;
			}
			else
			{
				down = false;
			}
			//
			if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("L_XAxis_1") < 0) || Input.GetKey(KeyCode.Q))
			{
				left = true;
			}
			else
			{
				left = false;
			}
			//
			if (Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("L_XAxis_1") > 0) || Input.GetKey(KeyCode.D))
			{
				right = true;
			}
			else
			{
				right = false;
			}
		
    }

    void vecteur()
    {
        //Diagonals
        if (up && left)
        {
            velocity = new Vector3(-1, 1, 0).normalized;
        }
        if (up && right)
        {
			velocity = new Vector3(1, 1, 0).normalized;
        }
        if (down && left)
        {
			velocity = new Vector3(-1, -1, 0).normalized;
        }
        if (down && right)
        {
			velocity = new Vector3(1, -1, 0).normalized;
        }


        //Simple direction
        if(up==true && !left && !right)
        {
            velocity = new Vector3(0,1,0);
        }
        if (down == true && !left && !right)
        {
            velocity = new Vector3(0,-1,0);
        }
        if (left == true && !up && !down)
        {
            velocity = new Vector3(-1,0,0);
        }
        if (right == true && !up && !down)
        {
            velocity = new Vector3(1,0,0);
        }
        

    }

    void acc_des()
    {
        //On ajoute une acceleration
        if (up || down || left || right)
        {
            moveSpeed += acceleration;
            if (moveSpeed >= max_moveSpeed)
            {
                moveSpeed = max_moveSpeed;
            }
        }
        else //ou on descelere
        {
            moveSpeed -= desceleration;
            if (moveSpeed <= 0)
            {
                moveSpeed = 0;
            }
        }
		//Déplacements
		GetComponent<Rigidbody2D>().velocity = velocity * moveSpeed +knockback_direction*knockback_force;
		
		if(knockback_force>0)
		{
			knockback_force -= Time.deltaTime*5;
		}
		else
		{
			knockback_force = 0;
		}

    }


    void animation()
    {
        bool moving;

        
        // Left / Right
        if (velocity.x == 1)
        {
            anim.SetBool("right", true);
        }
        if (velocity.x == -1)
        {
            anim.SetBool("right", false);
        }
      
        //Pas de vitesse
        if(moveSpeed==0)
        {
            move = 0;
            moving = false;
        }else
        {
            moving = true;
        }

        anim.SetBool("moving", moving);
        anim.SetFloat("speedX", velocity.x * moveSpeed);
        anim.SetFloat("speedY", velocity.y * moveSpeed);
        
    }
    #endregion
    

    void FixedUpdate()
    {
        
        //Déplacements
        if (general_time > 0 && life > 0 && CanMove==true) //Si le joueur est en vie
        {
            //On check les inputs pour savoir les directions
            directions();

            //On parametre alors le vecteur velocity
            vecteur();

            //On ajoute de la vitesse par accélération ou descélération
            acc_des();

            //On anim le personnage
            animation();
        }

		if(CanMove==false)
		{
			//Déplacements
			GetComponent<Rigidbody2D>().velocity = velocity * 0;
		}

        
    }
	
    void GoTo_Gameover()
    {
        Application.LoadLevel("Fin_Demo");
    }

}


/* BUTTON
if (Input.GetButtonDown("A_1"))
{
    Debug.Log("A");
}
if (Input.GetButtonDown("B_1"))
{
    Debug.Log("B");
}
if (Input.GetButtonDown("X_1"))
{
    Debug.Log("X");
}
if (Input.GetButtonDown("Y_1"))
{
    Debug.Log("Y");
}
if (Input.GetButtonDown("Start_1"))
{
    Debug.Log("Start");
}
if (Input.GetButtonDown("Back_1"))
{
    Debug.Log("Select");
}
if (Input.GetButtonDown("LB_1"))
{
    Debug.Log("LB");
}
if (Input.GetButtonDown("RB_1"))
{
    Debug.Log("RB");
}

if (Input.GetAxis("DPad_XAxis_1")>0)
{
    Debug.Log("Pad droit");
}
if (Input.GetAxis("DPad_XAxis_1") < 0)
{
    Debug.Log("Pad gauche");
}

if (Input.GetAxis("L_XAxis_1") < 0)
{
    Debug.Log("Stick Gauche Gauche");
}
if (Input.GetAxis("L_XAxis_1") > 0)
{
    Debug.Log("Stick Gauche Droit");
}
if (Input.GetAxis("L_YAxis_1") < 0)
{
    Debug.Log("Stick Gauche Haut");
}
if (Input.GetAxis("L_YAxis_1") > 0)
{
    Debug.Log("Stick Gauche Bas");
}



if (Input.GetAxis("R_XAxis_1") < 0)
{
    Debug.Log("Stick Droit Gauche");
}
if (Input.GetAxis("R_XAxis_1") > 0)
{
    Debug.Log("Stick Droit Droit");
}
if (Input.GetAxis("R_YAxis_1") < 0)
{
    Debug.Log("Stick Droit Haut");
}
if (Input.GetAxis("R_YAxis_1") > 0)
{
    Debug.Log("Stick Droit Bas");
}
 
Debug.Log("Gachette Droite "+Input.GetAxis("TriggersR_1"));

Debug.Log("Gachette Gauche " + Input.GetAxis("TriggersL_1"));

*/