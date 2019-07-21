using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool isActive = false;
    private bool playerHasEntered = false;
    public enum players { Player1, Player2}
    public players hasAccess;
    public Material activeMaterial;
    public Material enterMaterial;

    void Start()
    {
        


    }

    void Update()
    {
        
        if(isActive == true && playerHasEntered == false)
        {

            gameObject.GetComponent<MeshRenderer>().material = activeMaterial;

        }

    }

    private void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject.CompareTag(hasAccess.ToString()))
        {
            playerHasEntered = true;

            gameObject.GetComponent<MeshRenderer>().material = enterMaterial;

            if(hasAccess.ToString() == "Player1")
            {

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Player1Win();

            }
            else if(hasAccess.ToString() == "Player2")
            {

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Player2Win();

            }

            if(PlayerPrefs.GetInt("Player1Wins") == 2 || PlayerPrefs.GetInt("Player2Wins") == 2)
            {

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>().WinScreen();

            }

            /*if (PlayerPrefs.GetInt("Player1Wins") < 2 || PlayerPrefs.GetInt("Player2Wins") < 2)
            {

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>().NextSceneMenu();

            }*/

            if (PlayerPrefs.GetInt("Player1Wins") < 2 && PlayerPrefs.GetInt("Player2Wins") < 2)
            {

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>().NextSceneMenu();

            }

        }

    }

}
