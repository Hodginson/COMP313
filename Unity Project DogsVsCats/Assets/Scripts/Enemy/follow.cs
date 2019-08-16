using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : MonoBehaviour
{
  //public Animator animator;
  Vector3 characterScale;
  float xScale;
  public GameObject target = null;
  private NavMeshAgent nav = null;
  Animator animator;
    // Start is called before the first frame update
  void Start()
    {
      characterScale = transform.localScale; //set to the scale of the enemy
      xScale = characterScale.x; //set to x scale of the enemy
      nav = this.GetComponent<NavMeshAgent>(); //get the nav mesh agent
      animator = this.GetComponent<Animator>(); //get the nav mesh animator
    }

    // Update is called once per frame
    void Update()
    {
      float playerPos = target.transform.position.x; //get the player x posiotn
      float playerPosZ = target.transform.position.z; //get the player z posiotn
      float Enemyx =  transform.position.x; //get the enemy x posiotn
      float EnemyZ =  transform.position.z; //get the enemy z posiotn
      if(playerPos > (Enemyx)){
        characterScale.x = -1; //if the player is to the left of the player change the direction the enemy is facing
      } else if (playerPos < (Enemyx)){
        characterScale.x = 1; //if the player is to the right of the player change the direction the enemy is facing
      }
      transform.localScale = characterScale; //apply the changes made to the scale
      nav.SetDestination(target.transform.position); //set the destination to the player
      float distX = playerPos - Enemyx; //calculate the difference in x between the player and enemy
      float distZ = playerPosZ - EnemyZ;  //calculate the difference in z between the player and enemy
      if(distX < -1.5 || distX > 1.5 || distZ < -1.5 || distZ > 1.5){ //if the player is within a distance of 1.5 stop the walk animation otherwise start the walk animation
        animator.SetFloat("Speed",12);
      }else{
        animator.SetFloat("Speed",0);
      }

    }
}
