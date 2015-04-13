
using UnityEngine;
using System.Collections;
 
public class Pause: MonoBehaviour {
  
 //refrence for the pause menu panel in the hierarchy
 public GameObject fenetre_pause_menu;
 //animator reference
 private Animator anim;
 //variable for checking if the game is paused
 private bool isPaused = false;
 // Use this for initialization
 void Start () {
  //unpause the game on start
  Time.timeScale = 1;

 }
  
 // Update is called once per frame
 public void Update () {
  //pause the game on escape key press and when the game is not already paused
  if((Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("Start_1")))&&isPaused==false)
  {
		fenetre_pause_menu.SetActive(true);
		PauseGame();
  }
  //unpause the game if its paused and the escape key is pressed
  else if ((Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("Start_1")))&&isPaused==true)
  {
	  fenetre_pause_menu.SetActive(false);
	  UnpauseGame();
  }
 }

 //function to pause the game
 public void PauseGame(){
  /*
     //enable the animator component
  anim.enabled = true;
  //play the Slidein animation
  anim.Play("PauseMenuSlideIn");
  */


  //set the isPaused flag to true to indicate that the game is paused
  isPaused = true;
  //freeze the timescale
  Time.timeScale = 0;
 }
 //function to unpause the game
 public void UnpauseGame(){
   
  //set the isPaused flag to false to indicate that the game is not paused
  isPaused = false;
    /*
  //play the SlideOut animation
  anim.Play("PauseMenuSlideOut");
     */
  //set back the time scale to normal time scale
  Time.timeScale = 1;
 }
  
}