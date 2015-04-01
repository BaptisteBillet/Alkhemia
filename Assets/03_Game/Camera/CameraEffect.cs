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
    #endregion

    // Use this for initialization
    void Start()
    {
        QuestEventManager.onEvent += Effect;

        fisheye = GetComponent<Fisheye>();
        shakemanager = GetComponent<ShakeManager>();
        vignette = GetComponent<VignetteAndChromaticAberration>();

        fisheye.enabled = false;
        fisheye.strengthX = 0;
        fisheye.strengthY = 0;

        VignetteEffect = true;
        StartCoroutine(Vignette_Effect());

    }

    void OnDestroy()
    {
        QuestEventManager.onEvent -= Effect;
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
            
        }

    }

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
                intensity_value += 0.1f;
                vignette.intensity=intensity_value;
                yield return new WaitForSeconds(Random.Range(1,2));
            }

            while (intensity_value > 0.5f)
            {
                intensity_value -= 0.1f;
                vignette.intensity = intensity_value;
                yield return new WaitForSeconds(Random.Range(1,2));
            }

        }
    }

}
