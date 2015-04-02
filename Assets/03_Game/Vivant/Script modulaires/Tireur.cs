using UnityEngine;
using System.Collections;

public class Tireur : MonoBehaviour 
{
    public Transform cible;

    public GameObject bullet_prefab;
    private GameObject bullet;
    private Bullet bullet_script;

    public float frequence;
    public float vitesse;

    private Vector3 dir;



    void OnEnable()
    {
        StartCoroutine(tir());
    }

    IEnumerator tir()
    {
        while(this.enabled)
        {
            //INSTANTIATION
            bullet = Instantiate(bullet_prefab, this.gameObject.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation
            bullet.transform.parent = this.gameObject.transform.parent.parent.parent;

            if (cible != null)
            {
                dir = new Vector3(cible.transform.position.x - this.gameObject.transform.parent.transform.position.x,cible.transform.position.y- this.gameObject.transform.parent.transform.position.y,cible.transform.position.z);
                //dir = cible.transform.position.normalized;
            }
            //ENVOI dans une direction
            bullet.GetComponent<Rigidbody2D>().AddForce(dir * Time.deltaTime*vitesse);
            
            yield return new WaitForSeconds(frequence); 
            
        }

        yield return null;
    }



}
