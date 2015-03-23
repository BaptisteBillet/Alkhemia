using UnityEngine;
using System.Collections;

public class Render_profondeur : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        //Positionnement layer
        if(sprite!=null)
        {
            sprite.sortingOrder = (int)(this.gameObject.transform.position.y * 10) * -1;
        }
        
	}

}
