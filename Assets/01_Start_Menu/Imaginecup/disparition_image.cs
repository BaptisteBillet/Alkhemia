using UnityEngine;
using System.Collections;

public class disparition_image : MonoBehaviour {

    public CanvasGroup color;

    void Awake()
    {
        color.alpha = 0;
    }


	// Use this for initialization
	void Start () 
    {
        StartCoroutine(fade_out());
	}
	
	IEnumerator fade_out()
    {

        while (color.alpha < 1)
        {
            color.alpha += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this);

        yield return null;
    }
}
