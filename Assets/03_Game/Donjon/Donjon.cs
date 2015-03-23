using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Donjon : MonoBehaviour {

    //LA ROOM
        //Tableau des portes à instancier
        public GameObject[] room_prefab;

        //La room pour l'instanciation
        private GameObject room;

        //Le script de la room
        public Room room_script;

    //DONNEE DE CALCUL
        //L'id de la room dans laquel le joueur est actuellement
        public static int id_room_actual;

        //Nombre de portes ouvrant des nouvelles salles
        private int nbr_portes_restantes;
            private int min_nbr_portes;
            private int max_nbr_portes;
    //MEMOIRE
        //Liste des salles déjà instanciées
        public List<GameObject> list_room_memoire = new List<GameObject>();

        //Le script pour la room en mémoire
        private Room room_script_memoire;

    //CHANGEMENT DE PIECE
        //Variable de changement de room
        public string sortie_prise; 

    //GRAPHIQUEMENT
        public bool transition;
        public int id_last_room;
        public string from;
        public Vector3 arrive;
        public float smooth = 5;

	// Use this for initialization
	void Start () 
    {
        //Initialisation
        id_room_actual = 0;
        nbr_portes_restantes = 0;
        sortie_prise = "null";
        id_last_room = 0;
        from = "null";

        //Lancement de la première room
        creation_nouvelle_room();

	}
	
    void creation_nouvelle_room(int _x=0, int _y=0, string _from="null")
    {

        room_script = (Room)room_prefab[list_room_memoire.Count].GetComponent(typeof(Room));

        room_script.id_room = list_room_memoire.Count;
        room_script.x_room = _x;
        room_script.y_room = _y;

        room_script.up_id = -2;
        room_script.right_id = -2;
        room_script.down_id = -2;
        room_script.left_id = -2;

        room_script.up_open = false;
        room_script.right_open = false;
        room_script.down_open = false;
        room_script.left_open = false;

        //On attribue les portes et on cherche le min et le max
        definition_nbr_portes();
       
        //On va attribuer les portes supplémentaires


        int aleatoire_nbr_porte = Random.Range(min_nbr_portes, max_nbr_portes);

        for (int i = 1; i <= aleatoire_nbr_porte;i++ )
        {
            attribution_portes();
            //On a créer une nouvelle posibilité
            nbr_portes_restantes++;
        }

        
       //On bloque les autres portes
       if (room_script.up_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
       {
           room_script.up_open = false;
           room_script.up_id = -1;
       }
       if (room_script.down_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
       {
           room_script.down_open = false;
           room_script.down_id = -1; 
       }
       if (room_script.left_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
       {
           room_script.left_open = false;
           room_script.left_id = -1;
       }
       if (room_script.right_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
       {
           room_script.right_open = false;
           room_script.right_id = -1;
       }
      
            

        //On instancie la room
        room = Instantiate(room_prefab[list_room_memoire.Count]) as GameObject;
        list_room_memoire.Add(room);

        from = _from;

        room.transform.position = new Vector3(0, 0, 0);
    }

    void definition_nbr_portes()
    {
        min_nbr_portes = 0;
        max_nbr_portes = 0;

        //On élabore le min
        if(nbr_portes_restantes==0)
        {
            min_nbr_portes = 1;
        }
        else
        {
            min_nbr_portes = 0;
        }

        //On élabore le max

        #region test des portes autour
        //On parcours les pièces déja crées
        for (int i = 0; i < list_room_memoire.Count; i++)
        {
            room_script_memoire = (Room)(list_room_memoire[i]).GetComponent(typeof(Room));
            
            if (i != id_room_actual) //On cherche dans les autres pièces
            {
                //On vérifie l'accès à l'eventuel pièce de gauche
                if (room_script_memoire.x_room == room_script.x_room - 1 && room_script_memoire.y_room == room_script.y_room) //Si la pièce à gauche existe
                {
                    if (room_script_memoire.right_open == true) //Si la pièce à gauche à sa sortie vers la droite ouverte
                    {
                        room_script.left_open = true;     //On créer une porte dans la salle en question
                        room_script.left_id = room_script_memoire.id_room; //On indique à la pièce que la direction est celle de la pièce de droite

                    }
                    else //Si la porte de l'autre pièce est fermé
                    {
                        room_script.left_open = false; //On ferme la porte actuel
                        room_script.left_id = -1;      //Et au cas ou on vire l'id
                    }

                }
                /////////////////////////////////////////////////

                //On vérifie l'accès à l'eventuel pièce de droite
                if (room_script_memoire.x_room == room_script.x_room + 1 && room_script_memoire.y_room == room_script.y_room) //Si la pièce à droite existe
                {
                    if (room_script_memoire.left_open == true) //Si la pièce à droite à sa sortie vers la gauche ouverte
                    {
                        room_script.right_open = true;     //On créer une porte dans la salle en question
                        room_script.right_id = room_script_memoire.id_room; //On indique à la pièce que la direction est celle de la pièce de droite

                    }
                    else //Si la porte de l'autre pièce est fermé
                    {
                        room_script.right_open = false; //On ferme la porte actuel
                        room_script.right_id = -1;      //Et au cas ou on vire l'id
                    }

                }
                ////////////////////////////////////////////////

                //On vérifie l'accès à l'eventuel pièce de haut
                if (room_script_memoire.y_room == room_script.y_room - 1 && room_script_memoire.x_room == room_script.x_room) //Si la pièce en bas existe
                {
                    if (room_script_memoire.up_open == true) //Si la pièce à droite à sa sortie vers la gauche ouverte
                    {
                        room_script.down_open = true;     //On créer une porte dans la salle en question
                        room_script.down_id = room_script_memoire.id_room; //On indique à la pièce que la direction est celle de la pièce de droite

                    }
                    else //Si la porte de l'autre pièce est fermé
                    {
                        room_script.down_open = false; //On ferme la porte actuel
                        room_script.down_id = -1;      //Et au cas ou on vire l'id
                    }

                }
                ////////////////////////////////////////////////

                //On vérifie l'accès à l'eventuel pièce de bas
                if (room_script_memoire.y_room == room_script.y_room + 1 && room_script_memoire.x_room == room_script.x_room) //Si la pièce en haut existe
                {
                    if (room_script_memoire.down_open == true) //Si la pièce à droite à sa sortie vers la gauche ouverte
                    {
                        room_script.up_open = true;     //On créer une porte dans la salle en question
                        room_script.up_id = room_script_memoire.id_room; //On indique à la pièce que la direction est celle de la pièce de droite

                    }
                    else //Si la porte de l'autre pièce est fermé
                    {
                        room_script.up_open = false; //On ferme la porte actuel
                        room_script.up_id = -1;      //Et au cas ou on vire l'id
                    }

                }
                ////////////////////////////////////////////////

            }
        }
        #endregion

        //on va calculer les portes déjà ouvertes
        if(room_script.up_id==-2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
        {
            max_nbr_portes++;
        }
        if (room_script.down_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
        {
            max_nbr_portes++;
        }
        if (room_script.left_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
        {
            max_nbr_portes++;
        }
        if (room_script.right_id == -2) //Si la porte n'est pas condamné, c'est qu'elle est soit à -2 donc vers une nouvelle porte, soit vers un id qui existe
        {
            max_nbr_portes++;
        }

        if(max_nbr_portes>4)
        {
            Debug.LogWarning("Attention plus de 4 portes demandé pour le max");
            max_nbr_portes = 4;
        }

        //Si le minimum est plus grand que le maximum, soit max=0 et min=1 on réajuste
        if (max_nbr_portes < min_nbr_portes)
        {
            max_nbr_portes = min_nbr_portes;
        }

       

        //Nous avons refait les liaisons vers les portes existantes, et nous avons défini un nombre mini et max de portes

    }

    void attribution_portes()
    {
        bool ok = false;

        //Debug.Log(room_script.tab_prochaine_salle[0]);  
        int aleatoire;
        while (ok == false) //Tant qu'une nouvelle porte n'a pas été ouverte
        {
            aleatoire = Random.Range(0, 3); //On hasarde la porte

            if (aleatoire == 0 && room_script.up_open == false && room_script.up_id == -2)
            {
                ok = true;
                room_script.up_open = true; room_script.up_id = -3;
            }
            if (aleatoire == 1 && room_script.right_open == false && room_script.right_id == -2)
            {
                ok = true;
                room_script.right_open = true; room_script.right_id = -3;
            }
            if (aleatoire == 2 && room_script.down_open == false && room_script.down_id == -2)
            {
                ok = true;
                room_script.down_open = true; room_script.down_id = -3;
            }
            if (aleatoire == 3 && room_script.left_open == false && room_script.left_id == -2)
            {
                ok = true;
                room_script.left_open = true; room_script.left_id = -3;
            }
        }

    }

    void generation_ancienne_salle(int _id_ancienne_salle, string direction)
    {


        //On accède à la prochaine room actuel
        room_script = (Room)list_room_memoire[_id_ancienne_salle].GetComponent(typeof(Room));

        switch (direction)
        {
            case "up":
                //Si on va en haut, on s'assure que la prochaine pièce aura comme id la pièce actuel
                room_script.down_id = id_room_actual;
                from = "up";
                break;
            case "right":
                //Si on va en haut, on s'assure que la prochaine pièce aura comme id la pièce actuel
                room_script.left_id = id_room_actual;
                from = "right";
                break;
            case "down":
                //Si on va en haut, on s'assure que la prochaine pièce aura comme id la pièce actuel
                room_script.up_id = id_room_actual;
                from = "down";
                break;
            case "left":
                //Si on va en haut, on s'assure que la prochaine pièce aura comme id la pièce actuel
                room_script.right_id = id_room_actual;
                from = "left";
                break;
        }

        //On "efface" la pièce depuis laquel on vient
        list_room_memoire[id_room_actual].SetActive(false);


        //On actualise l'id
        id_room_actual = room_script.id_room;

        //On génére l'ancienne salle
        list_room_memoire[_id_ancienne_salle].SetActive(true);

    }


	// Update is called once per frame
	void Update ()
    {

        #region sorties
        //Si le joueur à pris une sortie
        if(sortie_prise!="null")
        {
            transition = true;

            //Le joueur à passé une porte. Il y en a donc une de moins
            nbr_portes_restantes--;

            //On accède à la room actuel
            room_script=(Room)list_room_memoire[id_room_actual].GetComponent(typeof(Room));
            
            switch(sortie_prise)
            {
                case "up":
                    sortie_prise = "null";
                    if (room_script.up_id == -3)
                    {
                        //Nouvelle pièce
                        room_script.up_id = list_room_memoire.Count;

                        //On "efface" la pièce depuis laquel on vient
                        list_room_memoire[id_room_actual].SetActive(false);
                        //On change l'id actuel
                        id_room_actual=list_room_memoire.Count;
                        //On crée une nouvelle pièce
                        from = sortie_prise;
                        creation_nouvelle_room(room_script.x_room, room_script.y_room + 1, "up");
                    }
                    //Si la pièce est une ancienne
                    if (room_script.up_id >=0)
                    {
                        
                        generation_ancienne_salle(room_script.up_id,"up");
                    }
                    break;

                case "right":
                    sortie_prise = "null";
                    if (room_script.right_id == -3)
                    {
                        //Nouvelle pièce
                        room_script.right_id = list_room_memoire.Count;

                        //On "efface" la pièce depuis laquel on vient
                        list_room_memoire[id_room_actual].SetActive(false);
                        //On change l'id actuel
                        id_room_actual = list_room_memoire.Count;
                        from = sortie_prise;
                        creation_nouvelle_room(room_script.x_room+1, room_script.y_room,"right");
                    }
                    //Si la pièce est une ancienne
                    if (room_script.right_id >=0)
                    {
                        
                        generation_ancienne_salle(room_script.right_id,"right");
                    }
                    break;

                case "down":
                    sortie_prise = "null";
                   if (room_script.down_id == -3)
                    {
                        //Nouvelle pièce

                        room_script.down_id = list_room_memoire.Count;

                        //On "efface" la pièce depuis laquel on vient
                        list_room_memoire[id_room_actual].SetActive(false);
                        //On change l'id actuel
                        id_room_actual = list_room_memoire.Count;
                        from = sortie_prise;
                        creation_nouvelle_room(room_script.x_room, room_script.y_room - 1,"down");
                    }
                    //Si la pièce est une ancienne
                    if (room_script.down_id >=0)
                    {
                        
                        generation_ancienne_salle(room_script.down_id,"down");
                    }
                    break;

                case "left":
                    sortie_prise = "null";
                    if (room_script.left_id == -3)
                    {
                        //Nouvelle pièce
                        
                        room_script.left_id = list_room_memoire.Count;

                        //On "efface" la pièce depuis laquel on vient
                        list_room_memoire[id_room_actual].SetActive(false);
                        //On change l'id actuel
                        id_room_actual = list_room_memoire.Count;
                        from = sortie_prise;
                        creation_nouvelle_room(room_script.x_room-1, room_script.y_room,"left");
                    }
                    //Si la pièce est une ancienne
                    if (room_script.left_id >=0)
                    {
                        
                        generation_ancienne_salle(room_script.left_id,"left");
                    }
                    break;


            }





        }
       
        #endregion
       
    }
}
