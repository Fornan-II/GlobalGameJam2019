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
    public Material[] TrackedMaterials;
    public List<ElementSensitive> ThingsActivatedOnNodeInteract;
    private List<float> _ES_distances;
    private bool _activateElementSensitives = false;
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
        Sync_ES_distances();
    }

    public void OnValidate()
    {
        Sync_ES_distances();
    }

    private void Sync_ES_distances()
    {
        _ES_distances = new List<float>();
        foreach (ElementSensitive es in ThingsActivatedOnNodeInteract)
        {
            if (es)
            {
                float dist = (transform.position - es.transform.position).magnitude;
                _ES_distances.Add(dist);
            }
            else
            {
                _ES_distances.Add(float.PositiveInfinity);
            }
        }
    }

    void Update ()
    {
        if (TrackedMaterials.Length >= 1)
        {
            foreach(Material m in TrackedMaterials)
            {
                m.SetVector("_Position", trackedTransform.position);
                m.SetFloat("_Radius", colorRadius);
            }
        }

        if(_activateElementSensitives && ThingsActivatedOnNodeInteract.Count >= 1)
        {
            for(int i = 0; i < ThingsActivatedOnNodeInteract.Count; i++)
            {
                if(_ES_distances[i] <= colorRadius)
                {
                    ThingsActivatedOnNodeInteract[i].OnElementTouch();
                    ThingsActivatedOnNodeInteract.RemoveAt(i);
                }
            }
        }
    }

    public virtual void OnInteract(Elemental source)
    {
        Debug.Log("Interact!");
        //if(!isColored), then...
        if (!isColored && _anim)
        {
            isColored = true;
            _anim.SetTrigger("Interact");
        }
    }

    public virtual void SetTrackedToThis()
    {
        trackedTransform = transform;
    }

    public virtual void BeginActivatingElementSensitives()
    {
        _activateElementSensitives = true;
    }

    public virtual void EndActivatingElementSensitives()
    {
        _activateElementSensitives = false;
    }
}
