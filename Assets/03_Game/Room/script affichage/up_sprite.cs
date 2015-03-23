using UnityEngine;
using System.Collections;

public class up_sprite : MonoBehaviour {

    public Room piece_up;

    public bool open;
	// Use this for initialization

    public GameObject blocage;
	void Start () 
    {
        piece_up.gameObject.GetComponent<Room>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (piece_up.up_open == false)
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
