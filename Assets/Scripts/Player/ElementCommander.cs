using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCommander : MonoBehaviour
{
    public string RedButton;
    public string BlueButton;
    public string GreenButton;

    public Elemental redElement;
    public Elemental blueElement;
    public Elemental greenElement;

    private void FixedUpdate()
    {
        if(RedButton != "" && redElement)
        {
            if (Input.GetButtonDown(RedButton))
            {
                redElement.DoInteract();
            }
        }
        if(BlueButton != "" && blueElement)
        {
            if (Input.GetButtonDown(BlueButton))
            {
                blueElement.DoInteract();
            }
        }
        if(GreenButton != "" && greenElement)
        {
            if (Input.GetButtonDown(GreenButton))
            {
                greenElement.DoInteract();
            }
        }
    }
}
