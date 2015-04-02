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
	public  List<GameObject> room_memory = new List<GameObject>();

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
        memory.Add(room_actual);
		room_memory.Add(room);
		room_memory[room_actual].SetActive(true);
    }


    public void ChangeRoom(int newRoom)
    {
		m_NeedNewRoom = true;
		
		room_memory[room_actual].SetActive(false);

        for (int i = 0; i<memory.Count;i++)
        {
			if (i == newRoom)
            {
				room_actual = i;
				room_memory[i].SetActive(true);
				m_NeedNewRoom = false;
            }
        }

        if(m_NeedNewRoom==true)
        {
			room_actual = newRoom;
            AddNewRoom();
            
			
        }
        

    }

}
