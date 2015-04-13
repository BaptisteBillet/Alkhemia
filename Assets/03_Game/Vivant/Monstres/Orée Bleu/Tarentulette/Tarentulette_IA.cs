using UnityEngine;
using System.Collections;

public class Tarentulette_IA : MonoBehaviour {

    public GameObject main;
    public Animator anim;
    private Tireur tireur_script;

    public GameObject destination_go;
    private Destination destination_script;

    public GameObject detection_go;
    private Detection detection_script;

    //BLOQUEUR
    public GameObject bloqueur;
    public GameObject position;

    //MEMBERs
    public bool ennemi_detected;
    public GameObject cible;

    public bool Isbloc;
    public bool IsHunting;

    public float distance;

    public bool mirror;

    public bool IsShooting;

	public Vivant vivant;

    void Start()
    {
        //INITIALISATION
        destination_script = destination_go.GetComponent<Destination>();
        detection_script = detection_go.GetComponent<Detection>();
        tireur_script = this.gameObject.GetComponent<Tireur>();
    }

    void Update()
    {
		

        if(IsHunting==false)
        {
			if (vivant.agresseur != null)
			{
				detection_script.cible = vivant.agresseur;
			}
			else
			{
				cible = null;
				ennemi_detected = false;
			}


            if (detection_script.cible != null)
            {
                cible = detection_script.cible;
                ennemi_detected = true;
                Depart();
            }
            else
            {
                cible = null;
                ennemi_detected = false;
            }
        }
        else
        {
            //Si le player est loin
            if (Vector3.Distance(this.transform.position, cible.transform.position) > distance)
            {
				tireur_script.IsAllowToShoot = false;
				anim.ResetTrigger("shoot");
				if(IsShooting==false)
				{
					destination_script.cible = cible.transform;
					anim.SetBool("moving", true);
				}
               
            }
            else 
            {
                destination_script.cible = null;
                tireur_script.IsAllowToShoot = true;
				IsShooting = true;
                anim.SetTrigger("shoot");
            }

        }

        //Mirror
        
            if (detection_script.cible != null)
            {
                if (cible.transform.position.x > this.transform.position.x)
                {
                    mirror = true;
                    anim.SetBool("mirror", mirror);
                }
                else
                {
                    mirror = false;
                    anim.SetBool("mirror", mirror);
                }
            }
        


       
    }

    void Depart()
    {
        if(Isbloc==false)
        {
            Isbloc = true;
            anim.SetTrigger("bloc");
			SoundManagerEvent.emit(SoundManagerType.FIGHT);
        }
    }

    public void BlockPosition()
    {
        position = Instantiate(bloqueur, position.transform.position, bloqueur.transform.rotation) as GameObject;
        position.transform.parent = this.gameObject.transform.parent.parent;

        IsHunting = true;
    }


   





}
