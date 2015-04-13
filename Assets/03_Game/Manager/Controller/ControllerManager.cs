using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {


	#region Singleton
	static private ControllerManager s_Instance;
	static public ControllerManager instance
	{
		get
		{
			return s_Instance;
		}
	}
	#endregion


	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		//DontDestroyOnLoad(this);
	}





	#region Members
	public bool m_XboxMode;

	#endregion

	// Use this for initialization
	void Start()
	{
		ControllerEventManager.onEvent += Effect;
	}

	void OnDestroy()
	{
		ControllerEventManager.onEvent -= Effect;
	}

	void Effect(ControllerEventManagerType emt)
	{

	}

	void Update()
	{

		#region Xbox
		{
			if (Input.GetButtonDown("A_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("B_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("X_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("Y_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("Start_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("Back_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("LB_1"))
			{
				m_XboxMode = true;
			}
			if (Input.GetButtonDown("RB_1"))
			{
				m_XboxMode = true;
			}

			if (Input.GetAxis("DPad_XAxis_1") > 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("DPad_XAxis_1") < 0)
			{
				m_XboxMode = true;
			}

			if (Input.GetAxis("L_XAxis_1") < 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("L_XAxis_1") > 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("L_YAxis_1") < 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("L_YAxis_1") > 0)
			{
				m_XboxMode = true;
			}



			if (Input.GetAxis("R_XAxis_1") < 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("R_XAxis_1") > 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("R_YAxis_1") < 0)
			{
				m_XboxMode = true;
			}
			if (Input.GetAxis("R_YAxis_1") > 0)
			{
				m_XboxMode = true;
			}
		}
		#endregion

		#region Clavier
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.E))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.A))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.Q))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.Z))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.S))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.D))
				m_XboxMode = false;
			if (Input.GetKeyDown(KeyCode.Escape))
				m_XboxMode = false;

			if (Input.GetMouseButtonDown(1))
				m_XboxMode = false;
			if (Input.GetMouseButtonDown(2))
				m_XboxMode = false;
			if (Input.GetMouseButtonDown(3))
				m_XboxMode = false;



		}
		#endregion

	}

	
}
