using UnityEngine;
using System.Collections;

public class bullet_toxic : Bullet {


    public GameObject explosion_prefab;
    private GameObject explosion;
    private Explosion explosion_script;

    private Bullet bullet_script;



    //private Player player_script;

    void OnTriggerEnter2D(Collider2D other)
    {
		
        if(other.gameObject.tag=="Player")
        {
			player_script = (Player)other.gameObject.GetComponent(typeof(Player));
			if (player_script.immortel == false) //SI le vivant n'est pas immortel
			{
				player_script.impact_process(this.transform, impact_degat, statut_bullet, temps_statut); //Dégât à l'impact, changement de statut sur une durée

				if (player_script.statut != statut_bullet && duree_degat > 0) //Si il n'est pas immunisé
				{
					player_script.dps_process(duree_degat, duree_temps, duree_intervalle, statut_bullet, temps_statut);
				}

				if (impact_mort == true)
				{
					
					Destroy(this.gameObject);
				}
			}

			

            //DEGAT PLAYER
            
        }

        if (other.gameObject.tag == "vivant") //Si l'on touche un vivant et que ce n'est pas notre parent
        {

			if (this.gameObject.transform.parent)
			{
                if (this.gameObject != transform.parent.parent || this.gameObject != transform.parent)
				{
					vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
                   
					if (vivant_script.immortel == false && other.gameObject != null) //SI le vivant n'est pas immortel
					{
						
						vivant_script.impact_process(this.transform, impact_degat, statut_bullet, temps_statut); //Dégât à l'impact, changement de statut sur une durée

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
		

        if (other.gameObject.tag == "bullet")
        {

			if (other.name == "bullet_feu(Clone)")
            {
                Destroy(other.gameObject);
				//GameObject Parent = this.gameObject.transform.parent.gameObject;
				//Destroy(Parent);
				//Passage Jérôme, incompréhension, perxplexitué
                explosion = Instantiate(explosion_prefab) as GameObject; //Instantiation
                explosion.transform.position = this.gameObject.transform.position;

            }
        }
        if (other.gameObject.tag == "explosion")
        {
            Destroy(this.gameObject);
            explosion_script = (Explosion)explosion_prefab.GetComponent(typeof(Explosion));
            explosion_script.source = "toxic";
            explosion = Instantiate(explosion_prefab) as GameObject; //Instantiation
            explosion.transform.position = this.gameObject.transform.position;
        }
		/*
        if (other.gameObject.tag == "mur")
		{
			Debug.Log("ATTENTION MUR");
            Destroy(this.gameObject);
        }*/
        

    }

   
}
