using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public string name;         //Nom de la bullet

    public string statut_bullet;       //Statut de la potion         
    public float temps_statut; //Temps avant que la cible ne soit plus empoisonné, electrifié, en feu... 

    public int impact_degat;     //Dégât à l'impact
    public bool impact_mort;     //Si la bullet se détruit à l'impact

    public bool duree_mort;      //Si la bullet se détruit avec le temps
    public float duree_temps;      //Durée de vie de la bullet
    public int duree_degat;      //Dégât sur la durée

    public float duree_couldown;   //Nombre de seconde durant lesquels les dégâts sur la durée se font.

    public float duree_intervalle; //Dégât tout les combien?

    public Vivant vivant_script;        //L'objet vivant que l'on touche
    protected Player player_script;        //L'objet player que l'on touche

    public GameObject source_degat;

    void Start()
    {
        if (duree_mort == true)
        {
            Destroy(this.gameObject, duree_temps);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject != null)
        {

            //DEGAT PLAYER
            if (other.gameObject.tag == "Player" && this.gameObject != transform.parent.parent && other.gameObject != null) //Si l'on touche le player et que ce n'est pas notre parent
            {

                player_script = (Player)other.gameObject.GetComponent(typeof(Player));
                if (player_script.immortel == false) //SI le vivant n'est pas immortel
                {
                    player_script.impact_process(impact_degat, statut_bullet, temps_statut); //Dégât à l'impact, changement de statut sur une durée

                    if (player_script.statut != statut_bullet && duree_degat > 0) //Si il n'est pas immunisé
                    {
                        player_script.dps_process(duree_degat, duree_temps, duree_intervalle, statut_bullet, temps_statut);
                    }

                    if (impact_mort == true)
                    {
                        Destroy(this.gameObject);
                    }
                }

            }
            
            //Dégât VIVANT
            if (other.gameObject.tag == "vivant" && this.gameObject != transform.parent.parent) //Si l'on touche un vivant et que ce n'est pas notre parent
            {

                vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));

                if (vivant_script.immortel == false && other.gameObject != null) //SI le vivant n'est pas immortel
                {

                    vivant_script.impact_process(impact_degat, statut_bullet, temps_statut); //Dégât à l'impact, changement de statut sur une durée

                    if (vivant_script.statut != statut_bullet && duree_degat > 0) //Si il n'est pas immunisé
                    {
                        vivant_script.dps_process(duree_degat, duree_temps, duree_intervalle, statut_bullet, temps_statut);
                    }

                    if (impact_mort == true)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }

    }


    void OnCollisionEnter2D(Collision2D other)
    {

        //DEGAT PLAYER
        if (other.gameObject.tag == "Player" && this.gameObject != transform.parent.parent && other.gameObject != null) //Si l'on touche le player et que ce n'est pas notre parent
        {

            player_script = (Player)other.gameObject.GetComponent(typeof(Player));
            if (player_script.immortel == false) //SI le vivant n'est pas immortel
            {
                player_script.impact_process(impact_degat, statut_bullet, temps_statut); //Dégât à l'impact, changement de statut sur une durée

                if (player_script.statut != statut_bullet && duree_degat>0) //Si il n'est pas immunisé
                {
                    player_script.dps_process(duree_degat, duree_temps, duree_intervalle, statut_bullet,temps_statut);
                }

                if (impact_mort == true)
                {
                    Destroy(this.gameObject);
                }
            }

        }

        //Dégât VIVANT
        if (other.gameObject.tag == "vivant" && this.gameObject != transform.parent.parent && other.gameObject != null) //Si l'on touche un vivant et que ce n'est pas notre parent
        {

            vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
            if (vivant_script.immortel == false && other.gameObject != null) //SI le vivant n'est pas immortel
            {
                vivant_script.impact_process(impact_degat, statut_bullet, temps_statut); //Dégât à l'impact, changement de statut sur une durée

                if (vivant_script.statut != statut_bullet && duree_degat > 0) //Si il n'est pas immunisé
                {
                    vivant_script.dps_process(duree_degat, duree_temps, duree_intervalle, statut_bullet, temps_statut);
                }

                if (impact_mort == true)
                {
                    Destroy(this.gameObject);
                }
            }

        }

        if (other.gameObject.tag == "mur")
        {
            Destroy(this.gameObject);
        }
        


    }

}
