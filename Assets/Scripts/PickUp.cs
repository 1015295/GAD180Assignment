using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //enum to set which player the key belongs to
    public playersKey iBelongTo;
    public enum playersKey { Player1, Player2}

    private KeyManager KeyHolder;
    

    void Start()
    {
        //finds the gamemanager and its componest keymanager
        KeyHolder = GameObject.FindGameObjectWithTag("GameManager").GetComponent<KeyManager>();

    }

    void Update()
    {
        


    }


    //checks so that the object that enters the trigger is the correct player and launches a function
    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == iBelongTo.ToString())
        {

            KeyCollection();

        }

    }
    //checks which player it should add a key to the does it and destroys the key
    void KeyCollection()
    {

        if(iBelongTo.ToString() == "Player1")
        {

            KeyHolder.player1Keys += 1;

            Destroy(this.gameObject);

        }

        if(iBelongTo.ToString() == "Player2")
        {

            KeyHolder.player2Keys += 1;

            Destroy(this.gameObject);

        }

    }

}
