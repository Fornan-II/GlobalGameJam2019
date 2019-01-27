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

    public float interactRange = 10.0f;
    public bool WithinInteractRange
    {
        private set;
        get;
    }

    private void FixedUpdate()
    {
        ElementNode nodeToInteractWith = ElementNode.GetNearestNode(transform.position, interactRange, true);
        if(nodeToInteractWith)
        {
            WithinInteractRange = true;
            if (redElement)
            {
                if (RedButton != "")
                {
                    if (Input.GetButtonDown(RedButton))
                    {
                        redElement.DoInteract(nodeToInteractWith);
                    }
                }
            }

            if (blueElement)
            {
                if (BlueButton != "")
                {
                    if (Input.GetButtonDown(BlueButton))
                    {
                        blueElement.DoInteract(nodeToInteractWith);
                    }
                }
            }

            if (greenElement)
            {
                if (GreenButton != "")
                {
                    if (Input.GetButtonDown(GreenButton))
                    {
                        greenElement.DoInteract(nodeToInteractWith);
                    }
                }
            }
        }
        else
        {
            WithinInteractRange = false;
        }
    }
}
