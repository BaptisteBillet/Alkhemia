using UnityEngine;
using System.Collections;

public class down_sprite : MonoBehaviour {

    public Room piece_down;
    public bool open;
	// Use this for initialization

    public GameObject blocage;
	void Start () 
    {
        piece_down.gameObject.GetComponent<Room>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (piece_down.down_open == false)
        {
            open = false;
            blocage.SetActive(true);
        }
        else
        {
            open = true;
            blocage.SetActive(false);
        }
        
	}
}
