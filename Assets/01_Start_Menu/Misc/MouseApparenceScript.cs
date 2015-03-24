using UnityEngine;
using System.Collections;

public class MouseApparenceScript : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void Start()
    { Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); }


}
