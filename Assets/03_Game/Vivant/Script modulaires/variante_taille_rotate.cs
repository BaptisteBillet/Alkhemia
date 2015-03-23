using UnityEngine;
using System.Collections;

public class variante_taille_rotate : MonoBehaviour {

    public bool taille_base;
    public float taille_min;
    public float taille_max;
    private float taille;

    public bool rotate_base;
    public float rotate_min;
    public float rotate_max;
    private float rotate;
	// Use this for initialization
	void Start () {

        if (taille_base == true)
        {
            taille = Random.Range(taille_min, taille_max);

            this.gameObject.transform.localScale = new Vector3(taille, taille, 0);
        }

        if (rotate_base == true)
        {
            if (rotate_min == 0 && rotate_max == 0)
            {
                rotate_max = 360;
            }

            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(rotate_min, rotate_max));
        }
	}

}
