using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ObjectActiveness : ElementSensitive
{
    public bool NewActiveStateOnTouch = true;

    private SpriteRenderer _sr;
    private MeshRenderer _mr;
    private Collider2D _c;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _mr = GetComponent<MeshRenderer>();
        _c = GetComponent<Collider2D>();
    }

    public override void OnElementTouch()
    {
        if(_sr)
        {
            _sr.enabled = NewActiveStateOnTouch;
        }
        if(_mr)
        {
            _mr.enabled = NewActiveStateOnTouch;
        }
        if(_c)
        {
            _c.enabled = NewActiveStateOnTouch;
        }
    }
}
