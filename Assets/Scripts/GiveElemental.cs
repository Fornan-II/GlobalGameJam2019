using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveElemental : MonoBehaviour
{
    public Elemental myElemental;
    public enum ElementalType
    {
        RED,
        GREEN,
        BLUE
    }
    public ElementalType myElementalType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ElementCommander ec = collision.GetComponent<ElementCommander>();
        if(ec)
        {
            if(myElementalType == ElementalType.RED)
            {
                ec.redElement = myElemental;
            }
            else if(myElementalType == ElementalType.GREEN)
            {
                ec.greenElement = myElemental;
            }
            else if(myElementalType == ElementalType.BLUE)
            {
                ec.blueElement = myElemental;
            }
            
            myElemental.HasBefriendedPlayer = true;
            myElemental.myBehavior.CurrentState = ElementalBehavior.State.FOLLOW;
            myElemental.myBehavior.objectOfInterest = myElemental.player;
            gameObject.SetActive(false);
        }
    }
}
