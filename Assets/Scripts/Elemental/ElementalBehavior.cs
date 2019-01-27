using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalBehavior : MonoBehaviour
{
    public enum State
    {
        NONE,
        FOLLOW,
        INTERACT
    }
    public State CurrentState = State.NONE;

    public Elemental myElemental;

    public Transform objectOfInterest;
    public Vector2 pointOfInterest;
    public float followRange = 2.5f;
    public float interactRange = 0.5f;
    public float moveSpeed = 10.0f;
    public float farAwayRange = 7.0f;
    public float speedMultiplier = 1.0f;
    public float turningSpeed = 30.0f;
    public float turningMultiplier = 1.0f;

    protected virtual void FixedUpdate()
    {
        if (CurrentState == State.NONE)
        {
            ProcessStateNone();
        }
        else if (CurrentState == State.FOLLOW)
        {
            ProcessStateFollow();
        }
        else if (CurrentState == State.INTERACT)
        {
            ProcessStateInteract();
        }
    }

    protected virtual void ProcessStateNone()
    {
        //idle anim at most
    }

    protected virtual void ProcessStateFollow()
    {
        if(objectOfInterest)
        {
            pointOfInterest = objectOfInterest.transform.position;
        }
        Vector2 position = transform.position;
        Vector2 moveDir = pointOfInterest - position;
        float angleBetween = Vector2.SignedAngle(transform.right, moveDir);
        float rotRange = 1.0f;
        if (moveDir.sqrMagnitude > farAwayRange * farAwayRange)
        {
            transform.Rotate(Vector3.forward, angleBetween);
            rotRange = 0.0f;
            speedMultiplier = Mathf.Clamp(speedMultiplier + Time.fixedDeltaTime, 1.0f, 7.0f);
        }
        else if (moveDir.sqrMagnitude > followRange * followRange)
        {
            rotRange = 3.0f;
            speedMultiplier = Mathf.Clamp(speedMultiplier + Time.fixedDeltaTime, 1.0f, 3.0f);
        }
        else
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier - Time.fixedDeltaTime, 1.0f, 3.0f);
        }

        if (angleBetween > 0.0f)
        {
            //Debug.Log("greater than 0");
            turningMultiplier = Mathf.Clamp(turningMultiplier - rotRange * Time.fixedDeltaTime, -rotRange, rotRange);
        }
        else if(angleBetween < 0.0f)
        {
            //Debug.Log("less than 0");
            turningMultiplier = Mathf.Clamp(turningMultiplier + rotRange * Time.fixedDeltaTime, -rotRange, rotRange);   
        }
        
        transform.Rotate(Vector3.forward, turningMultiplier * turningSpeed * Time.fixedDeltaTime);

        transform.position += transform.right * moveSpeed * speedMultiplier * Time.fixedDeltaTime;
    }

    public float interactTimeOut = 10.0f;
    protected Coroutine interactTimeOuter;

    protected virtual void ProcessStateInteract()
    {
        if(interactTimeOuter == null)
        {
            interactTimeOuter = StartCoroutine(InteractTimeOut(interactTimeOut));
        }

        if (objectOfInterest)
        {
            pointOfInterest = objectOfInterest.transform.position;
        }
        Vector2 position = transform.position;
        Vector2 moveDir = position - pointOfInterest;
        float angleBetween = Vector2.SignedAngle(transform.right, moveDir);
        float rotRange = 2.0f;
        if (moveDir.sqrMagnitude > followRange * followRange)
        {
            rotRange = 4.0f;
            speedMultiplier = Mathf.Clamp(speedMultiplier + Time.fixedDeltaTime, 1.0f, 3.0f);
        }
        else if(moveDir.sqrMagnitude < interactRange * interactRange)
        {
            rotRange = 3.0f;
            speedMultiplier = Mathf.Clamp(speedMultiplier - Time.fixedDeltaTime, 0.1f, 1.0f);

            //At this point, it's time to interact
            if(objectOfInterest)
            {
                ElementNode node = objectOfInterest.GetComponent<ElementNode>();
                if (node)
                {
                    StopCoroutine(interactTimeOuter);
                    StartCoroutine(TriggerNodeInteract(1.0f, node));
                }
            }
        }
        else
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier - Time.fixedDeltaTime, 1.0f, 2.0f);
        }

        if (angleBetween > 0.0f)
        {
            turningMultiplier = Mathf.Clamp(turningMultiplier - rotRange * Time.fixedDeltaTime, -rotRange, rotRange);
        }
        else if (angleBetween < 0.0f)
        {
            turningMultiplier = Mathf.Clamp(turningMultiplier + rotRange * Time.fixedDeltaTime, -rotRange, rotRange);
        }
        
        transform.Rotate(Vector3.forward, turningMultiplier * turningSpeed * Time.fixedDeltaTime);

        transform.position += transform.right * moveSpeed * speedMultiplier * Time.fixedDeltaTime;
    }

    protected IEnumerator TriggerNodeInteract(float t, ElementNode interactTarget)
    {
        yield return new WaitForSeconds(t);
        interactTarget.OnInteract(myElemental);
        myElemental.dontLetInteract = false;
        objectOfInterest = myElemental.player;
        CurrentState = State.FOLLOW;
    }

    protected IEnumerator InteractTimeOut(float t)
    {
        Debug.Log("time outter start...");
        if(t <= 0.0f)
        {
            Debug.LogWarning("Time out value set to less than or equal to 0, defaulting to 10");
            t = 10.0f;
        }
        yield return new WaitForSeconds(t);
        Debug.Log("Timed out.");
        //If just to be safe (or maybe lazy)
        if(CurrentState == State.INTERACT)
        {
            myElemental.dontLetInteract = false;
            objectOfInterest = myElemental.player;
            CurrentState = State.FOLLOW;
        }
    }
}
