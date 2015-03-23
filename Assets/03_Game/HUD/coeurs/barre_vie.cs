using UnityEngine;
using System.Collections;

public class barre_vie : MonoBehaviour {

    public GameObject player;
    private Player player_script;

    private float pv_max;
    public float pv_actuel; //pourcentage
    private float pixel = 0.0208f;

    private SpriteRenderer sprite;

    private Color red;
    private Color orange;
    private Color violet;

	// Use this for initialization
	void Start () {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        player_script = (Player)player.GetComponent(typeof(Player));
        pv_max = player_script.life_max;
        pv_actuel = (player_script.life*100)/pv_max;

	}
	
	// Update is called once per frame
	void Update () 
    {
        pv_actuel = (player_script.life * 100) / pv_max;

        this.gameObject.transform.localPosition= new Vector3((pv_actuel*pixel)-2.08f,0,0);

        if(player_script.statut_temporaire=="feu")
        {
            sprite.color = new Color32(225,128,48,255) ;
        }
        if (player_script.statut_temporaire == "toxic")
        {
            sprite.color = new Color32(95, 124, 71, 255);
        }
        if (player_script.statut_temporaire == "normal")
        {
            sprite.color = new Color32(202 , 65, 53, 255);
        }


	}
}
