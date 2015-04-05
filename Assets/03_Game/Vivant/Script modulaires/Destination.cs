using UnityEngine;
using System.Collections;

public class Destination : MonoBehaviour {

    //Cible
    public Transform cible;
    private Vector3 destination;

    public float distance;

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

    //Source sprite
    public GameObject source_sprite;

    //Animation
    public Animator anim;

    void OnEnable()
    {
        up = false;
        down = false;
        left = false;
        right = false;

           }

    void Update()
    {
       
        destination = cible.position;

		

        if (Vector3.Distance(this.transform.position, destination) > distance)
        {
			Debug.Log(Vector3.Distance(this.transform.position, destination) > distance);
            deplacement();
            vecteur();
            acc_des();
            animation();
        }
    }


    #region deplacement

    void deplacement()
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

        if (transform.position.x > destination.x - 0.1f && transform.position.x < destination.x + 0.1f)
        {
            left = false; right = false;
        }


        if (destination.x == transform.position.x)
        {
            left = false; right = false;
        }

        if (transform.position.y > destination.y - 0.1f && transform.position.y < destination.y + 0.1f)
        {
            up = false; down = false;
        }

        if (destination.y == transform.position.y)
        {
            up = false; down = false;
        }

    }
    void vecteur()
    {
        //Diagonals
        if (up && left)
        {
            velocity = new Vector3(-1, 1, 0);
        }
        if (up && right)
        {
            velocity = new Vector3(1, 1, 0);
        }
        if (down && left)
        {
            velocity = new Vector3(-1, -1, 0);
        }
        if (down && right)
        {
            velocity = new Vector3(1, -1, 0);
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
        }
        if (right == true && !up && !down)
        {
            velocity = new Vector3(1, 0, 0);
        }

    }
    void acc_des()
    {
        //On ajoute une acceleration
        if (up || down || left || right)
        {
            moveSpeed += acceleration;
            if (moveSpeed >= max_moveSpeed)
            {
                moveSpeed = max_moveSpeed;
            }
        }
        else //ou on descelere
        {
            moveSpeed -= desceleration;
            if (moveSpeed <= 0)
            {
                moveSpeed = 0;
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

        anim.SetBool("moving", move);
        anim.SetBool("mirror", mirror);

    }
    #endregion 
}
