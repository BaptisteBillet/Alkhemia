using UnityEngine;
using System.Collections;

public class left_sprite : MonoBehaviour {

    public Room piece_left;
    public bool open;
	// Use this for initialization

    public GameObject blocage;

    SpriteRenderer sprite;
	void Start () 
    {
        //piece_left.gameObject.GetComponent<Room>();
        sprite = GetComponent<SpriteRenderer>();
        Debug.Log(piece_left);
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (piece_left.left_open == false)
        {
            Debug.Log("a");
            open = false;
            blocage.SetActive(true);
           
        }
        else
        {
            Debug.Log("b");
            open = true;
            blocage.SetActive(false);
        }
        
	}
}
