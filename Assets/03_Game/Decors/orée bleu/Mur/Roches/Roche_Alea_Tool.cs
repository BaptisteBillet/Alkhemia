using UnityEngine;
using System.Collections;

public class Roche_Alea_Tool : MonoBehaviour {

    public GameObject[] tab_roche_prefab;

    public bool taille_aleatoire;
    public float taille_min;
    public float taille_max;

    private GameObject roche;

    public void BuildObject()
    {
        if (tab_roche_prefab.Length>0)
        {
            roche = Instantiate(tab_roche_prefab[Random.Range(0, tab_roche_prefab.Length)], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            float taille=1;

            if(taille_max<taille_min)
            {
                taille_max = taille_min;    
            }
            
            if(taille_aleatoire==true)
            {
                taille = Random.Range(taille_min, taille_max);
            }

            roche.transform.localScale = new Vector3(taille, taille, 1);
        }
    }
}
