using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : MonoBehaviour
{
    public static List<Elemental> ActiveElementals = new List<Elemental>();

    public string InteractButton;
    public bool FollowPlayer;
    public Transform player;
    public Transform otherTarget;
    public bool dontLetInteract = false;
    public ElementalBehavior myBehavior;

	void Awake ()
    {
        ActiveElementals.Add(this);
	}

    private void OnDestroy()
    {
        ActiveElementals.Remove(this);
    }

    public void DoInteract(ElementNode node)
    {
        if(dontLetInteract)
        {
            return;
        }

        Debug.Log(name + " trying to interact...");

        if(node)
        {
            otherTarget = node.transform;
            myBehavior.objectOfInterest = otherTarget;
            myBehavior.CurrentState = ElementalBehavior.State.INTERACT;
            dontLetInteract = true;
        }
    }
}
