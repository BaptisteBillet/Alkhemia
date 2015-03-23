using UnityEngine;
using System.Collections;

public class Fuis : MonoBehaviour {

    //Cible
    public Transform cible;
    private Vector3 destination;
    private Vector3 autre_sens;


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

    //Gameobject
    public GameObject source_sprite;

    //Animation
    Animator anim;

    void OnEnable()
    {
        up = false;
        down = false;
        left = false;
        right = false;
        //rigidbody2D.velocity *= 0;
        if (anim == null)
        {
            anim = source_sprite.GetComponent<Animator>();
            anim.SetBool("moving", false);
        }
    }

    void Update()
    {
        destination = cible.position;


        if (Vector3.Distance(this.transform.position, destination) < distance)
        {
            autre_sens = new Vector3(destination.x, destination.y , destination.z);

            deplacement();
            vecteur();
            acc_des();
            animation();
        }
        else
        {
            up = false;
            down = false;
            left = false;
            right = false;
            velocity = new Vector3(0, 0, 0);
            moveSpeed = 0;
            GetComponent<Rigidbody2D>().velocity = velocity * moveSpeed;
            anim.SetBool("moving", false);
        }
    }


    #region deplacement

    
    void deplacement()
    {
        if (velocity.sqrMagnitude > 0)
        {
            if (autre_sens.x > transform.position.x)
            {
                left = true;
                right = false;
            }
            if (autre_sens.x < transform.position.x)
            {
                right = true;
                left = false;
            }
            if (autre_sens.y < transform.position.y)
            {
                up = true;
                down = false;

            }
            if (autre_sens.y > transform.position.y)
            {
                down = true;
                up = false;
            }
        }
        else
        {
            up = false;
            down = false;
            left = false;
            right = false;
            anim.SetBool("moving", false);
        }
     
    }
    void vecteur()
    {

        velocity = (transform.position - autre_sens).normalized;

       
    }
    void acc_des()
    {
        //On ajoute une acceleration

        if(velocity.sqrMagnitude>0)
        {
            moveSpeed += acceleration;
            if (moveSpeed >= max_moveSpeed)
            {
                moveSpeed = max_moveSpeed;
            }
        }
        else
        {
            moveSpeed = 0;
            up = false;
            down = false;
            left = false;
            right = false;
            anim.SetBool("moving", false);

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
