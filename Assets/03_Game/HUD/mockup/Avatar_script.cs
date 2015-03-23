using UnityEngine;
using System.Collections;

public class Avatar_script : MonoBehaviour {

    public GameObject player;
    private Player player_script;

    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        player_script = (Player)player.GetComponent(typeof(Player));
        

    }

    // Update is called once per frame
    void Update()
    {


        if (player_script.statut_temporaire == "feu")
        {
            sprite.color = new Color32(217, 115, 16, 255);
        }
        if (player_script.statut_temporaire == "toxic")
        {
            sprite.color = new Color32(95, 124, 71, 255);
        }
        if (player_script.statut_temporaire == "normal")
        {
            sprite.color = Color.white;
        }


    }
}
