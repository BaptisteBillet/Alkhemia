using UnityEngine;
using System.Collections;

public class Ouverture_sac : MonoBehaviour {
    #region Singleton
    static private Ouverture_sac s_Instance;
    static public Ouverture_sac instance
    {
        get
        {
            return s_Instance;
        }
    }

    void Awake()
    {
        
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    public bool ouvert;

    public Animator animator;

    public void letsTremble()
    {
        animator.ResetTrigger("tremble");
        animator.SetTrigger("tremble");
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Back_1") || Input.GetKeyDown(KeyCode.R))
        {
            if (ouvert==true) { ouvert = false; }
            else if (ouvert == false) { ouvert = true; }

            animator.SetBool("ouvert", ouvert);
        }

	}
}
