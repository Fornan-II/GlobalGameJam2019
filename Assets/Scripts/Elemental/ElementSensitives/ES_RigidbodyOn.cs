using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ES_RigidbodyOn : ElementSensitive
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnElementTouch()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
