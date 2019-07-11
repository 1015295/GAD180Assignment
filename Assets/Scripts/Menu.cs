using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject pauseMenue;

    void Update()
    {
        //pressing p will launch a function
        if (Input.GetKeyDown(KeyCode.P))
        {

            PauseGame();

        }

    }
    //when called loads scene 1
    public void StartButton()
    {

        SceneManager.LoadScene(1);

    }
    //when called stops time and enables the paus menu gameobject
    public void PauseGame()
    {

        Time.timeScale = 0;
        pauseMenue.SetActive(true);


    }
    //when called starts time and dissables the pause menu object
    public void UnpauseGame()
    {

        Time.timeScale = 1;
        pauseMenue.SetActive(false);

    }
    // when called loads the menu scene
    public void BackToMenu()
    {

        SceneManager.LoadScene(0);

    }
    // when called quits the game
    public void ExitGame()
    {

        Application.Quit();

    }

}
