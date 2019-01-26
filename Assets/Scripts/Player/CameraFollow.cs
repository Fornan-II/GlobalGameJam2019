using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform FollowTarget;
    public AnimationCurve followSpeedCurve;
    public Vector3 FollowOffset;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(FollowTarget)
        {
            Vector3 distanceToTarget = transform.position - (FollowTarget.position + FollowOffset);
            float distanceToTravel = followSpeedCurve.Evaluate(distanceToTarget.sqrMagnitude * Time.fixedDeltaTime) * Time.fixedDeltaTime;
            //Debug.Log("Total dist: " + distanceToTarget.sqrMagnitude * Time.fixedDeltaTime + "\nTravelDist: " + distanceToTravel);
            if (distanceToTarget.sqrMagnitude >= distanceToTravel * distanceToTravel)
            {
                //Debug.Log("Close enough");
                transform.position = FollowTarget.position + FollowOffset;
            }
            else
            {
                //Debug.Log("Can travel");
                transform.position = distanceToTarget.normalized * distanceToTravel;
            }
        }
	}
}
