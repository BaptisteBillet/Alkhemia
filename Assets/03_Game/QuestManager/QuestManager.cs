using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class QuestManager : MonoBehaviour
{
	#region Members

	public int toxic_mush;
	public int objectif_toxic_mush;

	public int spider_venon;
	public int objectif_spider_venon;


	#endregion

	// Use this for initialization
	void Start()
	{


		toxic_mush=0;
		spider_venon=0;
		QuestEventManager.onEvent += Effect;
	}

	void OnDestroy()
	{
		QuestEventManager.onEvent -= Effect;
	}

	void Effect(QuestEventManagerType emt)
	{
		switch(emt)
		{
			case QuestEventManagerType.START:

				break;

			case QuestEventManagerType.ADD_MUSH:

				break;

			case QuestEventManagerType.ADD_SPIDER:

				break;

			case QuestEventManagerType.SUBSTRACT_MUSH:

				break;

			case QuestEventManagerType.SUBSTRACT_SPIDER:

				break;



		}
	}

	
}
