using UnityEngine;
using System.Collections;

public class Inflammable : MonoBehaviour {

    //Accès au script du main_script
    public Vivant main_script;

    //Cendres
    public GameObject cendre_prefab;
    private GameObject cendre;

    public GameObject parent;

	// Use this for initialization
	void Start () 
    {
        if(main_script==null)
        {
            Debug.LogError("Vous avez oubliez d'attacher le scipt de référence pour le scipt usuel Inflammable sur le Gameobject " + this.gameObject);
        }
	}
    
	void Update()
    {

        if(main_script.statut_temporaire =="feu")
        {
            
            if(main_script.life<=0)
            {
                cendre = Instantiate(cendre_prefab) as GameObject; //Instantiation
                cendre.transform.position = this.gameObject.transform.position;
                cendre.transform.parent = this.gameObject.transform.parent.parent;
                if(parent!=null)
                {
                    Destroy(parent);
                }
                else
                {
                    Destroy(this.gameObject);
                }
                
            }

        }
    }

}
