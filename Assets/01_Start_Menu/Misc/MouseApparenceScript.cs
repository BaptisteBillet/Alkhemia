using UnityEngine;
using System.Collections;

public class MouseApparenceScript : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = new Vector2(16, 16);//Vector2.zero;

    public void Start()
    { Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); }




}
