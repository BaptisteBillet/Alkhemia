using UnityEngine;
using System.Collections;

public class MouseApparenceScript : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = new Vector2(16, 16);//Vector2.zero;

    public GameObject cursor_prefab;
    public GameObject cursor;

    public void Start()
    { 
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        cursor = Instantiate(cursor_prefab) as GameObject;
        
    }

    public void Update()
    {
		if (ControllerManager.instance.m_XboxMode == false)
		{
			Debug.Log("false");
		}
		else
		{
			cursor.transform.position = new Vector3(1000, 1000, 0);
			Debug.Log("true");
		}

		cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, 10f);
    }


}
