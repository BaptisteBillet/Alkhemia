using UnityEngine;
using System.Collections;

public class PorteScript : MonoBehaviour {

    

    void OnTriggerEnter2D(Collider2D other)
    {
        Application.LoadLevel("Labo");
    }

}
