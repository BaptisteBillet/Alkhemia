using UnityEngine;
using System.Collections;

public class left_sprite : MonoBehaviour {

    public Room piece_left;
    public bool open;
	// Use this for initialization

    public GameObject blocage;
	void Start () 
    {
        piece_left.gameObject.GetComponent<Room>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (piece_left.left_open == false)
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
