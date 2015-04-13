using UnityEngine;
using System.Collections;

public class BagManager : MonoBehaviour {

    #region Singleton
    static private BagManager s_Instance;
    static public BagManager instance
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

    #region members
    public bool m_CanShoot;

    public HUD_inventaire m_HUDInventaire;

    #endregion

    // Use this for initialization
	void Start () {
        m_CanShoot = true;
	}

}
