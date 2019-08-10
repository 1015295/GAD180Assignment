using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    //how many keys players have
    public int player1Keys;
    public int player2Keys;
    //how many key players need
    public int keysNeeded;
    //array of doors
    public GameObject[] doors;
    //if the players are possesing the doll
    public bool player1Doll = false;
    public bool player2Doll = false;
    //the active doors
    private GameObject player1Door;
    private GameObject player2Door;


    
    void Start()
    {
        //finds the doors and ads them to a array
        doors = GameObject.FindGameObjectsWithTag("Door");

        
    }

    void Update()
    {

        //disables the active doors and clears the active door gameobject
        if (player1Doll == false && player1Door != null)
        {

            player1Door.GetComponent<Door>().isActive = false;
            player1Door = null;

        }

        if (player2Doll == false && player2Door != null)
        {

            player2Door.GetComponent<Door>().isActive = false;
            player2Door = null;

        }

        // checks which player has the doll depending the player script
        if (GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>().isDoll == true)
        {

            player1Doll = true;

        }

        if (GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>().isDoll == true)
        {

            player2Doll = true;

        }



        //runs the slect door function
        if(player1Keys == keysNeeded || player2Keys == keysNeeded)
        {
            //Debug.Log("got the keys");
            if (player1Doll == true || player2Doll == true)
            {

                //Debug.Log("SelectDoor");
                SelectDoor();

            }

        }
        
    }
    //selects a door and activates it 
    void SelectDoor()
    {
        int activeDoor;

        if(player1Keys == keysNeeded && player1Door == null)
        {

            activeDoor = Random.Range(0, doors.Length);
            //Debug.Log(activeDoor);

            player1Door = doors[activeDoor];
            player1Door.GetComponent<Door>().isActive = true;
            player1Door.GetComponent<Door>().hasAccess = Door.players.Player1;

        }

        if(player2Keys == keysNeeded && player2Door == null)
        {

            activeDoor = Random.Range(0, doors.Length);

            player2Door = doors[activeDoor];
            player2Door.GetComponent<Door>().isActive = true;
            player2Door.GetComponent<Door>().hasAccess = Door.players.Player2;

        }

    }

}
