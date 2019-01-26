using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float acceleration = 1.0f;
    public float maxSpeed = 10.0f;
    public AnimationCurve accelCurve;
    [Range(0, 1)] public float friction;
    public Vector2 currentSpeed;
    public float gravity = 20.0f;
    public float jumpForce = 5.0f;

    [SerializeField] private bool _isGrounded = true;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();

        float inputVector = 0.0f;
        inputVector += Input.GetAxisRaw("Horizontal");

        bool doJump = false;
        doJump = Input.GetButton("Jump");

        MovementWithFriction(inputVector, doJump);
    }

    //MOVEMENT
    //
    public void MovementLinearAccel(float inputVector)
    {
        float moveForce = Time.fixedDeltaTime * acceleration;
        float newVelocity = _rb.velocity.x + (moveForce * inputVector);

        if (Mathf.Abs(newVelocity) > maxSpeed)
        {
            newVelocity = Mathf.Max(maxSpeed, Mathf.Abs(_rb.velocity.x)) * Mathf.Sign(_rb.velocity.x);
        }

        _rb.velocity = new Vector2(newVelocity, _rb.velocity.y);
        Debug.Log("New velocity: " + newVelocity);
    }

    public void MovementCurveAccel(float inputVector)
    {
        float moveForce = Time.fixedDeltaTime * acceleration;

        float tFactor = Mathf.Clamp01((inputVector * _rb.velocity.x) / maxSpeed);
        float accelMultiplier = accelCurve.Evaluate(tFactor);
        //Debug.Log("tFactor: " + tFactor + "\nAcceleration: " + accelMultiplier);

        float newVelocity = _rb.velocity.x + (moveForce * accelMultiplier * inputVector);

        _rb.velocity = new Vector2(newVelocity, _rb.velocity.y);
        currentSpeed = _rb.velocity;
    }

    public void MovementWithFriction(float inputVector, bool doJump)
    {
        //Initialize base forces
        float moveForce = Time.fixedDeltaTime * acceleration;
        float yForce = 0.0f;
        if (!_isGrounded)
        {
            yForce = _rb.velocity.y + gravity * Time.fixedDeltaTime * -1f;
        }
        else if(doJump)
        {
            yForce = jumpForce;
        }

        //Calculate acceleration using animation curve
        float tFactor = Mathf.Clamp01((inputVector * _rb.velocity.x) / maxSpeed);
        float accelMultiplier = accelCurve.Evaluate(tFactor);

        //Calculate horizontal velocity
        float newVelocity = _rb.velocity.x + (moveForce * accelMultiplier * inputVector);

        //Figure out friction based on input
        if (inputVector == 0.0f && _isGrounded)
        {
            _rb.velocity = new Vector2(newVelocity * friction, yForce);
        }
        else
        {
            _rb.velocity = new Vector2(newVelocity, yForce);
        }

        //Display current speed for debug
        currentSpeed = _rb.velocity;
    }
    //

    void CheckIfGrounded()
    {
        float r = 1.0f;
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        if (cc)
        {
            r = cc.radius;
        }

        int results = Physics2D.CircleCastNonAlloc(transform.position, r, Vector2.down, new RaycastHit2D[2], 0.05f);
        //Debug.Log(results);
        _isGrounded = 1 < results;
    }
}