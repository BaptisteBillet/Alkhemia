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

	private bool IsFire;

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
		SoundManagerEvent.emit(SoundManagerType.GAZ);
        while(life>0)
        {
			anim.ResetTrigger("produit");
			anim.SetTrigger("produit");
            //création du nuage
            toxic_nuage = Instantiate(toxic_nuage_prefab,new Vector3(this.transform.position.x,this.transform.position.y+0.5f,this.transform.position.z),this.transform.rotation) as GameObject; //Instantiation

			toxic_nuage.transform.position = this.transform.parent.position;
			toxic_nuage.transform.parent = this.transform.parent.parent;

			toxic_nuage.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);



            yield return new WaitForSeconds(delay_new_fongus_cloud);
            

        }

        yield return null;
    }
    // Destruction des nuages toxic avec le temps
  
    void Update()
    {
        if(life<=0 && mort==false)
        {
			if (statut_temporaire == "feu")
			{
				IsFire = true;
			}

			mort = true;
			producteur = false;
			producteur_ok = false;
			anim.ResetTrigger("production");
			anim.SetBool("explosion",true);
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
		if (IsFire == true)
		{
				cendre = Instantiate(cendre_prefab,this.transform.position,cendre_prefab.transform.rotation ) as GameObject; //Instantiation
		}
		
	}

}
