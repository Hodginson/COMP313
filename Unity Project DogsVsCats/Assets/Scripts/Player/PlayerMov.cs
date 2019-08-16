using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMov : MonoBehaviour
{
  //TODO:find a way to add a stun enemys
  public Animator animator;
  Vector3 characterScale;
  float horizontalx;
  float yPos;
  //[SerializeField] public Transform GroundCheck;
  [SerializeField] public LayerMask groundLayer; //what can we define as ground
  public float speed = 12;
  private bool Grounded;
  private Vector3 m_Velocity = Vector3.zero;
  public int attackDamage = 20;
  public float range = 1f;
  int colliderMask;
  //create game objects for all the enemy objects
  GameObject Spawnenemy1;
  GameObject Spawnenemy2;
  GameObject Spawnenemy3;
  GameObject Spawnenemy4;
  GameObject Spawnenemy5;
  GameObject Spawnenemy6;
  GameObject Spawnenemy7;
  GameObject Currentenemy1;
  GameObject Currentenemy2;
  GameObject Currentenemy3;
  GameObject Currentenemy4;
  //are the enemys that are currently spawned in the players range
  public bool enemy1InRange = false;
  public bool enemy2InRange = false;
  public bool enemy3InRange = false;
  public bool enemy4InRange = false;

  public bool facingRight = true; //is the player facing to the right?
  private Rigidbody body;
  private Rigidbody enemyRigi;

  public int wave = 1; //the current wave
  float timer;
  public int attackNumber; //cycle between the attacks - could this be done with an RNG or would a combo timer be better for different attacks?
  public float timeBetweenAttacks = 1f; //how long before the player can attack again
  public bool jump = false;  //is the player jumping

    // Start is called before the first frame update
    void Start()
    {
      timer = 10;
      attackNumber = 1;
      //assign the relevent enemys to the game objects - TODO: instatiate the enemys rather than hardcode them -
      Currentenemy1 = GameObject.Find("Cat");
      Currentenemy2 = GameObject.Find("Cat (1)");
      Spawnenemy1 = GameObject.Find("Cat (2)");
      Spawnenemy2 = GameObject.Find("Cat (3)");
      Spawnenemy3 = GameObject.Find("Cat (4)");
      Spawnenemy4 = GameObject.Find("Cat (5)");
      Spawnenemy5 = GameObject.Find("Cat (6)");
      Spawnenemy6 = GameObject.Find("Cat (8)");
      Spawnenemy7 = GameObject.Find("Cat (9)");
      //these enemys we don't want to have active yet
      Spawnenemy1.SetActive(false);
      Spawnenemy2.SetActive(false);
      Spawnenemy2.SetActive(false);
      Spawnenemy3.SetActive(false);
      Spawnenemy4.SetActive(false);
      Spawnenemy5.SetActive(false);
      Spawnenemy6.SetActive(false);
      Spawnenemy7.SetActive(false);
      colliderMask = LayerMask.GetMask ("DestroyableObject");

      body = GetComponent<Rigidbody>();
      characterScale = transform.localScale;
      horizontalx = characterScale.x;
      yPos = transform.position.y;
    }


    // Update is called once per frame
    void Update()
    {

      //
    /*  bool wasGrounded = Grounded;
      Grounded = false;

      Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, .3f, WhatIsGround);
  		for (int i = 0; i < colliders.Length; i++)
  		{
  			if (colliders[i].gameObject != gameObject)
  			{
  				Grounded = true;
          print("hi");
  				if (!wasGrounded)
  					OnLandEvent.Invoke();
  			}
  		}*/

  timer += Time.deltaTime;
  if(Input.GetButtonDown("Fire1") && timer >= timeBetweenAttacks){ //call attack when button pressed and the time between attacks has passed
      attack();
  }

  if(jump == true && IsGrounded()){ //if the player has landed set jump to false and stop the animation
    jump = false;
    animator.SetBool("isJumping",false);
  }

  if(Input.GetButtonDown("Jump") && transform.position.y < 0.5608){ //only jump if the player is below a certain Y positon - TODO: get the boolean Grounded to work rather than this Y value. not sure why it isn't
    	body.AddForce(new Vector2(0f, 1000));
      jump = true;
      animator.SetBool("isJumping",true);
  }

   float horizontal = Input.GetAxis("Horizontal");
   float vertical = Input.GetAxis("Vertical");
   if(horizontal> 0 || horizontal <0 || vertical >0 || vertical < 0){ //if any of the buttons relting to horizontal or vertical is pressed
     animator.SetFloat("Speed",12); //make the walk animation start
     if (horizontal < 0 && facingRight == true) { //if the player is moving left and facing right make the player face to the left
          transform.Rotate(0f,180f,0f);
          facingRight = false;
      }
      if (horizontal > 0 && facingRight == false) //if the player is moving right and not facing right make the player face to the right
      {
        transform.Rotate(0f,180f,0f);
        facingRight = true;
      }

       Vector2 position = transform.position;
       body.MovePosition(new Vector3((transform.position.x - horizontal * 0.15f),
                  transform.position.y,(transform.position.z - vertical * speed * Time.deltaTime))); //update the player position depending on the directions the user wants them to move in
      //Vector3 targetVelocity = new Vector3(-horizontal* speed,body.velocity.y,-vertical*speed);
            			// And then smoothing it out and applying it to the character
      //body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref m_Velocity, 0.05f);
      } else{animator.SetFloat("Speed",0);} //otherwise stop the animation
    }

    void attack(){
      timer = 0f;
      if(jump == false){ //if the player isn't jumping trigger the not=rmal attack animation depending on the attack number
        if(attackNumber == 1){
          animator.SetTrigger("Attack");
          attackNumber++;
        }else if(attackNumber == 2){
          animator.SetTrigger("Attack2");
          attackNumber++;
        } else if(attackNumber == 3){
          animator.SetTrigger("Attack3");
          attackNumber = 1;
        }

      //use a Raycast to see if the attack will hit an enemy
      Vector3 fwd = transform.TransformDirection(Vector3.left);
      RaycastHit hitLeft;
      if(Physics.Raycast(transform.position, fwd,out hitLeft, range,colliderMask)){
        EnemyHealth enemyHealth = hitLeft.collider.GetComponent <EnemyHealth> ();
        if(enemyHealth != null)
        {
            //if so the enemy should take damage.
            enemyHealth.TakeDamage (attackDamage);
        }
      }
    }

    if(jump == true){ //if the player is jumping trigger the slam attack instead
      animator.SetBool("isJumping",false);
      animator.SetTrigger("Slam"); //TODO: get the animation to actually trigger
      jump = false;
      body.AddForce(new Vector2(0f, -1000)); //add a downwards force to the player

      //for each enemy in range call the apply slam force function
      if(enemy1InRange){
        applySlamForce(Currentenemy1);
      }
      if(enemy2InRange){
        applySlamForce(Currentenemy2);
      }
      if(enemy3InRange){
        applySlamForce(Currentenemy3);
      }
      if(enemy4InRange){
        applySlamForce(Currentenemy4);
      }

    }
    //call the wave system function - TODO: find a better way to call this. Maybe a coroutine so that we can have a delay
    waveSystem();


    }

    public void waveSystem(){
      if (GameObject.FindGameObjectWithTag("Enemy") == null) //if there are not enemys active
      {
        if(wave == 2){ //if it is the second wave
          Destroy(GameObject.Find("Enemy Barrier 2")); //destroy the 2nd invisible wall
          //set the following enemys as active and set them as the current enemy
          Spawnenemy4.SetActive(true);
          Spawnenemy5.SetActive(true);
          Spawnenemy6.SetActive(true);
          Spawnenemy6.SetActive(true);
          Currentenemy1 = Spawnenemy4;
          Currentenemy2 = Spawnenemy5;
          Currentenemy3 = Spawnenemy6;
          Currentenemy4 = Spawnenemy7;
          wave=3;
        }
        if(wave == 1){//if it is the first wave
            Destroy(GameObject.Find("Enemy Barrier 1")); //destroy the 1st invisible wall
            //set the following enemys as active and set them as the current enemy
            Spawnenemy1.SetActive(true);
            Spawnenemy2.SetActive(true);
            Spawnenemy3.SetActive(true);
            Currentenemy1 = Spawnenemy1;
            Currentenemy2 = Spawnenemy2;
            Currentenemy3 = Spawnenemy3;
            wave=2; //move to the next wave

        }

      }
    }

    void OnTriggerEnter (Collider other) //TODO: find a better way to decet that a enemy is in range rather than having a check for every enemy
    {
        // If the entering collider is the player...
        if(other.gameObject == Currentenemy1) //TODO: need to find a better way to do this as it gives a null pointer error when the enemy is destroyed. need to add a check if they are there. Maybe add && Currentenemy1 != NULL?
        {
            // ... the player is in range.
            enemy1InRange = true;
        }

        if(other.gameObject == Currentenemy2)
        {
            enemy2InRange = true;
        }
        if(wave >=2){//if it is the second wave we have an extra enemy so need to start checking for the 3rd enemy as well
          if(other.gameObject == Currentenemy3)
          {
              enemy3InRange = true;
          }
        }
        if(wave==3){//if it is the third wave we have an extra enemy so need to start checking for the 4th enemy as well
          if(other.gameObject == Currentenemy4)
          {
              enemy4InRange = true;
          }
        }
    }


    void OnTriggerExit (Collider other) //same as above but for them exiting the trigger
    {
        // If the exiting collider is the player...
        if(other.gameObject == Currentenemy1)
        {
        //  player = other.GameObject;
            // ... the player is in range.
            enemy1InRange = false;
        }
        if(other.gameObject == Currentenemy2)
        {
        //  player = other.GameObject;
            // ... the player is in range.
            enemy2InRange = false;
        }
        if(wave>=2){
          if(other.gameObject == Currentenemy3)
          {
              enemy3InRange = false;
          }
        }
        if(wave==3){

          if(other.gameObject == Currentenemy4)
          {
              enemy4InRange = false;
          }
        }
    }

    bool IsGrounded() {
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    float distance = 0.50f;
    //send a raycast down to see if the player is close enough to the ground to be considered grounded
    RaycastHit hit ;
    if (Physics.Raycast(position, direction,out hit, distance, groundLayer)){
        return true;
    }

    return false;
}

  void applySlamForce(GameObject enemy){

    enemyRigi = enemy.GetComponent<Rigidbody>(); //get the rigibody related to the enemy
    EnemyHealth enemyHealth = enemy.GetComponent <EnemyHealth> (); //get the enemys health
    //get the x and z difference in order to find what direction to push the enemy in order to ensure they are pushed away from the player
    float Xdif = enemy.transform.position.x - transform.position.x;
    float Zdif = enemy.transform.position.z - transform.position.z;
    //make sure that regardless of the distance the smae force is applyed - TODO: find a way so that the closer to the player the greater the damage and force applied and have this decrease as they distance increases
    if(Zdif > 0){
      Zdif = 1;
    }else if(Zdif < 0){
      Zdif = -1;
    }
    if(Xdif > 0){
      Xdif = 1;
    }else if(Xdif < 0){
      Xdif = -1;
    }
    //apply damage - TODO: find a better value for the damage otherwise players may just slam constantly, could also overcome this by adding a timer so that they can't spam it.
    enemyHealth.TakeDamage(30);
    //apply the force sending the enemy in the relevent direction
    enemyRigi.AddForce(new Vector3(Xdif*10000f,1000f,Zdif*10000f));
  }

}
