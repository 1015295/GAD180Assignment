using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int startScene;
    public int p1Win1;
    public int p2Win1;
    //public int nextScene;

    private int player1Wins;
    private int player2Wins;



    void Start()
    {

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "MainMenu")
        {

            PlayerPrefs.SetInt("Player1Wins", 0);
            PlayerPrefs.SetInt("Player1Wins", 0);
            PlayerPrefs.SetInt("NextScene", startScene);
            PlayerPrefs.SetInt("StartScene", startScene);
            PlayerPrefs.SetInt("P1Win1", p1Win1);
            PlayerPrefs.SetInt("P2Win1", p2Win1);

        }

        if(scene.name != "MainMenu")
        {

            startScene = PlayerPrefs.GetInt("StartScene");
            p1Win1 = PlayerPrefs.GetInt("P1Win1");
            p2Win1 = PlayerPrefs.GetInt("P2Win1");

        }

    }

    void Update()
    {
        
        if(PlayerPrefs.GetInt("Player1Wins") == 0 && PlayerPrefs.GetInt("Player2Wins") == 0)
        {

            PlayerPrefs.SetInt("NextScene", startScene);
            
        }
        else if(PlayerPrefs.GetInt("Player1Wins") == 1 && PlayerPrefs.GetInt("Player2Wins") == 0)
        {

            PlayerPrefs.SetInt("NextScene", p1Win1);
            

        }
        else if(PlayerPrefs.GetInt("Player1Wins") == 0 && PlayerPrefs.GetInt("Player2Wins") == 1)
        {

            PlayerPrefs.SetInt("NextScene", p2Win1);

        }
        else if(PlayerPrefs.GetInt("Player1Wins") == 2 && PlayerPrefs.GetInt("Player2Wins") == 0)
        {

            Debug.Log("Player 1 Wins");

        }
        else if(PlayerPrefs.GetInt("Player1Wins") == 0 && PlayerPrefs.GetInt("Player2Wins") == 2)
        {

            Debug.Log("Player 2 Wins");

        }

    }


    public void Player1Win()
    {

        PlayerPrefs.SetInt("Player1Wins", PlayerPrefs.GetInt("Player1Wins") + 1);
        PlayerPrefs.SetInt("Player2Wins", PlayerPrefs.GetInt("Player2Wins") - 1);
        if (PlayerPrefs.GetInt("Player2Wins") < 0)
        {

            PlayerPrefs.SetInt("Player2Wins", 0);

        }
        
        Debug.Log(PlayerPrefs.GetInt("Player1Wins"));
        Debug.Log(PlayerPrefs.GetInt("Player2Wins"));

    }

    public void Player2Win()
    {

        PlayerPrefs.SetInt("Player1Wins", PlayerPrefs.GetInt("Player1Wins") - 1);
        PlayerPrefs.SetInt("Player2Wins", PlayerPrefs.GetInt("Player2Wins") + 1);
        if (PlayerPrefs.GetInt("Player1Wins") < 0)
        {

            PlayerPrefs.SetInt("Player1Wins", 0);

        }
        
        Debug.Log(PlayerPrefs.GetInt("Player1Wins"));
        Debug.Log(PlayerPrefs.GetInt("Player2Wins"));

    }

}
