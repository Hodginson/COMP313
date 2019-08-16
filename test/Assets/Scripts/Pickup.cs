using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player"); for some reson this won't work so need to set in the inspector
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter (Collider other){
      if(other.gameObject == player){ //only pick up if it is the player that walks into the trigger
        playerHealth.eatSandwich();//call the eatSandwich function in player health
      }
      gameObject.SetActive(false);//remove the sandwich from the screen
    }

    // Update is called once per frame
    void Update()
    {

    }
}
