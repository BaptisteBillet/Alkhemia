using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
    Vector3 last_mousePos= new Vector3();
    Vector3 mousePos = new Vector3();

    private Player player_script;
    

    void Start()
    {
        player_script = (Player)transform.parent.GetComponent(typeof(Player));

        mousePos = Input.mousePosition;
        last_mousePos = mousePos;
        player_script.mode_xbox = false;
    }

    

    void Update()
    {
        if (((Input.GetAxis("R_XAxis_1") != 0) || (Input.GetAxis("R_YAxis_1") != 0)) && player_script.mode_xbox == false)
        {
            player_script.mode_xbox = true;
        }

        mousePos = Input.mousePosition;

        if (last_mousePos != mousePos && (Input.GetAxis("R_XAxis_1") == 0) && (Input.GetAxis("R_YAxis_1") == 0))
        {
            player_script.mode_xbox = false;
            last_mousePos = mousePos;
        }


        if ((Input.GetAxis("R_XAxis_1") == 0) && (Input.GetAxis("R_YAxis_1") == 0) && player_script.mode_xbox == false)
        {
            //rotation
            mousePos = Input.mousePosition;
            mousePos.z = 5.23f;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
        else if (((Input.GetAxis("R_XAxis_1") != 0) || (Input.GetAxis("R_YAxis_1") != 0)) && player_script.mode_xbox == true)
        {
            mousePos = new Vector3((Input.GetAxis("R_XAxis_1")), (Input.GetAxis("R_YAxis_1")) * -1, 0);
            mousePos.z = 5.23f;
           /*
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            */
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
           // last_mousePos = mousePos;
        }

    }
}
