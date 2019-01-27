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
    public string RequiredInteractButton
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
            RequiredInteractButton = nodeToInteractWith.trackedElemental.InteractButton;

            if (redElement)
            {
                if (redElement.InteractButton != "")
                {
                    if (Input.GetButtonDown(redElement.InteractButton))
                    {
                        redElement.DoInteract(nodeToInteractWith);
                    }
                }
            }

            if (blueElement)
            {
                if (blueElement.InteractButton != "")
                {
                    if (Input.GetButtonDown(blueElement.InteractButton))
                    {
                        blueElement.DoInteract(nodeToInteractWith);
                    }
                }
            }

            if (greenElement)
            {
                if (greenElement.InteractButton != "")
                {
                    if (Input.GetButtonDown(greenElement.InteractButton))
                    {
                        greenElement.DoInteract(nodeToInteractWith);
                    }
                }
            }
        }
        else
        {
            WithinInteractRange = false;
            RequiredInteractButton = "";
        }
    }
}
