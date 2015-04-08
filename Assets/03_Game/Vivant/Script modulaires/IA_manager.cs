using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class IA_manager : MonoBehaviour {


    private Vivant vivant_script;

    // Scripts d'accès
        //Etape 1 Déplacement 
    public GameObject objet_deplacement;

    private Balade Balade_script;
    private Destination Destination_script;
    private Patrouille Patrouille_script;
    private Fuis Fuis_script;

        //Etape 2 Détection
    public GameObject Detection_object;
    private Detection Detection_script;

        //Etape 3 Action


        //Etape 4 Autres spécificités
    private CAC CAC_script;
    public Tireur Tireur_script;



    /////////////////////////////////////

    ///
    private bool IA_Balade;
    private bool IA_Destination;
    private bool IA_Patrouille;

    private bool IA_Detection;

    private bool IA_CAC;
    private bool IA_Tireur;

    private bool IA_Fuis;

    //Permet de savoir le deplacement de base
    public string module_deplacement;

    //Réaction de fuite en cas de contact ou douleur
    public bool reaction_contact_fuite;

    //Réaction de fuite en cas de contact ou douleur
    public bool reaction_contact_agressif;

    //Agressif donc attaque CAC ou Tireur
    public bool agressif;

    //Acces animator
    //Source sprite

    //Sprite
    SpriteRenderer sprite;

    //Animation
    Animator anim;



	// Use this for initialization
    void OnEnable() 
    {

        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        sprite.sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }



        vivant_script = (Vivant)this.gameObject.GetComponent(typeof(Vivant));

        //Mise à 0 des boolean
        IA_Balade = false;
        IA_Detection = false;
        IA_Patrouille = false;
        IA_Destination = false;
        IA_CAC = false;
        IA_Tireur = false;
        IA_Fuis = false;

        //On gére les modules de déplacements
        if (objet_deplacement != null)
        {
            Balade_script = (Balade)objet_deplacement.GetComponent(typeof(Balade));
            //Balade
            if (Balade_script != null)
            {
                IA_Balade = true;
            }

            Destination_script = (Destination)objet_deplacement.GetComponent(typeof(Destination));
            //Destination
            if (Destination_script != null)
            {
                
                IA_Destination = true;
            }

            Patrouille_script = (Patrouille)objet_deplacement.GetComponent(typeof(Patrouille));
            //Patrouille
            if (Patrouille_script != null)
            {
                
               IA_Patrouille = true;
            }

            Fuis_script = (Fuis)objet_deplacement.GetComponent(typeof(Fuis));
            //Fuite
            if (Fuis_script != null)
            {

                IA_Fuis = true;
            }
			

        }

        //Detection
        Detection_script = (Detection)Detection_object.GetComponent(typeof(Detection)); 
        if (Detection_script != null)
        {
            IA_Detection = true;
        }

        //CAC
        CAC_script = (CAC)this.gameObject.GetComponent(typeof(CAC));
        if (CAC_script != null)
        {
            IA_CAC = true;
        }

		//TIREUR
		if (Tireur_script != null)
		{
			IA_Tireur = true;
		}

        //On indique le module de déplacement de base
        modules_deplacements(module_deplacement);



	}
    

    void modules_deplacements(string deplacement) //balade destination patrouille
    {
        
       // rigidbody2D.velocity *= 0;
        switch(deplacement)
        {
            case "balade":
                if (IA_Balade == true)        
                {
                    Balade_script.enabled = true;
                }
                if (IA_Destination == true)        
                {
                    Destination_script.cible = null;
                    Destination_script.enabled = false;
                }
                if(IA_Patrouille==true)
                {
                    Patrouille_script.enabled = false;
                }
                if (IA_Fuis == true)
                {
                    Fuis_script.enabled = false;
                }
                anim.SetBool("enemy_detected", false);
                break;

            case "destination":
                if (IA_Balade == true)        
                {
                    Balade_script.enabled = false;
                }
                if (IA_Destination == true)        
                {
                    Destination_script.cible = Detection_script.cible.transform;
                    Destination_script.enabled = true;
                }
                if(IA_Patrouille==true)
                {
                    Patrouille_script.enabled = false;
                }
                if (IA_Fuis == true)
                {
                    Fuis_script.enabled = false;
                }
                anim.SetBool("enemy_detected", true);
                break;


            case "patrouille":
                 if (IA_Balade == true)        
                {
                    Balade_script.enabled = false;
                }
                if (IA_Destination == true)        
                {
                    Destination_script.cible = null;
                    Destination_script.enabled = false;
                }
                if(IA_Patrouille==true)
                {
                    Patrouille_script.enabled = true;
                }
                if (IA_Fuis == true)
                {
                    Fuis_script.enabled = false;
                }
                anim.SetBool("enemy_detected", false);
                break;

            case "fuite":
                if (IA_Balade == true)        
                {
                    Balade_script.enabled = false;
                }
                if (IA_Destination == true)        
                {
                    Destination_script.cible = null;
                    Destination_script.enabled = false;
                }
                if(IA_Patrouille==true)
                {
                    Patrouille_script.enabled = false;
                }
                if (IA_Fuis == true)
                {
                    if (Detection_script.cible != null)
                    {

                        //Fuis_script.cible = vivant_script.agresseur.transform;
                        Fuis_script.cible = Detection_script.cible.transform;

                        Fuis_script.enabled = true;
                    }
                    else
                    {
                        
                        Fuis_script.cible = vivant_script.agresseur.transform;
                        Fuis_script.enabled = true;
                    }
                }
                anim.SetBool("enemy_detected", true);
                break;
            case "stop":
                if (IA_Balade == true)
                {
                    Balade_script.GetComponent<Rigidbody2D>().velocity *= 0;
                    Balade_script.enabled = false;

                }
                if (IA_Destination == true)
                {
                    Destination_script.cible = null;
                    Destination_script.enabled = false;
                }
                if (IA_Patrouille == true)
                {
                    Patrouille_script.enabled = true;
                }
                if (IA_Fuis == true)
                {
                    Fuis_script.enabled = false;
                }
                break;

           
        }
    }

    void resolution_detection()
    {
       
       if(Detection_script.cible!=null) //Si quelquechose est trouvé
       {
           if (agressif == true)            //Si le sujet est agressif
           {
               if (IA_CAC == true)          //Si le sujet attaque au corps à corps
               {
                   if (Destination_script == true)
                   {
                       modules_deplacements("destination");
                   }
               }

               if(IA_Tireur==true)
               {
                   if (Destination_script == true)
                   {
                       modules_deplacements("stop");
                   }

                   Tireur_script.enabled = true;
                   
               }
           }
           else
           {
               if(IA_Fuis==true)
               {
                   modules_deplacements("fuite");
               }

           }
       }
       else if(Detection_script.cible==null)    //Si rien n'est trouvé
       {
           if (IA_Tireur == true)
           {
               Tireur_script.enabled = false;
           }
           if(Balade_script==true)              //Si le comportement normal c'est la balade
           {
               modules_deplacements("balade");
           }
           if (Patrouille_script == true)       //Si le comportement normal c'est la patrouille
           {
               modules_deplacements("patrouille");
           }
          
       }

       if (reaction_contact_fuite == true)            //Si il y a contact, est-ce que le sujet fuit?
       {
           if(vivant_script.agresseur!=null)
           {
               if (IA_Balade == true)
               {
                   Balade_script.enabled = false;
               }
               if (IA_Destination == true)
               {
                   Destination_script.cible = null;
                   Destination_script.enabled = false;
               }
               if (IA_Patrouille == true)
               {
                   Patrouille_script.enabled = false;
               }
               if (IA_Fuis == true)
               {
                   Fuis_script.source_sprite = vivant_script.agresseur;
                   Fuis_script.cible = vivant_script.agresseur.transform;
                   Fuis_script.enabled = true;
               }
           }
       }

       if (reaction_contact_agressif == true)            //Si il y a contact, est-ce que le sujet fuit?
       {
           if (vivant_script.agresseur != null)
           {
               modules_deplacements("destination");
           }
       }
    }

    void Update()
    {
        if (vivant_script.life > 0)
        {
            if(IA_Detection==true) //Si l'IA est censé détécter quelquechose
            {
                resolution_detection();
            }
            sprite.sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;

        }
        if (vivant_script.life <= 0)
        {
            modules_deplacements("stop");
        }
    }
}
