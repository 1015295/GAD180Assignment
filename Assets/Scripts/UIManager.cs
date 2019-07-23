using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //UI for how many keys Player One has.
    public Text keyText1;
    private string player1KeysText;
    public Text keyText2;
    private string player2KeysText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Adding keys to the UI
        player1KeysText = gameObject.GetComponent<KeyManager>().player1Keys.ToString();
        keyText1.text = player1KeysText;
        player2KeysText = gameObject.GetComponent<KeyManager>().player2Keys.ToString();
        keyText2.text = player2KeysText;
    }
}
