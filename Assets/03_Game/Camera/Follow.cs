using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject player;
    private Player player_script;

    private Vector3 position_base = new Vector3();

	// Use this for initialization
	void Start () 
    {
        //player.gameObject.GetComponent<Player>();
        player_script = (Player)player.GetComponent(typeof(Player));
        position_base = transform.position;

	}
	
	// Update is called once per frame
	void Update () 
    {
	
        if(player_script.big_room==true)
        {
            transform.position = player.transform.position;
        }
        else
        {
            transform.position = position_base;
        }

	}
}
