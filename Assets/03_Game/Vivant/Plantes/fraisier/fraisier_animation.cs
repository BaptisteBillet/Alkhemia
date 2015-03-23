using UnityEngine;
using System.Collections;

public class fraisier_animation : MonoBehaviour {

    public Vivant vivant_script;

    Animator anim;
	// Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("plein", vivant_script.producteur_ok);

	}
}
