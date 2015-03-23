using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detection : MonoBehaviour
{

    //Accès au IA_Manager
    //public GameObject prioritaire;
    public GameObject cible;
    private GameObject nouvelle_cible;
    private string last_statut;
    //categorie recherché
    [System.Serializable]
    public struct recherche
    {
        public bool exclu_cette_recherche;
        public int ordre;
        public string nom;
        public string tag;
        public string categorie;
        public string statut;
    }

    public recherche[] recherches;

    private Player player_script;
    private Vivant vivant_script;

    public GameObject[] exception;

    //Liste de détecté
    public List<GameObject> detecte = new List<GameObject>();
    //Liste de cible_probable
    public List<GameObject> cible_probable = new List<GameObject>();

    private float test;
    private float test_cible;

    void OnTriggerEnter2D(Collider2D other)
    {
        verification(other.gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        actualise(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enleve(other.gameObject);
    }

    void actualise(GameObject other)
    {

        if (cible_probable.Count > 0) //S'il y a des cibles probables
        {
            for (int i = 0; i < cible_probable.Count; i++) //Il cherche dans les cibles probables
            {
                if (cible_probable[i] == other)             //Si la cible actuel est déjà dans cible probable
                {
                    cible_probable.RemoveAt(i);             //Il l'efface (il la reprend après)
                }

            }
        }
        else //Sinon il n'y a pas de cible probable
        {
            cible = null;
        }

        if (detecte.Contains(other))
        {

            foreach (recherche cas in recherches) //Pour toutes les recherches mise en place
            {

                if (cas.statut != "") //S'il y a un cas
                {
                    if ((verfi_statut(cas, other)) == true) //S'il y en a un qui a le bon statut
                    {
                        cible_probable.Add(other);      //Si cet objet est compatible il le met (ou remet) dans la liste des probables
                    }
                }

                else                    //Si il n'y a pas de statut dans la recherche
                {
                    for (int i = 0; i < cible_probable.Count; i++)  //Il fait le tour de la liste
                    {
                        if (cible_probable[i] == other)             //Si l'objet est dans la liste
                        {
                            cible_probable.RemoveAt(i);             //Il l'enleve
                        }
                    }
                    cible_probable.Add(other);                      //Et il la remet
                }

            }
            prioritaire();
        }


        if (!cible_probable.Contains(cible))                     //Si la cible actuel n'est plus dans la liste, ce n'est plus la cible
        {
            cible = null;
            prioritaire();
        }

        
    }



    void prioritaire() //Permet d'attribuer la cible
    {

        for (int i = 0; i < cible_probable.Count; i++)    //On parcours les cibles
        {


            if (cible_probable[i] == default(GameObject)) //Si il y a des cibles on les enleves
            {
                cible_probable.RemoveAt(i);
                if (cible_probable.Count == i)
                {
                    return;
                }
            }
            else
            {
                if (cible_probable[i].tag == "Player")     //Si la cible en cours et Player elle passe prioritaire
                {
                    cible = cible_probable[i];
                    player_script = (Player)cible.GetComponent(typeof(Player));
                    last_statut = player_script.statut;
                    break;
                }
                else if (cible_probable[i].tag == "vivant")//Si c'est un vivant
                {

                    test = Vector3.Distance(this.gameObject.transform.position, cible_probable[i].transform.position); //On récupère sa distance

                    if (cible != null)  //Si il y a une cible probable
                    {
                        test_cible = Vector3.Distance(this.gameObject.transform.position, cible.transform.position);
                        if (test < test_cible)
                        {
                            cible = cible_probable[i];

                            vivant_script = (Vivant)cible.GetComponent(typeof(Vivant));
                            last_statut = vivant_script.statut;

                        }
                    }
                }
            }

        }




    }

    //Vérification
    void verification(GameObject other)
    {

        foreach (GameObject ex in exception)            //On enleve  les exceptions
        {
            if (ex == other)
            {
                return;
            }
        }

      
        //On assure l'accès aux variables
        if (other.tag == "Player")
        {
            player_script = (Player)other.GetComponent(typeof(Player));
        }
        if (other.tag == "vivant")
        {
            vivant_script = (Vivant)other.GetComponent(typeof(Vivant));
        }

        foreach (recherche cas in recherches)
        {
            if (cas.exclu_cette_recherche != true) //Si on recherche les éléments de la recherche plutot que de les exclures
            {
                /////////////////////////////////
                if (cas.nom != "") //S'il y a un nom
                {
                    detecte.Add(other);
                    actualise(other);
                }
                else            //S'il n'y a pas de nom
                {
                    if (cas.tag != "")//S'il y a un tag
                    {
                        if (cas.categorie != "")//S'il y a une catégorie
                        {

                            if (verfi_tag(cas, other) && verfi_categorie(cas, other))
                            {
                                //ON ENREGISTRE
                                detecte.Add(other);
                                actualise(other);

                            }

                        }
                        else
                        {
                            if (verfi_tag(cas, other))
                            {
                                //ON ENREGISTRE
                                detecte.Add(other);
                                actualise(other);
                            }
                        }

                    }
                    else       //S'il n'y a ni nom ni tag
                    {
                        if (cas.categorie != "")//S'il y a une catégorie
                        {
                            if (verfi_categorie(cas, other))
                            {
                                //ON ENREGISTRE
                                detecte.Add(other);
                                actualise(other);
                            }

                        }
                        else   //S'il n'y a ni nom ni tag ni catégorie
                        {

                            if (cas.nom == "" && cas.categorie == "" && cas.tag == "")
                            {
                                //ON ENREGISTRE
                                detecte.Add(other);
                                actualise(other);
                            }

                        }
                    }
                }
            }
           
        }
    }

    bool verfi_nom(recherche cas, GameObject other)
    {
        if (other.name == cas.nom)
        {
            return true;
        }

        return false;
    }

    bool verfi_tag(recherche cas, GameObject other)
    {
        if (other.tag == cas.tag)
        {
            return true;
        }

        return false;
    }

    bool verfi_categorie(recherche cas, GameObject other)
    {

        if (other.tag == "vivant") //Si l'objet est un vivant
        {
            vivant_script = (Vivant)other.GetComponent(typeof(Vivant));
            if (vivant_script.categorie == cas.categorie) //Et que c'est le bon statut
            {
                return true;
            }
        }

        return false;
    }

    bool verfi_statut(recherche cas, GameObject other)
    {

        if (other.tag == "vivant") //Si l'objet est un vivant
        {
            vivant_script = (Vivant)other.GetComponent(typeof(Vivant));
            if (vivant_script.statut == cas.statut) //Et que c'est le bon statut
            {
                return true;
            }
        }

        if (other.tag == "Player") //Si l'objet est un vivant
        {
            player_script = (Player)other.GetComponent(typeof(Player));

            //Debug.Log(cas.statut + " " + player_script.statut);

            if (player_script.statut == cas.statut) //Et que c'est le bon statut
            {
                return true;
            }
        }

        return false;
    }

    void enleve(GameObject other)
    {
        for (int i = 0; i < detecte.Count; i++)
        {
            if (other == detecte[i])
            {
                detecte.RemoveAt(i);
            }
        }

        for (int i = 0; i < cible_probable.Count; i++)
        {
            if (other == cible_probable[i])
            {
                cible_probable.RemoveAt(i);
            }
        }

        if (cible == other)
        {
            cible = null;
        }

    }

}
