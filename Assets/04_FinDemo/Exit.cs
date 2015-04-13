using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0))
        {
			Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
			Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
			Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
			Application.Quit();
        }
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			Application.Quit();
		}
        
        if (Input.GetButtonDown("A_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("B_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("X_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("Y_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("Start_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("Back_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("LB_1"))
        {
			Application.Quit();
        }
        if (Input.GetButtonDown("RB_1"))
        {
			Application.Quit();
        }

		if (Input.GetMouseButtonDown(0))
			Application.Quit();
		if (Input.GetMouseButtonDown(1))
			Application.Quit();

		if (Input.GetMouseButtonDown(2))
			Application.Quit();

       
	}
	
}
