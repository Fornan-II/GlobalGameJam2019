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

    public Elemental trackedElemental;
    public Transform trackedTransform;   //by default, set this to the elemental of this node
    public float colorRadius = 1.5f;
    public float grownRadius = 20.0f;
    public bool isColored = false;
    public Material TrackedMaterial;
    protected Animator _anim;

	void Awake ()
    {
        allNodes.Add(this);
	}

    private void OnDestroy()
    {
        allNodes.Remove(this);
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        trackedTransform = trackedElemental.transform;
    }

    void Update ()
    {
        if (TrackedMaterial)
        {
            TrackedMaterial.SetVector("_Position", trackedTransform.position);
            TrackedMaterial.SetFloat("_Radius", colorRadius);
        }
    }

    public virtual void OnInteract(Elemental source)
    {
        Debug.Log("Interact!");
        //if(!isColored), then...
        if (!isColored && _anim)
        {
            _anim.SetTrigger("Interact");
        }
    }

    public virtual void SetTrackedToThis()
    {
        trackedTransform = transform;
    }
}
