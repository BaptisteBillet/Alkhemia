using UnityEngine;
using System.Collections;

public class right_sprite : MonoBehaviour {

    public Room piece_right;
    public bool open;

    public GameObject blocage;
	// Use this for initialization
	void Start () 
    {
        piece_right.gameObject.GetComponent<Room>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (piece_right.right_open == false)
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
