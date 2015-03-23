using UnityEngine;
using System.Collections;

public class Balade : MonoBehaviour {


    //BALADE///////////////////////////////////////////////////////////////////////////

        //Indiquateur d'une balade en cours
        public bool balade_en_cours;

        //TAILLE DE LA BALADE//////////////////////////////////////////////////////////////
            // Entier permettant de dire quel est l'envergure de la prochaine destination
            [Range(-4, 4)]
            public float taille_balade;
            
            // Si la balade est aléatoire? Si taille_balade est aléatoire
            public bool balade_aleatoire;
                //Si la balade est aléatoire permet de mettre un min et un max 
                [Range(0.2f,10)]
                public float min_distance; //0.2f est un minimum
                public float max_distance;

            

        //TEMPS DE LA BALADE//////////////////////////////////////////////////////////////
        public float temps_ballade; //Le temps laissé pour la balade
        public bool temps_ballade_aleatoire; //Si le temps est aléatoire
        public float min_temps_ballade;
        public float max_temps_ballade;

    //DEPLACEMENT/////////////////////////////////////////////////////////////////////////

        //La destination
        private Vector3 destination;
        //La distance entre le point actuel et la destination
        private float distance;


    //PAUSE///////////////////////////////////////////////////////////////////////////////
        
        public bool pause_en_cours;

        //TEMPS DE LA BALADE//////////////////////////////////////////////////////////////
        public float temps_pause; //Le temps laissé pour la balade
        public bool temps_pause_aleatoire; //Si le temps est aléatoire
        public float min_temps_pause;
        public float max_temps_pause;

    //BOOL KEYBOARD/////////////////

        public bool rotate;

        public Vector3 velocity;
        
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

	// Use this for initialization
	void OnEnable()
    {

        up = false;
        down = false;
        left = false;
        right = false;


        //On lance une pause, 
        balade_en_cours = false; //
        pause_en_cours = false; //
        StartCoroutine(pause_balade());

        //rigidbody2D.velocity *= 0;

        if (anim == null)
        {
            anim = source_sprite.GetComponent<Animator>();
            anim.SetBool("moving", false);
        }
    }



    IEnumerator pause_balade()
    {
        //TEMPS/////////////////////////////////////////////////////////////

        if (temps_pause_aleatoire == true) //Si le temps est aléatoire
        {
            temps_pause = Random.Range(min_temps_pause, max_temps_pause);
        }
        

        pause_en_cours = true;
        yield return new WaitForSeconds(temps_pause);
        pause_en_cours = false;

        StartCoroutine(balade());

        yield return null;
    }

    IEnumerator balade()
    {
        //TEMPS/////////////////////////////////////////////////////////////
        
        if(temps_ballade_aleatoire==true) //Si le temps est aléatoire
        {
            temps_ballade = Random.Range(min_temps_ballade, max_temps_ballade);
        }
       
        //MOUVEMENT/////////////////////////////////////////////////////////
        //SI c'est aléatoire
        if (balade_aleatoire == true)
        {
           
            //On s'assure que le max soit plus grand que le min
            if (max_distance <= min_distance)
            {
                max_distance = min_distance;
                Debug.LogError("LE MAX EST EGALE AU MIN");
            }

            //On défini une taille aléatoire
            taille_balade = Random.Range(min_distance, max_distance);
            //Position aléatoire
            destination = new Vector3(Random.Range(-taille_balade, taille_balade) + this.gameObject.transform.position.x, Random.Range(-taille_balade, taille_balade) + this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        } 
        if (balade_aleatoire == false)
        {
            float alea_x = Random.value;

            if(alea_x<=0.5)
            {
                alea_x = 1;
            }
            else if (alea_x > 0.5)
            {
                alea_x = -1;
            }
            float alea_y = Random.value;
            if (alea_y <= 0.5)
            {
                alea_y = 1;
            }else if (alea_y > 0.5)
            {
                alea_y = -1;
            }

            destination = new Vector3(taille_balade*alea_x + this.gameObject.transform.position.x, taille_balade*alea_y + this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        }
        
        
        distance = Vector3.Distance(transform.position, destination);

        ////////////////////////////////////////////////////////////////////

        
        balade_en_cours = true;

        //Debug.Log(destination);

        yield return new WaitForSeconds(temps_ballade);
        balade_en_cours = false;
        StartCoroutine(pause_balade());

        yield return null;
    }

  

    void Update()
    {

        if (balade_en_cours == true)
        {
            deplacement();
            vecteur();
            acc_des();
            animation();
        }

        if(balade_en_cours==false)
        {
            up = false;
            down = false;
            left = false;
            right = false;

            velocity = new Vector3(0, 0, 0);
            moveSpeed = 0;
            move = false;
            //anim.SetBool("moving", move);
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
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 45); }
        }
        if (up && right)
        {
            velocity = new Vector3(1, 1, 0);
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 315); }
        }
        if (down && left)
        {
            velocity = new Vector3(-1, -1, 0);
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 135); }
        }
        if (down && right)
        {
            velocity = new Vector3(1, -1, 0);
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
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90); } //transform.Rotate(Vector3.forward * Time.deltaTime * 100); 

        }
        if (right == true && !up && !down)
        {
            velocity = new Vector3(1, 0, 0);
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);}
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
        if(up==false && down ==false && left==false && right==false)
        {
            move = false;
        }

        if(right==true)
        {
            mirror = true;
        }
        if(left==true)
        {
            mirror = false;
        }

        anim.SetBool("moving", move);
        anim.SetBool("mirror", mirror);

    }
#endregion 

    void OnCollisionEnter2D(Collision2D other)
    {
        if(balade_en_cours==true)
        {
            balade_en_cours = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (balade_en_cours == true)
        {
            balade_en_cours = false;
        }
    }


}
