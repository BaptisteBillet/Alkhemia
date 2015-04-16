using UnityEngine;
using System.Collections;

/*
 * Comment émettre un event:
		SoundManagerEvent.emit(SoundManagerType.ENEMY_HIT);
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


public enum SoundManagerType
{
	DAMAGE,
	EXPLOSION,
	FIGHT,
	FIRE_START,
	FIRE_LOOP,
	GAZ,
	OREEBLEUE,
	PLAYER_POTION,
	PLAYER_INTERRACTION,
	PLAYER_DROP,
	PLAYER_PICK,
	POTION,
	SPIDER_DIE,
	SPIDER_SPLIT,
	SPIDER_BLOQUE,
	QUEST_PANACEE,
	QUEST_INGREDIENT,
	QUEST_ALL



}

public class SoundManagerEvent : MonoBehaviour
{

	public delegate void EventAction(SoundManagerType emt);
	public static event EventAction onEvent;

	#region Singleton
	static private SoundManagerEvent s_Instance;
	static public SoundManagerEvent instance
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
		SoundManagerEvent.onEvent += (SoundManagerType emt) => { Debug.Log("&"); };
	}

	public static void emit(SoundManagerType emt)	{
		
		if (onEvent != null)
		{
			onEvent(emt);
		}
	}



}
