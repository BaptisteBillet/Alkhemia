using UnityEngine;
using System.Collections;

public class Start_Cine : MonoBehaviour {

	public GameObject m_SpawnPlayer;

	void Start()
	{
		CameraEventManager.emit(EventManagerType.START_CINE);
	}

	
}
