using UnityEngine;
using System.Collections;

public class Chargement_cadran : MonoBehaviour {

    public Transform aiguille;
    public float timer;
    private float rotation_frame;
    private bool animation_break;

    private bool stop;

    //Animation
    Animator anim;

    void Start()
    {
        animation_break = false;
        stop = false;
        //Animation
        anim = GetComponent<Animator>();

        rotation_frame = 360/timer;

        StartCoroutine(delai());
    }
    
    IEnumerator delai()
    {
        yield return new WaitForSeconds(timer);
        stop = true;
        animation_break = true;
        anim.SetBool("break", animation_break);
        Destroy(this.gameObject, 0.5f);
    }

   
	// Update is called once per frame
	void Update () 
    {

      if(stop==false && (Input.GetKey(KeyCode.E) || Input.GetButton("LB_1") || Input.GetButton("RB_1")))
      {
          aiguille.Rotate(Vector3.forward * -rotation_frame * Time.deltaTime);
      }
       
        if(timer<=0)
        {
            stop = true;
            animation_break = true;
            anim.SetBool("break", animation_break);
            Destroy(this.gameObject, 0.5f);


        }
	}

}
