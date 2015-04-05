using UnityEngine;
using System.Collections;

public class ShakeManager : MonoBehaviour {
    #region Singleton
    static private ShakeManager s_Instance;
    static public ShakeManager instance
    {
        get
        {
            return s_Instance;
        }
    }

    void Awake()
    {
        mainCamera = Camera.main;
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    #region members
    private float shakeAmt;
    public Camera mainCamera;

    private bool shake_up;
    private bool shake_left;

    public GameObject douleur;
    SpriteRenderer douleur_sprite;

    public GameObject flash;
    SpriteRenderer flash_sprite;

    Color alphachange;
    Color alphachange_flash;
    #endregion

    void Start()
    {
        douleur_sprite = douleur.GetComponent<SpriteRenderer>();
        flash_sprite = flash.GetComponent<SpriteRenderer>();
    }

    public void LetsShake(bool rouge,float relative = 100, bool _shake_up = true, bool _shake_left = true)
    {
        
        shake_up=_shake_up;
        shake_left=_shake_left;
        
        if(rouge==true)
        {
            StartCoroutine(douleurflashonce());
        }

        shakeAmt = relative * .0025f;
        
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
        
    }

    public void LetsShakeLife(Player playerscript, float relative = 20)
    {
        StartCoroutine(LetsShakeLifeCoroutine(playerscript,relative));
        
        StartCoroutine(douleurflash(playerscript));
    }

    IEnumerator douleurflashonce()
    {
        alphachange_flash = flash_sprite.color;
        alphachange_flash.a = 0f;
        flash_sprite.color = alphachange_flash;
        flash.SetActive(true);

        alphachange_flash.a = 1f;
        flash_sprite.color = alphachange_flash;

        while (alphachange_flash.a > 0.0f)
        {
            alphachange_flash.a -= 0.01f;
            flash_sprite.color = alphachange_flash;
            yield return new WaitForSeconds(0.01f);
        }
        flash.SetActive(false);
    }

    IEnumerator douleurflash (Player playerscript)
    {
        alphachange = douleur_sprite.color;
        alphachange.a = 0f;
        douleur_sprite.color = alphachange;
        douleur.SetActive(true);


        while (douleur_sprite.color.a < 0.8f)
        {
            alphachange.a += 0.01f;
            douleur_sprite.color = alphachange;
            yield return new WaitForSeconds(0.01f);
        }
        
        while (playerscript.life <= playerscript.life_alerte)
        {
            while (douleur_sprite.color.a > 0.2f)
            {
                alphachange.a -= 0.01f;
                douleur_sprite.color = alphachange;
                yield return new WaitForSeconds(0.01f);

            }
            while (douleur_sprite.color.a < 0.8f)
            {
                alphachange.a += 0.01f;
                douleur_sprite.color = alphachange;
                yield return new WaitForSeconds(0.01f);
            }

        }

        while (douleur_sprite.color.a > 0)
        {
            alphachange.a -= 0.01f;
            douleur_sprite.color = alphachange;
            yield return new WaitForSeconds(0.01f);
        }

        douleur.SetActive(false);

    }

    IEnumerator LetsShakeLifeCoroutine(Player playerscript, float relative = 20)
    {
        shake_up = true;
        shake_left = true;

        shakeAmt = relative * .0025f;

        while (playerscript.life <= playerscript.life_alerte)
        {

            InvokeRepeating("CameraShake", 0, .01f);
            Invoke("StopShaking", 0.3f);
            yield return new WaitForSeconds(2);
        }

        playerscript.alertelaunched = false;
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            if(shake_up)
            {
                pp.y += quakeAmt;
            }
            if(shake_left)
            {
                pp.x += quakeAmt;
            }
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
		mainCamera.transform.position = new Vector3(0, 0, -10);
    }

}
