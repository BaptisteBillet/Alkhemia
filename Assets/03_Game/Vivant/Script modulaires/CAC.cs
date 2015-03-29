using UnityEngine;
using System.Collections;

public class CAC : MonoBehaviour {

    private Player player_script;
    private Vivant vivant_script;

    public Vivant main_script;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        //#CAC
       
            if (other.tag == "Player")
            {
                
                if (main_script.degat_impact > 0)
                {

                    player_script = (Player)other.gameObject.GetComponent(typeof(Player));
                    
                    player_script.impact_process(this.transform,main_script.degat_impact, main_script.statut, main_script.temps_statut);
                }
            }

            if (other.tag == "vivant")
            {
                if (main_script.degat_impact > 0)
                {
                    vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
					vivant_script.impact_process(this.transform,main_script.degat_impact, main_script.statut, main_script.temps_statut);
                }
            }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //#CAC

            if (other.tag == "Player")
            {
                if (main_script.dps_degat > 0)
                {
                    player_script = (Player)other.gameObject.GetComponent(typeof(Player));
                    player_script.dps_process(main_script.dps_degat, main_script.dps_duree, main_script.dps_intervalle, main_script.statut, main_script.temps_statut);
                }
            }

            if (other.tag == "vivant")
            {
                if (main_script.dps_degat > 0)
                {
                    vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
                    vivant_script.dps_process(main_script.dps_degat, main_script.dps_duree, main_script.dps_intervalle, main_script.statut, main_script.temps_statut);
                }
            }
        
    }

    void Update()
    {

        if(main_script.life<=0)
        {
            Destroy(this);
        }

    }

}
