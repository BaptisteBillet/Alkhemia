using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//ESSAI STRUCT


public class Vivant : MonoBehaviour {

    //BASE

    public string categorie; // plante_agressive plante_passive , monstre_agressif monstre_passif

    public string statut;   //normal //feu //toxic
    public string statut_temporaire;

    public float life;        //Points de vie
    public bool immortel;   //Est immortel
    public bool non_tuable;
    public bool disparait_si_mort;
    
    //RECOLTE
    
    public bool producteur;     //Est ce que l'objet est récoltable sans être tué? Arbre ou mouton typiquement
    public bool producteur_ok;  //Est ce que la production est prête?
    public bool recolte_fait;   //Est ce que la recolte est faite
    public bool destroy_si_recolte; //Détruit quand récolté
    public float temps_recolte;   //Temps nécessaire la récolte

    public bool recolte_vent;

    //Ingrédient recolte
    public GameObject[] ingredient_recolte = new GameObject[5];

    //Ingrédient depecable
    public GameObject[] ingredient_depecable = new GameObject[5];

    //RECOLTE SI TUE
    public bool depecable;      //Est ce que l'objet est récoltable si tué
    public bool depecable_fait; //Est ce que c'est depecé?
    public float temps_depecage; //Le temps nécessaire au dépecage

    private Player player_script;
    private Vivant vivant_script;
    private Bullet bullet_script;

    private float temps_invincible=1;

    //Dégat contact
    //public bool contact_dangereux;  //Le vivant fait des dégâts au contact
    public int degat_impact;        //Le nombre de dégat fait au contact
    public float temps_statut;      //Le nombre de temps ou le statut est appliqué

    //Dégat durée
    //public bool contact_dps;  //Le vivant fait des dégâts au contact
    public int dps_degat;        //Le nombre de dégat fait au contact à chaque intervalle
    public int dps_intervalle;  //L'intervalle de dégât (Tout les combiens de temps subira-t-il des dégats?
    public int dps_duree;        //Le temps de dps (combien de temps le contacts sera empoisoner.)

    //Sprite 
    public SpriteRenderer sprite;
    public bool volumique; //Est ce que l'objet est solide ou est ce que le joueur marche librement dessus

    public GameObject agresseur;

    public GameObject parent;

    public Animator anim;


    void OnEnable()
    {
        temps_invincible = 0.4f;

        temps_statut = 3;

        if(sprite==null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
 
        if (volumique == true && sprite!=null)
        {
            sprite.sortingOrder = (int)this.gameObject.transform.position.y * 10 * -1;
        }
        else if (sprite != null)
        {
            sprite.sortingLayerName = "Sol";
            sprite.sortingOrder = (int)-1;
        }
    }


    //Fonction d'appel
    #region Appel
    float sensibilite(string comparatif) //Compare la sensibilité
    {
        //Nécessite un comparatif (electricité +eau = bobo)
        return 1;
    }


    public void dps_process(float degat_dps, float duree_dps, float intervalle_dps, string statut_impact, float temps_impact)
    {
        if (non_tuable == false)
        {
            StartCoroutine(dps(degat_dps, duree_dps, intervalle_dps, statut_impact, temps_impact));
        }
    }

    //Dégât sur la durée
    public IEnumerator dps(float degat_dps, float duree_dps, float intervalle_dps, string statut_impact, float temps_impact)
    {
        //Si on est pas insensibilisé
        if (statut != statut_impact)
        {

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

                if (life <= 0 && disparait_si_mort == true)
                {

                    Destroy(parent);
                }
                if (life <= 0 && disparait_si_mort == false && anim!=null)
                {
                    degat_impact = 0;
                    anim.SetTrigger("dies");
                }


            }
        }

    }

	public void impact_process(Transform position_agresseur, float degat_impact, string statut_impact, float temps_impact)
    {
        if (non_tuable == false)
        {
            StartCoroutine(impact(degat_impact, statut_impact, temps_impact));
        }
    }

    //Dégât à l'impact
    public IEnumerator impact(float degat_impact, string statut_impact, float temps_impact)
    {
         //Si on est pas insensibilisé
        if (statut != statut_impact)
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
            statut_temporaire = statut;

            if(life<=0 && disparait_si_mort==true)
            {
               
                Destroy(parent);
            }


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
            
            if (sprite.material.color == Color.white)
            {
                sprite.material.color = Color.red;
            }
            else
            {
                sprite.material.color = Color.white;
            }

            yield return new WaitForSeconds(delay);

            temps -= delay;
        }

        sprite.material.color = Color.white;
        yield return null;
    }


    #endregion


    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.tag == "vivant" || col.tag == "Player")
        {
            agresseur = col.gameObject;
        }
        
        if(col.tag=="bullet")
        {
            //Un niveau
			if (col.transform.parent)
			{
				if (col.gameObject.transform.parent.gameObject.tag == "vivant")
				{
					agresseur = col.gameObject.transform.parent.gameObject;
				}

				if (col.gameObject.transform.parent.gameObject.tag == "Player")
				{
					agresseur = col.gameObject.transform.parent.gameObject;
				}

				if (col.transform.parent.parent)
				{
					//Deux niveaux
					if (col.gameObject.transform.parent.parent.gameObject.tag == "vivant")
					{
						agresseur = col.gameObject.transform.parent.parent.gameObject;
					}

					if (col.gameObject.transform.parent.parent.gameObject.tag == "Player")
					{
						agresseur = col.gameObject.transform.parent.parent.gameObject;
					}
				}
			}
			
        }
        
        if (recolte_vent == true)
        {
            if (col.gameObject.tag == "bullet")
            {
                bullet_script = col.gameObject.GetComponent<Bullet>();
                if (bullet_script.name == "bullet_vent")
                {
                    if (recolte_fait == false && producteur_ok == true)
                    {
                        recolte_fait = true;
                        producteur_ok = false;
                        if (destroy_si_recolte == true)
                        {
                            Destroy(this.gameObject);
                        }

                        Instantiate(ingredient_recolte[0], this.gameObject.transform.position, ingredient_recolte[0].transform.rotation);
                    }
                }
            }
        }
    }
   
}
