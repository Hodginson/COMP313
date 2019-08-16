using UnityEngine;
using UnityEngine.UI;

    public class EnemyHealth : MonoBehaviour
    {
        public Slider expSlider;
        public int startingHealth = 100;            // The amount of health the enemy starts the game with.
        public int currentHealth;                   // The current health the enemy has.
        public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.               // The amount added to the player's score when the enemy dies.
        //public AudioClip deathClip;                 // The sound to play when the enemy dies.
        public PlayerMov playerMovement;

        Animator anim;                              // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;                                // Whether the enemy is dead.
        bool isSinking;                             // Whether the enemy has started sinking through the floor.
        bool damaged;
        SpriteRenderer renderer;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //set the colour that the enemy will flash when taking damage to red
        public Color normalColour = new Color(1f, 1f, 1f, 1f);
        EnemyAttack enemyAttack;
        follow Follow;
        void Awake ()
        {
            Follow = GetComponent <follow> ();
          enemyAttack = GetComponent <EnemyAttack> ();
          renderer = GetComponent <SpriteRenderer> ();
            // Setting up the references.
            anim = GetComponent <Animator> ();
            //enemyAudio = GetComponent <AudioSource> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();

            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }


        void Update ()
        {
          if(damaged)
          {
              renderer.color = flashColour;
          }
          else
          {
              renderer.color = Color.Lerp (renderer.color, Color.white, flashSpeed * Time.deltaTime);
          }
          damaged = false;
            // If the enemy should be sinking
            if(isSinking)
            {
                // move the enemy down by the sinkSpeed per second.
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage (int amount)
        {
          damaged = true;
            // If the enemy is dead...
            if(isDead)
                //no need to take damage so exit the function.
                return;

            // Play the hurt sound effect.
            //enemyAudio.Play ();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;

            // If the current health is less than or equal to zero
            if(currentHealth <= 0)
            {
                // the enemy is dead.
                Death ();
            }
        }


        void Death ()
        {

            // The enemy is dead.
            isDead = true;
            enemyAttack.enabled = false;
            Follow.enabled = false;
            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            expSlider.value = expSlider.value + 10;
            StartSinking();

            // Tell the animator that the enemy is dead.
            //anim.SetTrigger ("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            //enemyAudio.clip = deathClip;
          //  enemyAudio.Play ();
        }


        public void StartSinking ()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent <Rigidbody> ().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
            //ScoreManager.score += scoreValue;

            // After 2 seconds destory the enemy.
            Destroy (gameObject, 2f);
        }
    }
