using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractInstructions : MonoBehaviour {

    public Text instructions;

	// Use this for initialization
	void Start () {
        instructions.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        // if near node
        //instructions.enabled = true;

        //if node is red
        instructions.text = "Press R";
        instructions.color = Color.red;

        //if node is blue
        instructions.text = "Press B";
        instructions.color = Color.blue;

        //if node is green
        instructions.text = "Press G";
        instructions.color = Color.green;
    }
}
