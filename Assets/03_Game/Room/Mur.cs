using UnityEngine;
using System.Collections;

public class Mur : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
