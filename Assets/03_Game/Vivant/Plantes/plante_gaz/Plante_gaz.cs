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


	// Use this for initialization
	void Start () 
    {
        //Initialisation
        name = "Plante_gaz";
        statut = "toxic";
        immortel = false;
        //Initialisation Cloud
        delay_new_fongus_cloud = 5;
        time_fongus_life=2;

        
        StartCoroutine(fongus());
        
	}
	
    // Création de nuages toxic
    IEnumerator fongus()
    {
        while(life>0)
        {

            //création du nuage
            toxic_nuage = Instantiate(toxic_nuage_prefab) as GameObject; //Instantiation
            toxic_nuage.transform.parent = transform;
            toxic_nuage.transform.localPosition = new Vector3(-1,2.2f,0);
            
            toxic_nuage_script = (Bullet)toxic_nuage.GetComponent(typeof(Bullet));

            toxic_nuage.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            StartCoroutine(kill_fongus());

            yield return new WaitForSeconds(delay_new_fongus_cloud);
            

        }

        yield return null;
    }
    // Destruction des nuages toxic avec le temps
    IEnumerator kill_fongus()
    {

        yield return new WaitForSeconds(time_fongus_life);
        Destroy(toxic_nuage.gameObject);
        yield return null;
    }


    void Update()
    {
        if(life<=0)
        {
            Destroy(this.gameObject);
            explosion_prefab.transform.position = this.gameObject.transform.position;

            explosion = Instantiate(explosion_prefab) as GameObject; //Instantiation
            explosion.transform.position = this.gameObject.transform.position;
        }
    }

}
