using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : MonoBehaviour
{
    public static List<Elemental> ActiveElementals = new List<Elemental>();

    public bool FollowPlayer;
    public Transform player;
    public Transform target;
    public float interactRange = 10.0f;
    public bool dontLetInteract = false;

	void Awake ()
    {
        ActiveElementals.Add(this);
	}

    private void OnDestroy()
    {
        ActiveElementals.Remove(this);
    }

    public void DoInteract()
    {
        if(dontLetInteract)
        {
            return;
        }

        Debug.Log(name + " trying to interact...");

        ElementNode nodeToInteractWith = ElementNode.GetNearestNode(transform.position, interactRange, true);
        target = nodeToInteractWith.transform;
        dontLetInteract = true;
    }

    protected IEnumerator TriggerNodeInteract(ElementNode interactTarget)
    {
        yield return new WaitForSeconds(1.0f);
        interactTarget.OnInteract(this);
    }
}
