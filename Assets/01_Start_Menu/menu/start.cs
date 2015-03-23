using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0))
        {
            Application.LoadLevel("Game");
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("Game");
        }
        
        if (Input.GetButtonDown("A_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("B_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("X_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("Y_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("Start_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("Back_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("LB_1"))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetButtonDown("RB_1"))
        {
            Application.LoadLevel("Game");
        }

       
	}
}
