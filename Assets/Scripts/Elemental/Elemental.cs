using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : MonoBehaviour
{
    public static List<Elemental> ActiveElementals = new List<Elemental>();

    public bool HasBefriendedPlayer = false;
    public string InteractButton;
    public bool FollowPlayer;
    public Transform player;
    public Transform otherTarget;
    public bool dontLetInteract = false;
    public ElementalBehavior myBehavior;

    public Animator _anim;

	void Awake ()
    {
        _anim = gameObject.GetComponentInChildren<Animator>();
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
            _anim.ResetTrigger("Idle");
            _anim.SetTrigger("Follow");
            otherTarget = node.transform;
            myBehavior.objectOfInterest = otherTarget;
            myBehavior.CurrentState = ElementalBehavior.State.INTERACT;
            dontLetInteract = true;
        }
    }
}
