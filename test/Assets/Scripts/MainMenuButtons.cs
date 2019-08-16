using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
      {
          SceneManager.LoadScene("test");
      }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }
}
