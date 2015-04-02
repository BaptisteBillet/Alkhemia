using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    #region Singleton
    static private RoomManager s_Instance;
    static public RoomManager instance
    {
        get
        {
            return s_Instance;
        }
    }


    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }


    #endregion



    public int room_actual;
    private GameObject room;
    //Tableau des rooms à instancier
    public List<GameObject> set = new List<GameObject>();
    private List<int> memory = new List<int>();

    private bool m_NeedNewRoom;

    void Start()
    {
        AddNewRoom();
        m_NeedNewRoom = true;
    }

    private void AddNewRoom()
    {
        //On instancie la room
        room = Instantiate(set[room_actual]) as GameObject;
        room.transform.position = new Vector3(0, 0, 0);
        room.SetActive(true);
        memory.Add(room_actual);
    }


    public void ChangeRoom(int newRoom)
    {
        room.SetActive(false);

        room_actual = newRoom;


        for (int i = 0; i>memory.Count;i++)
        {
            if(i==room_actual)
            {
                room.SetActive(false);
                room=set[room_actual];
                room.SetActive(true);
                m_NeedNewRoom = false;
                return;
            }
        }
        if(m_NeedNewRoom==true)
        {
            room.SetActive(false);
            AddNewRoom();
            m_NeedNewRoom = false;
        }
        

    }

}
