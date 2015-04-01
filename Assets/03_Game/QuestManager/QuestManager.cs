using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class QuestManager : MonoBehaviour
{
	#region Members

	#endregion

	// Use this for initialization
	void Start()
	{
		QuestEventManager.onEvent += Effect;
	}

	void OnDestroy()
	{
		QuestEventManager.onEvent -= Effect;
	}





	void Effect(EventManagerType emt)
	{
	
	}

	
}
