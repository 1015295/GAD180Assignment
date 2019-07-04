using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    public int player1Keys;
    public int player2Keys;
    public int keysNeeded;
    public GameObject[] doors;
    public bool player1Doll;
    public bool player2Doll;
    private GameObject player1Door;
    private GameObject player2Door;

    void Start()
    {

        doors = GameObject.FindGameObjectsWithTag("Door");

    }

    void Update()
    {

        if(player1Doll == false && player1Door != null)
        {

            player1Door.GetComponent<Door>().isActive = false;
            player1Door = null;

        }

        if(player2Doll == false && player2Door != null)
        {

            player2Door.GetComponent<Door>().isActive = false;
            player2Door = null;

        }

        if(player1Keys == keysNeeded || player2Keys == keysNeeded)
        {

            if(player1Doll == true || player2Doll == true)
            {

                SelectDoor();

            }

        }
        
    }

    void SelectDoor()
    {
        int activeDoor;

        if(player1Keys == keysNeeded && player1Door == null)
        {

            activeDoor = Random.Range(0, doors.Length);

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
