using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public string source;

    private Player player_script;
    private Vivant vivant_script;

    public int degats=2;
    

    // Use this for initialization
    void Start()
    {
        ShakeManager.instance.LetsShake(false,300);
        Destroy(this.gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //#CAC

        if (other.tag == "Player")
        {

           
                player_script = (Player)other.gameObject.GetComponent(typeof(Player));

                player_script.impact_process(this.transform,degats, "feu", 2);
            
        }

        if (other.tag == "vivant")
        {

                vivant_script = (Vivant)other.gameObject.GetComponent(typeof(Vivant));
                vivant_script.impact_process(this.transform,degats, "feu", 2);

        }

    }

    



}
