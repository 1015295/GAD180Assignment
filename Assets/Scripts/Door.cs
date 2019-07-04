﻿using System.Collections;
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

        }

    }

}
