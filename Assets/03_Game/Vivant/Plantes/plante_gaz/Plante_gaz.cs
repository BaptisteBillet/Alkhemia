using UnityEngine;
using System.Collections;

public class Plante_gaz : Vivant {

    public GameObject toxic_nuage_prefab;
    private GameObject toxic_nuage;
    private Bullet toxic_nuage_script;

    public float delay_new_fongus_cloud;
    public float time_fongus_life;

    public GameObject explosion_prefab;
    private GameObject explosion;

	public GameObject cendre_prefab;
	private GameObject cendre;

	private bool mort;
	// Use this for initialization
	void Start () 
    {

		anim.SetBool("recolte", false);
        //Initialisation
        name = "Plante_gaz";
        statut = "toxic";
        immortel = false;
		mort=false;
        
        StartCoroutine(fongus());
        
	}
	
    // Création de nuages toxic
    IEnumerator fongus()
    {
        while(life>0)
        {
			anim.ResetTrigger("produit");
			anim.SetTrigger("produit");
            //création du nuage
            toxic_nuage = Instantiate(toxic_nuage_prefab) as GameObject; //Instantiation

			toxic_nuage.transform.position = this.transform.parent.position;
			toxic_nuage.transform.parent = this.transform.parent.parent;
            //toxic_nuage.transform.localPosition = new Vector3(-1,2.2f,0);
            /*
            toxic_nuage_script = (Bullet)toxic_nuage.GetComponent(typeof(Bullet));

            toxic_nuage.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
           */

            yield return new WaitForSeconds(delay_new_fongus_cloud);
            

        }

        yield return null;
    }
    // Destruction des nuages toxic avec le temps
  
    void Update()
    {
        if(life<=0 && mort==false)
        {
			mort = true;
			producteur = false;
			producteur_ok = false;
			anim.SetTrigger("explosion");
            explosion_prefab.transform.position = this.gameObject.transform.position;

            explosion = Instantiate(explosion_prefab) as GameObject; //Instantiation
            explosion.transform.position = this.gameObject.transform.position;
        }

		if(producteur_ok==false)
		{
			anim.SetBool("recolte", true);
		}

    }

	public void Cendre()
	{
		if (statut_temporaire == "feu")
		{
				cendre = Instantiate(cendre_prefab,this.transform.position,cendre_prefab.transform.rotation ) as GameObject; //Instantiation
		}
		
	}

}
