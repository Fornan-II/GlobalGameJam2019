using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementNode : MonoBehaviour {

    //Static stuff
    //
    public static List<ElementNode> allNodes = new List<ElementNode>();

    public static ElementNode GetNearestNode(Vector3 position, float range, bool onlyFindUncolored = false)
    {
        ElementNode nearestNode = null;
        float nearestSquaredDist = 30.0f;
        foreach (ElementNode en in allNodes)
        {
            if(nearestNode == null)
            {
                if ((onlyFindUncolored && !en.isColored) || !onlyFindUncolored)
                {
                    nearestSquaredDist = (en.transform.position - position).sqrMagnitude;
                    if (nearestSquaredDist <= range)
                    {
                        nearestNode = en;
                    }
                }
            }
            else
            {
                if ((onlyFindUncolored && !en.isColored) || !onlyFindUncolored)
                {
                    float squaredDist = (en.transform.position - position).sqrMagnitude;
                    if (squaredDist < nearestSquaredDist)
                    {
                        nearestNode = en;
                        nearestSquaredDist = squaredDist;
                    }
                }
            }
        }
        return nearestNode;
    }
    //

    public Transform trackedTransform;   //by default, set this to the elemental of this node
    public float colorRadius = 2.5f;
    public float grownRadius = 20.0f;
    public bool isColored = false;
    public Material TrackedMaterial;

	void Awake ()
    {
        allNodes.Add(this);
	}

    private void OnDestroy()
    {
        allNodes.Remove(this);
    }
    
    void Update ()
    {
        if (TrackedMaterial)
        {
            TrackedMaterial.SetVector("_Position", transform.position);
            TrackedMaterial.SetFloat("_Radius", colorRadius);
        }
    }

    public virtual void OnInteract(Elemental source)
    {
        //if(!isColored), then...
        //Shrink radius down to zero.
        //Set trackedTransfrom to this object's transform.
        //Grow the radius up to grownRadius.
        //isColored = true;
        //source.dontLetInteract = false;
    }
}
