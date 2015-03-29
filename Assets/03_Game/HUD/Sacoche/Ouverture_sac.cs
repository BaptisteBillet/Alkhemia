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
	public bool autel;
    public Animator animator;
	public bool ouvertureForce;
	public bool IsouvertureForce;
    public void letsTremble()
    {
        animator.ResetTrigger("tremble");
        animator.SetTrigger("tremble");
    }

	void Start()
	{
		IsouvertureForce = false;
	}

	// Update is called once per frame
	void Update () 
    {
        if ((Input.GetButtonDown("Back_1") || Input.GetKeyDown(KeyCode.R) || ouvertureForce==true)&&IsouvertureForce==true)
        {
			Debug.Log("sac");
			IsouvertureForce = false;
            animator.SetBool("ouvert", ouvert);
			if (ouvert == true) { ouvert = false; }
			else if (ouvert == false) { ouvert = true; }
        }

	}
}
