using UnityEngine;
using System.Collections;


/*
 * Comment émettre un event:
		QuestEventManager.emit(QuestEventManagerType.ENEMY_HIT);
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


public enum ControllerEventManagerType
{

}

public class ControllerEventManager : MonoBehaviour
{

	public delegate void EventAction(ControllerEventManagerType emt);
	public static event EventAction onEvent;

	#region Singleton
	static private ControllerEventManager s_Instance;
	static public ControllerEventManager instance
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
		ControllerEventManager.onEvent += (ControllerEventManagerType emt) => {  };
	}

	public static void controller_emit(ControllerEventManagerType emt)
	{

		if (onEvent != null)
		{
			onEvent(emt);
		}
	}



}