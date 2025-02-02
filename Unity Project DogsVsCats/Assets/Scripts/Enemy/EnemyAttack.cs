﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
  public int attackDamage = 10;               // The amount of health taken away per attack.


public Animator anim;                              // Reference to the animator component.
  PlayerHealth playerHealth;                  // Reference to the player's health.
  EnemyHealth enemyHealth;                    // Reference to this enemy's health.
  bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
  float timer;                                // Timer for counting up to the next attack.
public GameObject player = null;

  void Awake ()
  {
      // Setting up the references.
      playerHealth = player.GetComponent <PlayerHealth> ();
      enemyHealth = GetComponent<EnemyHealth>();

  }


  void OnTriggerEnter (Collider other)
  {
      // If the entering collider is the player
      if(other.gameObject == player)
      {
          //the player is in range.
          playerInRange = true;
      }
  }


  void OnTriggerExit (Collider other)
  {
      // If the exiting collider is the player
      if(other.gameObject == player)
      {
          //the player is no longer in range.
          playerInRange = false;
      }
  }


  void Update ()
  {
      // Add the time since Update was last called to the timer.
      timer += Time.deltaTime;

      // If the timer exceeds the time between attacks, the player is in range and this enemy is alive.
     if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
      {
          // call attack.
          Attack ();
      }

  }


  void Attack ()
  {
      //trigger the attack animation
      anim.SetTrigger("Attack");
      // Reset the timer.
      timer = 0f;

      // If the player has health to lose
      if(playerHealth.currentHealth > 0)
      {
          //damage the player.
          playerHealth.TakeDamage (attackDamage);
      }
  }
}
