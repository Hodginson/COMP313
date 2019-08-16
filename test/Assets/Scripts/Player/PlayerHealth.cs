using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  public int startingHealth = 100;
  public int currentHealth;
  public Slider healthSlider;
  public Image damageImage;
//  public AudioClip deathClip;
  public float flashSpeed = 5f;
  public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //flash the screen red to let the player know they took damage


  Animator anim;
  //AudioSource playerAudio;
  PlayerMov playerMovement;

  bool isDead; //is the player dead?
  bool damaged;


  void Awake ()
  {
      anim = GetComponent <Animator> ();
    //  playerAudio = GetComponent <AudioSource> ();
      playerMovement = GetComponent <PlayerMov> ();
      //playerShooting = GetComponentInChildren <PlayerShooting> ();
      currentHealth = startingHealth;
  }


  void Update ()
  {
      if(damaged)
      {
          damageImage.color = flashColour;
      }
      else
      {
          damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
      }
      damaged = false;
  }


  public void TakeDamage (int amount)
  {
    if (isDead) {
      return;
    }
      damaged = true;

      currentHealth -= amount;

      healthSlider.value = currentHealth; //update the health bar slider

    //  playerAudio.Play ();

      if(currentHealth <= 0) //if the players health is 0 or below call the death function
      {
          Death ();
      }
  }

  public void eatSandwich(){ //after picking up the health item(sandwich) update the players health
    currentHealth += 50;
    if(currentHealth > startingHealth){
      currentHealth = startingHealth;
    }
    healthSlider.value = currentHealth;
  }


  void Death ()
  {
      isDead = true;


      //playerAudio.clip = deathClip;
      //playerAudio.Play ();

      playerMovement.enabled = false; //stop the player from being able to move
      //playerShooting.enabled = false;
  }


  public void RestartLevel ()
  {
      SceneManager.LoadScene (0);
  }
}
