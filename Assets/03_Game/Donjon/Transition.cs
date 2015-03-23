using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

    private Donjon donjon_script;

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {

        donjon_script = (Donjon)transform.parent.GetComponent(typeof(Donjon));
        sprite=this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (donjon_script.transition == true)
        {
            donjon_script.transition = false;
            //StartCoroutine(fade_in());
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            StartCoroutine(fade_out());
        }    
	}

    /*
    IEnumerator fade_in()
    {
        while (sprite.color.a < 1)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a+0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        StartCoroutine(fade_out());
    }*/

    IEnumerator fade_out()
    {
        while (sprite.color.a > 0.1f)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }

}
