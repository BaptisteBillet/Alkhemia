using UnityEngine;
using System.Collections;

public class Planche_animation : MonoBehaviour
{

    bool ouvert;
    Animator anim;

    void Start()
    {
        ouvert = false;
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Back_1") || Input.GetKeyDown(KeyCode.R))
        {
            ouvert = !ouvert;

            anim.SetBool("sac_ouvert", ouvert);
        }

       

    }
}
