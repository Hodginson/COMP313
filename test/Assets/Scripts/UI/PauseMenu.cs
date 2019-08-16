using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
  public static bool GamePaused = false;
  public GameObject PauseMenuUI;
  public GameObject Hud;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
          if(GamePaused){
            Resume();
          }else{
            Pause();
          }
        }
    }

    public void Resume(){
      PauseMenuUI.SetActive(false);
      Hud.SetActive(true);
      Time.timeScale = 1f;
      GamePaused = false;
    }

    void Pause(){
      PauseMenuUI.SetActive(true);
      Hud.SetActive(false);
      Time.timeScale = 0f;
      GamePaused = true;
    }

    public void LoadMenu(){
      Time.timeScale = 1f;
      SceneManager.LoadScene("MainMenu");
    }

    public void quit(){
      Application.Quit();
    }
}
