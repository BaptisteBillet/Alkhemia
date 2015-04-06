using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class QuestManager : MonoBehaviour
{
	#region Members
	[HideInInspector]
	public int toxic_mush_number;
	[HideInInspector]
	public bool toxic_mush;
	[HideInInspector]
	public int spider_venon_number;
	[HideInInspector]
	public bool spider_venon;
	[HideInInspector]
	public bool golden;
	[HideInInspector]
	public bool finish;

	public GameObject Validate_mush;
	public GameObject Validate_spider;

	public GameObject mush_white;
	public GameObject venon_white;

	public GameObject Finish;
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

	void Effect(QuestEventManagerType emt)
	{
		switch(emt)
		{
			case QuestEventManagerType.START:

				break;

			case QuestEventManagerType.ADD_MUSH:
				toxic_mush_number++;
				toxic_mush = true;
				break;

			case QuestEventManagerType.ADD_VENON:
				spider_venon_number++;
				spider_venon = true;
				break;

			case QuestEventManagerType.ADD_GOLDEN:
				golden = true;
				break;

			case QuestEventManagerType.SUBSTRACT_MUSH:
				toxic_mush_number--;
				if (toxic_mush_number <= 0)
				{
					toxic_mush = false;
				}
				break;

			case QuestEventManagerType.SUBSTRACT_VENON:
				spider_venon_number--;
				if (spider_venon_number <= 0)
				{
					spider_venon = false;
				}
				break;
			case QuestEventManagerType.SUBSTRACT_GOLDEN:
				golden = false;
				break;
		}
	}

	void Update()
	{
		if (toxic_mush && spider_venon)
		{
			finish = true;
		}

		if (toxic_mush==false || spider_venon==false)
		{
			finish = false;
		}

		if (toxic_mush == true)
		{
			Validate_mush.SetActive(true); mush_white.SetActive(false);
		}
		if (spider_venon == true)
		{
			Validate_spider.SetActive(true); venon_white.SetActive(false);
		}


		if (toxic_mush == false)
		{
			Validate_mush.SetActive(false); mush_white.SetActive(true);
		}
		if (spider_venon == false)
		{
			Validate_spider.SetActive(false); venon_white.SetActive(true);
		}
		
		

	}

	
}
