using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour 
{
    public Element element;
    public bool IsInInventaire;


    public Transform PlayerPosition;

    public GameObject Origin;

    public int index_inventaire;

    void OnMouseEnter()
    {
        BagManager.instance.m_CanShoot = false;
        if (IsInInventaire)
        {
            
        }
    }
    void OnMouseExit()
    {
        BagManager.instance.m_CanShoot = true;
        if (IsInInventaire)
        {

        }
    }

    void OnMouseDown()
    {
        if(IsInInventaire)
        {
            if(Autel.instance || Marchand.instance)
            {

                if (Autel.instance)
                {
                    if (Autel.instance.ReadyForChange)
                    {
                        //Autel.instance.autel_activation(Origin);
                    }
                }

                if (Marchand.instance)
                {
                    if (Marchand.instance.ReadyForChange)
                    {
                        //Marchand.instance.autel_activation(Origin);
                    }
                }

            }
            else
            {
                Instantiate(Origin, PlayerPosition.position, this.transform.rotation);
				
				switch (this.gameObject.name)
				{
					case "I_champignon(Clone)":
						QuestEventManager.quest_emit(QuestEventManagerType.SUBSTRACT_MUSH);
						break;

					case "I_venin(Clone)":
						QuestEventManager.quest_emit(QuestEventManagerType.SUBSTRACT_VENON);
						break;
				}



            }

			BagManager.instance.m_HUDInventaire.drop_inventaire(index_inventaire);
			Invoke("SetTrue", 0.1f);
			GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject,0.2f);
        }
    }

	void SetTrue() //C'est jeromou il a dit
	{
		BagManager.instance.m_CanShoot = true;
	}


}
