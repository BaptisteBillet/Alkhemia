using UnityEngine;
using System.Collections;

public class Inflammable : MonoBehaviour {

    //Accès au script du main_script
    public Vivant main_script;

    //Cendres
    public GameObject cendre_prefab;
    private GameObject cendre;

    public GameObject parent;

    public Transform Parent;

	// Use this for initialization
	void Start () 
    {
        if(main_script==null)
        {
            Debug.LogError("Vous avez oubliez d'attacher le scipt de référence pour le scipt usuel Inflammable sur le Gameobject " + this.gameObject);
        }
	}
    
    void Cendre()
    {
        cendre = Instantiate(cendre_prefab) as GameObject; //Instantiation

		Parent = this.transform;

		if(this.transform.parent)
		{
			Parent = this.transform.parent;

			while (Parent.parent)
			{
				Parent = Parent.parent;
			}

		}

      
        cendre.transform.position = this.gameObject.transform.position;
        cendre.transform.parent = Parent;

       
        if(parent!=null)
        {
            Destroy(this.parent);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	void Update()
    {

        if(main_script.statut_temporaire =="feu")
        {
            
            if(main_script.life<=0)
            {

                Cendre();
            }

        }
    }

}
