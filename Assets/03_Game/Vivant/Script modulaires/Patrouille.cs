using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patrouille : MonoBehaviour {

    //List des étapes
    public List<Vector3> patrouille = new List<Vector3>();

    public Vector3 destination; //La destination en cours
    public int etape;           //L'étape en cours

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

    public float ecart;

    //Source sprite
    public GameObject source_sprite;

    //Animation
    Animator anim;

    void OnEnable()
    {
        up = false;
        down = false;
        left = false;
        right = false;

        if (anim == null)
        {
            anim = source_sprite.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(patrouille.Count-1>0)
        {
            parcours();
            deplacement();
            vecteur();
            acc_des();
            animation();
        }
        else
        {
            moveSpeed = 0;
            left = false; right = false; up = false; down = false;
            velocity=new Vector3(0,0,0);
        }
        
    }


    #region deplacement

    void parcours()
    {
        destination = patrouille[etape];
    }

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

        if (transform.position.x > destination.x - ecart && transform.position.x < destination.x + ecart)
        {
            left = false; right = false;
        }

        if (transform.position.y > destination.y - ecart && transform.position.y < destination.y + ecart)
        {
            up = false; down = false;
        }

        if (destination.x == transform.position.x)
        {
            left = false; right = false;
        }

        if (destination.y == transform.position.y)
        {
            up = false; down = false;
        }

        if (transform.position.x > destination.x - ecart && transform.position.x < destination.x + ecart)
        {
            left = false; right = false;
            if (transform.position.y > destination.y - ecart && transform.position.y < destination.y + ecart)
            {

                up = false; down = false;

                if (etape < patrouille.Count - 1)
                {
                    etape++;
                }
                else if (etape >= patrouille.Count - 1)
                {
                    etape = 0;
                }
            }
        }

      
        if (destination.y == transform.position.y && destination.x == transform.position.x)
        {
            left = false; right = false; up = false; down = false;
            if (patrouille.Count < etape)
            {
                etape++;
            }
            else if (patrouille.Count >= etape)
            {
                etape = 0;
            }
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
