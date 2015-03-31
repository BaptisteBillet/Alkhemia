using UnityEngine;
using System.Collections;

public class ChangeCollider : MonoBehaviour {


	public Collider2D[] erase_trigger;

	//public Collision2D[] erase_collider;

	public Collider2D[] new_trigger;

	//public Collision2D[] new_collision;
	

	public void change_Collider()
	{
		for(int i=0; i<erase_trigger.Length;i++)
		{
			erase_trigger[i].enabled = false;
		}
		/*
		for (int y = 0; y < erase_collider.Length; y++)
		{
			erase_collider[y].collider.enabled = false;
		}
		*/
		for (int x = 0; x < new_trigger.Length; x++)
		{
			new_trigger[x].enabled = true;
		}
		/*
		for (int z = 0; z < new_collision.Length; z++)
		{
			new_collision[z].collider.enabled= true;
		}
		*/
	}

}
