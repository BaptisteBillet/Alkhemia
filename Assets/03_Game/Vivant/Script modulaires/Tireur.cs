using UnityEngine;
using System.Collections;

public class Tireur : MonoBehaviour 
{
    public GameObject cible;

    public GameObject bullet_prefab;
    private GameObject bullet;
    private Bullet bullet_script;

    public float frequence;
    public float vitesse;

    private Vector3 dir;

	private GameObject m_Player;

	public Animator anim;

    void OnEnable()
    {
		m_Player = GameObject.FindGameObjectWithTag("Player");
    }

	public void Tir()
	{
		//INSTANTIATION
		bullet = Instantiate(bullet_prefab, this.gameObject.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation
		bullet.transform.parent = this.gameObject.transform.parent.parent.parent;


		dir = new Vector3(m_Player.transform.position.x - this.gameObject.transform.parent.transform.position.x, m_Player.transform.position.y - 1 - this.gameObject.transform.parent.transform.position.y, m_Player.transform.position.z);
		//dir = cible.transform.position.normalized;

		//ENVOI dans une direction
		bullet.GetComponent<Rigidbody2D>().AddForce(dir * vitesse * 0.001f);
	}



}
