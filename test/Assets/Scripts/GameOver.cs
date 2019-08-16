using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerMov playerMovement;
    public PlayerHealth playerHealth;
    public GameObject Player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(playerHealth.currentHealth <= 0 || (playerMovement.wave == 3 && GameObject.FindGameObjectWithTag("Enemy") == null)){
        GameObject fade = GameObject.Find("fade");
        fade.SetActive(true);
        fadeToLevel();
      }
    }

    public void fadeToLevel(){
      animator.SetTrigger("fade");
    }

    public void onFadeComplete(){
      SceneManager.LoadScene(0);
    }
}
