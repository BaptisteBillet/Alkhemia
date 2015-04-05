using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraEffect : MonoBehaviour
{
    #region Members
    private UnityStandardAssets.ImageEffects.Fisheye fisheye;
    private ShakeManager shakemanager;
    private VignetteAndChromaticAberration vignette;
    public bool VignetteEffect;

	[HideInInspector]
	public GameObject m_Player;

	public GameObject Apparition;

    #endregion


    // Use this for initialization
    void Start()
    {
		m_Player = GameObject.FindGameObjectWithTag("Player");
		

		CameraEventManager.onEvent += Effect;

        fisheye = GetComponent<Fisheye>();
        shakemanager = GetComponent<ShakeManager>();
        vignette = GetComponent<VignetteAndChromaticAberration>();

        fisheye.enabled = false;
        fisheye.strengthX = 0;
        fisheye.strengthY = 0;

        VignetteEffect = true;
        

    }

    void OnDestroy()
    {
		CameraEventManager.onEvent -= Effect;
    }


   


    void Effect(EventManagerType emt)
    {
       
        switch (emt)
        {
            case  EventManagerType.FISHEYEBUMP:
                StartCoroutine(fisheyemanager());
                break;
            //Bullets

            case EventManagerType.SHAKE:
                shakemanager.LetsShake(false);
                break;
            
			case EventManagerType.START_CINE:
				StartCoroutine(start_coroutine());
				break;

        }

    }


	#region Start
	IEnumerator start_coroutine()
	{
		//On lance le vignetage traditionnel
		StartCoroutine(start_vignetage());
		yield return null;
	}

	IEnumerator start_vignetage()
	{	
		m_Player.SetActive(false);
		vignette.intensity = 33;
		yield return new WaitForSeconds(2);
		Instantiate(Apparition, Vector3.zero, this.transform.rotation);
		yield return new WaitForSeconds(0.9f);
		m_Player.SetActive(true);

		while(vignette.intensity>1)
		{
			vignette.intensity -= 0.1f;
			yield return new WaitForSeconds(0.0000000001f);
		}
		vignette.intensity = 1;
	}


	#endregion


	IEnumerator fisheyemanager()
    {

        fisheye.enabled = true;

        yield return new WaitForSeconds(0.3f);

        fisheye.enabled = false;
        
        float fade=0;
        while(fade<0.3f)
        {
            fade += 0.0001f;
            fisheye.strengthX = fade;
            fisheye.strengthY = fade;
            yield return new WaitForSeconds(0.0001f);
        }

        while (fade > 0.0f)
        {
            fade -= 0.0001f;
            fisheye.strengthX = fade;
            fisheye.strengthY = fade;
            yield return new WaitForSeconds(0.0001f);
        }
        fisheye.enabled = false;
        fisheye.strengthX = 0;
        fisheye.strengthY = 0;
        yield return null;
         
    }

    IEnumerator Vignette_Effect()
    {
        float intensity_value=0.5f;

        while (VignetteEffect)
        {

            while(intensity_value<2f)
            {
                intensity_value += 0.05f;
                vignette.intensity=intensity_value;
                yield return new WaitForSeconds(Random.Range(0.5f,1f));
            }

            while (intensity_value > 0.5f)
            {
                intensity_value -= 0.05f;
                vignette.intensity = intensity_value;
                yield return new WaitForSeconds(Random.Range(0.5f,1f));
            }

        }
    }

}
