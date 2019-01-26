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
        if(redElement)
        {
            if (RedButton != "")
            {
                if (Input.GetButtonDown(RedButton))
                {
                    redElement.DoInteract();
                }
            }
        }

        if(blueElement)
        {
            if (BlueButton != "")
            {
                if (Input.GetButtonDown(BlueButton))
                {
                    blueElement.DoInteract();
                }
            }
        }

        if(greenElement)
        {
            if (GreenButton != "")
            {
                if (Input.GetButtonDown(GreenButton))
                {
                    greenElement.DoInteract();
                }
            }
        }
    }
}
