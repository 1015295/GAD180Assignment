using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int startScene;
    public int p1Win1;
    public int p2Win1;
    public int nextScene;

    private int player1Wins;
    private int player2Wins;



    void Start()
    {
        


    }

    void Update()
    {
        
        if(player1Wins == 0 && player2Wins == 0)
        {

            nextScene = startScene;
            Debug.Log(nextScene);
        }
        else if(player1Wins == 1 && player2Wins == 0)
        {

            nextScene = p1Win1;
            Debug.Log(nextScene);

        }
        else if(player2Wins == 1 && player1Wins == 0)
        {

            nextScene = p2Win1;

        }
        else if(player1Wins == 2 && player2Wins == 0)
        {



        }
        else if(player2Wins == 2 && player1Wins == 0)
        {



        }

    }


    public void Player1Win()
    {

        player1Wins += 1;
        player2Wins -= 1;
        if(player2Wins < 0)
        {

            player2Wins = 0;

        }

    }

    public void Player2Win()
    {

        player2Wins += 1;
        player1Wins -= 1;
        if(player1Wins < 0)
        {

            player1Wins = 0;

        }

    }

}
