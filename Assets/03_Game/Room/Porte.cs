using UnityEngine;
using System.Collections;

public class Porte : MonoBehaviour {


    public int m_GotoRoomNumber;

    void OnTriggerEnter2D (Collider2D other)
    {

        if(other.gameObject.tag=="Player")
        {
            RoomManager.instance.ChangeRoom(m_GotoRoomNumber);
        }

    }

}
