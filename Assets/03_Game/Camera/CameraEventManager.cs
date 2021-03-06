﻿using UnityEngine;
using System.Collections;

/*
 * Comment émettre un event:
		CameraEventManager.emit(EventManagerType.ENEMY_HIT, this.gameObject);
 * 
 * Comment traiter un event (dans un start ou un initialisation)
		EventManagerScript.onEvent += (EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
 * ou:
		void maCallback(EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
		EventManagerScript.onEvent += maCallback;
 * 
 * qui permet de:
		EventManagerScript.onEvent -= maCallback; //Retire l'appel
 */


public enum EventManagerType
{
    FISHEYEBUMP,
    SHAKE,
	START_CINE
}

public class CameraEventManager : MonoBehaviour
{

    public delegate void EventAction(EventManagerType emt);
    public static event EventAction onEvent;

    #region Singleton
	static private CameraEventManager s_Instance;
	static public CameraEventManager instance
    {
        get
        {
            return s_Instance;
        }
    }
    #endregion


    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
		CameraEventManager.onEvent += (EventManagerType emt) => { Debug.Log("&"); };
    }

    public static void emit(EventManagerType emt)
    {

        if (onEvent != null)
        {
            onEvent(emt);
        }
    }
    


}
