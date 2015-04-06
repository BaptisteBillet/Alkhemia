using UnityEngine;
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


public enum QuestEventManagerType
{
	START,
	ADD_MUSH,
	ADD_VENON,
	ADD_GOLDEN,
	SUBSTRACT_MUSH,
	SUBSTRACT_VENON,
	SUBSTRACT_GOLDEN
}

public class QuestEventManager : MonoBehaviour
{

	public delegate void EventAction(QuestEventManagerType emt);
	public static event EventAction onEvent;

	#region Singleton
	static private QuestEventManager s_Instance;
	static public QuestEventManager instance
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
		QuestEventManager.onEvent += (QuestEventManagerType emt) => { Debug.Log("&"); };
	}

	public static void quest_emit(QuestEventManagerType emt)
	{

		if (onEvent != null)
		{
			onEvent(emt);
		}
	}



}
