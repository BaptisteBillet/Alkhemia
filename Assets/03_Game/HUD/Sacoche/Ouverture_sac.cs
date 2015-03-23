using UnityEngine;
using System.Collections;

public class Ouverture_sac : MonoBehaviour {

    int ouvert;

    public GameObject sac_ouvert;
    public GameObject sac_ferme;

    void Start()
    {
        ouvert = 0;
        sac_ouvert.SetActive(false);
        sac_ferme.SetActive(true);
    }

    IEnumerator fermeture ()
    {
        yield return new WaitForSeconds(0.2f);
        sac_ouvert.SetActive(false);
        sac_ferme.SetActive(true);
        ouvert = 0;
        yield return null;
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Back_1") || Input.GetKeyDown(KeyCode.R))
        {
            if (ouvert == 0) { ouvert = 1; }
            else if (ouvert == 1) { ouvert = 2; }
        }

        if(ouvert==1)
        {
            sac_ouvert.SetActive(true);
            sac_ferme.SetActive(false);
            
        }
        if(ouvert==2)
        {
            StartCoroutine(fermeture());
        }

	}
}
