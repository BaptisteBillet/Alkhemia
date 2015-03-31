using UnityEngine;
using System.Collections;

public class Render_profondeur : MonoBehaviour {

    public SpriteRenderer sprite;
	// Use this for initialization
	void Start () {

        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        //Positionnement layer
        if(sprite!=null)
        {
            sprite.sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;
        }
        
	}

}
