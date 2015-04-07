using UnityEngine;
using System.Collections;

public class Destination : MonoBehaviour {

    //Cible
    public Transform cible;
    private Vector3 destination;

    public float distance;
	public float delta;

    //BOOL KEYBOARD/////////////////

    private Vector3 velocity;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    private bool move;
    private bool mirror;

    public float moveSpeed;
    public float max_moveSpeed;
    public float acceleration;
    public float desceleration;

    //Verificateur tir
    private bool IsShooting;

    //Source sprite
    public GameObject source_sprite;

    //Animation
    public Animator anim;

	private bool rotate = false;

	public GameObject tireur_script;

    void OnEnable()
    {
        up = false;
        down = false;
        left = false;
        right = false;
        IsShooting = false;
     }

    void Update()
    {
       
        destination = cible.position;

		if (tireur_script != null)
		{
			if (Vector3.Distance(this.transform.position, destination) > distance && Vector3.Distance(this.transform.position, destination) > delta)
			{
			    tireur_script.SetActive(false);
                IsShooting = false;

				deplacement();
				vecteur();
				acc_des();
				animation();
                anim.SetBool("shooting", false);
               
			}
			else
			{



                if (IsShooting == false)
                {
                    tireur_script.SetActive(true);
                    IsShooting = true;

                    up = false;
                    down = false;
                    right = false;
                    left = false;

                    velocity = Vector3.zero;

                    moveSpeed = 0;
                    GetComponent<Rigidbody2D>().velocity = velocity * moveSpeed;
                    move = false;
                    anim.SetBool("shooting", true);
                    anim.SetBool("moving", move);
                    anim.ResetTrigger("shoot");
                    anim.SetTrigger("shoot");
                }
			}
		}
		else
		{
			if (Vector3.Distance(this.transform.position, destination) > distance)
			{
				deplacement();
				vecteur();
				acc_des();
				animation();
			}
			else
			{
                    up = false;
                    down = false;
                    right = false;
                    left = false;

                    velocity = Vector3.zero;

                    moveSpeed = 0;
                    GetComponent<Rigidbody2D>().velocity = velocity * moveSpeed;
                    move = false;
                    anim.SetBool("moving", move);


			}
		}



    }


	#region deplacement

	void deplacement()
	{
		if (destination.x == transform.position.x)
		{
			left = false; right = false;
		}
		else
		{
			if (transform.position.x > destination.x - 0.2f && transform.position.x < destination.x + 0.2f)
			{
				left = false; right = false;
			}
			else
			{
				if (destination.x > transform.position.x)
				{
					left = false;
					right = true;
				}
				if (destination.x < transform.position.x)
				{
					right = false;
					left = true;
				}
			}



		}


		if (destination.y == transform.position.y)
		{
			up = false; down = false;

		}
		else
		{
			if (transform.position.y > destination.y - 0.2f && transform.position.y < destination.y + 0.2f)
			{
				up = false; down = false;
			}
			else
			{
				if (destination.y < transform.position.y)
				{
					up = false;
					down = true;

				}
				if (destination.y > transform.position.y)
				{
					down = false;
					up = true;

				}
			}
		}

	}
	void vecteur()
	{
		//Diagonals
		if (up && left)
		{
			velocity = new Vector3(-1, 1, 0).normalized;
			if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 45); }
		}
		if (up && right)
		{
			velocity = new Vector3(1, 1, 0).normalized;
			if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 315); }
		}
		if (down && left)
		{
			velocity = new Vector3(-1, -1, 0).normalized;
			if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 135); }
		}
		if (down && right)
		{
			velocity = new Vector3(1, -1, 0).normalized;
			if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 225); }
		}


		//Simple direction
		if (up == true && !left && !right)
		{
			velocity = new Vector3(0, 1, 0);

		}
		if (down == true && !left && !right)
		{
			velocity = new Vector3(0, -1, 0);
		}
		if (left == true && !up && !down)
		{
			velocity = new Vector3(-1, 0, 0);
			if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90); }

		}
		if (right == true && !up && !down)
		{
			velocity = new Vector3(1, 0, 0);
			if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270); }
		}

	}
	void acc_des()
	{
		if (tireur_script != null)
		{
		//On ajoute une acceleration
		if ((up || down || left || right) && distance > 1)
		{
			moveSpeed += acceleration * Time.deltaTime;
			if (moveSpeed >= max_moveSpeed)
			{
				moveSpeed = max_moveSpeed;
			}
		}

		
			if (distance < 1) //ou on descelere
			{
				moveSpeed -= desceleration * Time.deltaTime;
				if (moveSpeed <= 0)
				{
					moveSpeed = 0;
				}
			}

			if (distance < 0.5f) //ou on descelere
			{
				moveSpeed = 0;
			}
		}
		else
		{
			if ((up || down || left || right))
			{
				moveSpeed += acceleration * Time.deltaTime;
				if (moveSpeed >= max_moveSpeed)
				{
					moveSpeed = max_moveSpeed;
				}
			}
		}

		//Déplacements
		GetComponent<Rigidbody2D>().velocity = velocity * moveSpeed;

	}

	void animation()
	{
		if (up || down || left || right)
		{
			move = true;
		}
		if (up == false && down == false && left == false && right == false)
		{
			move = false;
		}

		if (right == true)
		{
			mirror = true;
		}
		if (left == true)
		{
			mirror = false;
		}

		if (anim != null)
		{
			anim.SetBool("moving", true);
			anim.SetBool("mirror", mirror);
		}


		if (anim != null)
		{
			anim.SetBool("moving", move);
			anim.SetBool("mirror", mirror);
		}

	}
	#endregion 
}
