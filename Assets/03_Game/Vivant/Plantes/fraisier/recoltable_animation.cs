using UnityEngine;
using System.Collections;

public class recoltable_animation : MonoBehaviour {

    private Vivant vivant_script;

    Animator anim;
	// Use this for initialization
    void Start()
    {
        vivant_script = GetComponent<Vivant>();
        anim = this.gameObject.GetComponent<Animator>();
    }
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("plein", vivant_script.producteur_ok);

	}
}
