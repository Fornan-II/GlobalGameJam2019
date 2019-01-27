using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractInstructions : MonoBehaviour {

    public ElementCommander elementComm;
    public string buttonColor;
    public Text instructions;

	// Use this for initialization
	void Start () {
        instructions.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (elementComm.WithinInteractRange)
        {
            buttonColor = elementComm.RequiredInteractButton;
            instructions.enabled = true;

            if (buttonColor == "Red Button")
            {
                instructions.text = "Press R";
                instructions.color = Color.red;

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("pressed R");
                    
                    instructions.text = "";
                }
            }


            if (buttonColor == "Blue Button")
            {
                instructions.text = "Press B";
                instructions.color = Color.blue;

                if (Input.GetKeyDown(KeyCode.B))
                {
                    instructions.enabled = false;
                }
            }

            if (buttonColor == "Green Button")
            {
                instructions.text = "Press G";
                instructions.color = Color.green;

                if(Input.GetKeyDown(KeyCode.B))
                {
                    instructions.enabled = false;
                }
            }
        }
    }
}
