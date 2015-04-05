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


public enum SoundManagerType
{
	SHOOT

}

public class SoundManagerEvent : MonoBehaviour
{

	public delegate void EventAction(SoundManagerType emt, AudioSource audio_source);
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
		SoundManagerEvent.onEvent += (SoundManagerType emt, AudioSource audio_source ) => { Debug.Log("&"); };
	}

	public static void emit(SoundManagerType emt, AudioSource audio_source)
	{
		
		if (onEvent != null)
		{
			onEvent(emt,audio_source);
		}
	}



}
