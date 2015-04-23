using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class QuestManager : MonoBehaviour
{
	#region Members
	
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


	public GameObject Validate_mush_pop;
	public GameObject Validate_spider_pop;
	public GameObject Validate_golden_pop;

	public GameObject Popup;

	public GameObject Finish;

	private bool champi;
	private bool venin;
 

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
		if (toxic_mush && spider_venon && golden)
		{
			finish = true;
			Invoke("Goto_Fin", 3);
		}

		if (toxic_mush==false || spider_venon==false)
		{
			finish = false;
		}

		if (toxic_mush == true && champi==false)
		{
			champi = true;
			Validate_mush.SetActive(true);
			Validate_mush_pop.SetActive(true);
			Popup.SetActive(true);
		}
		if (spider_venon == true && venin==false)
		{
			venin = true;
			Validate_spider.SetActive(true);
			Validate_spider_pop.SetActive(true);
			Popup.SetActive(true);
		}


		if (toxic_mush == false)
		{
			champi = false;
			Validate_mush.SetActive(false);
			Validate_mush_pop.SetActive(false);
		}
		if (spider_venon == false)
		{
			venin = false;
			Validate_spider.SetActive(false);
			Validate_spider_pop.SetActive(false);
		}
		
		if(golden==true)
		{
			Validate_golden_pop.SetActive(true);
		}

	}

	void Goto_Fin()
	{
		Application.LoadLevel("Fin_Demo");
	}


}
