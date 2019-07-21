using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject nextSceneMenu;
    public GameObject player1WinMenu;
    public GameObject player2WinMenu;

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
        pauseMenu.SetActive(true);


    }
    //when called starts time and dissables the pause menu object
    public void UnpauseGame()
    {

        Time.timeScale = 1;
        pauseMenu.SetActive(false);

    }
    //screen that directs the player to the next scene
    public void NextSceneMenu()
    {

        Time.timeScale = 0;
        nextSceneMenu.SetActive(true);

    }
    //function that launches the next scene
    public void NextScene()
    {

        int nextSceneIndex = PlayerPrefs.GetInt("NextScene");
        Time.timeScale = 1;
        SceneManager.LoadScene(nextSceneIndex);

    }
    // when called loads the menu scene
    public void BackToMenu()
    {

        SceneManager.LoadScene(0);

    }
    //activates the win screen depending on which player won
    public void WinScreen()
    {

        if (PlayerPrefs.GetInt("Player1Wins") >= 2)
        {

            player1WinMenu.SetActive(true);

        }

        if (PlayerPrefs.GetInt("Player2Wins") >= 2)
        {

            player2WinMenu.SetActive(true);

        }

    }
    // when called quits the game
    public void ExitGame()
    {

        Application.Quit();

    }

}
