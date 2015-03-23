using UnityEngine;
using System.Collections;

public class Arbre_Alea_Tool : MonoBehaviour {

    public GameObject[] tab_arbre_prefab;

    public bool taille_aleatoire;
    public float taille_min;
    public float taille_max;

    private GameObject arbre;

    public void BuildObject()
    {
        if (tab_arbre_prefab.Length > 0)
        {
            arbre = Instantiate(tab_arbre_prefab[Random.Range(0, tab_arbre_prefab.Length)], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            float taille=1;

            if(taille_max<taille_min)
            {
                taille_max = taille_min;    
            }
            
            if(taille_aleatoire==true)
            {
                taille = Random.Range(taille_min, taille_max);
            }

            arbre.transform.localScale = new Vector3(taille, taille, 1);
        }
    }
}
